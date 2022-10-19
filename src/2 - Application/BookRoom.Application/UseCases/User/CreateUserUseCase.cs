using AutoMapper;
using BookRoom.Domain.Contract.Requests.Commands.User;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.User;
using BookRoom.Domain.Contract.UseCases;
using BookRoom.Domain.Contract.UseCases.User;
using BookRoom.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.User
{
    public class CreateUserUseCase : ICreateUserUseCase
    {   
        
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserUseCase> logger;
        public CreateUserUseCase(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<CreateUserUseCase> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            this.logger = logger;
        }

        public async Task<CommonResponse<UserResponse>> HandleAsync(UserCreateRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
