using AutoMapper;
using BookRoom.Service.Application.UseCases.UserPropagation;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Contract.UseCases;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Services.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookRoom.Service.Application.UseCases.RoomPropagation
{
    public class UpdateRoomBookRoomUseCase : IUpdateRoomBookRoomUseCase
    {
        private readonly IMapper _mapper;
        private readonly IBookRoomRepository _repository;
        private readonly ILogger<UpdateRoomBookRoomUseCase> _logger;

        public UpdateRoomBookRoomUseCase(
            ILogger<UpdateRoomBookRoomUseCase> logger,
            IMapper mapper,
            IBookRoomRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task HandleAsync(RoomNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var room = _mapper.Map<Room>(request);
                var books = await _repository.GetAllByRoomAsync(room.Id, cancellationToken);
                if (books.Any())
                {
                    foreach (var item in books)
                    {
                        item.Room = room;
                        await _repository.UpdateAsync(item, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(UpdateRoomBookRoomUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
