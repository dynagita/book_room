using AutoMapper;
using BookRoom.Application.UseCases.RooUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.RoomUseCases
{
    public class UpdateRoomUseCase : IUpdateRoomUseCase
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomUseCase> _logger;
        private readonly IRoomProducer _producer;
        public UpdateRoomUseCase(
            IRoomRepository repository,
            IMapper mapper,
            ILogger<CreateRoomUseCase> logger,
            IRoomProducer producer)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _producer = producer;
        }

        public async Task<CommonResponse<RoomResponse>> HandleAsync(int key, UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roomRequest = _mapper.Map<Room>(request);

                var room = await _repository.UpdateAsync(key, roomRequest, cancellationToken);

                await _producer.SendAsync(room, cancellationToken);

                var roomResponse = _mapper.Map<RoomResponse>(room);

                return CommonResponse<RoomResponse>.Ok(roomResponse);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(UpdateRoomUseCase), nameof(HandleAsync));
                return CommonResponse<RoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
