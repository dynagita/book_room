using AutoMapper;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.UserPropagation
{
    public class UpdateUserBookRoomUseCase : IUpdateUserBookRoomUseCase
    {
        private readonly IBookRoomRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserBookRoomUseCase> _logger;

        public UpdateUserBookRoomUseCase(
            IBookRoomRepository repository,
            IMapper mapper,
            ILogger<UpdateUserBookRoomUseCase> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task HandleAsync(PropagateUserNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request);
                var books = await _repository.GetAllByUserAsync(user.Reference, cancellationToken);
                if (books.Any())
                {
                    foreach (var item in books)
                    {
                        item.User = user;
                        await _repository.UpdateAsync(item, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(UpdateUserBookRoomUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
