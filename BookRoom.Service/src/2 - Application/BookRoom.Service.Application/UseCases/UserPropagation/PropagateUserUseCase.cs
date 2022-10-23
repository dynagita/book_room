using AutoMapper;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.UserPropagation
{
    public class PropagateUserUseCase : IPropagateUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PropagateUserUseCase> _logger;
        private readonly IUpdateUserBookRoomUseCase _propagateToBooks;

        public PropagateUserUseCase(
            IUserRepository repository,
            IMapper mapper,
            ILogger<PropagateUserUseCase> logger,
            IUpdateUserBookRoomUseCase propagateToBooks)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _propagateToBooks = propagateToBooks;
        }

        public async Task HandleAsync(UserNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var userDb = await _repository.FindOneAsync(request.Id, cancellationToken);

                var userEvent = _mapper.Map<User>(request);
                if(userDb != null)
                {
                    userEvent.Books = userDb.Books;
                    await _repository.UpdateAsync(userEvent, cancellationToken);
                }
                else
                {
                    await _repository.InsertAsync(userEvent, cancellationToken);
                }

                userDb = await _repository.FindOneAsync(request.Id, cancellationToken);

                var propagateToBooks = _mapper.Map<UserNotification>(userDb);

                await _propagateToBooks.HandleAsync(propagateToBooks, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(PropagateUserUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
