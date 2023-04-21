using AutoMapper;
using BookRoom.Application.Extensions;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.UseCases.Users;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Domain.Validation.UserValidations;
using Serilog;

namespace BookRoom.Application.UseCases.UserUseCases
{
    public class CreateUserUseCase : ICreateUserUseCase
    {   
        
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserProducer _userProducer;
        public CreateUserUseCase(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger logger,
            IUserProducer userProducer)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _userProducer = userProducer;
        }

        public async Task<CommonResponse<UserResponse>> HandleAsync(UserCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Password = request.Password.ComputeHash();

                var userDb = await _userRepository.GetByMailAsync(request.Email, cancellationToken);

                var notRegisteredUserValidation = new UserNotRegisteredValidation(userDb);

                var user = _mapper.Map<User>(request);

                var notRegisteredUser = await notRegisteredUserValidation.ValidateAsync(user, cancellationToken);

                if (notRegisteredUser.IsValid)
                {
                    user = await _userRepository.InsertAsync(user, cancellationToken);

                    await _userProducer.SendAsync(user, cancellationToken);

                    return CommonResponse<UserResponse>.Ok(_mapper.Map<UserResponse>(user));
                }

                return CommonResponse<UserResponse>.BadRequest(notRegisteredUser.Errors.FirstOrDefault().ErrorMessage);
                
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{UseCase} - {Method} - Exception Thrown", nameof(CreateUserUseCase), nameof(HandleAsync));
                return CommonResponse<UserResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
