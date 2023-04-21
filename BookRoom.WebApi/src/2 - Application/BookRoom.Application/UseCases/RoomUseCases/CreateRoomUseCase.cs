using AutoMapper;
using BookRoom.Application.UseCases.UserUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Domain.Validation.RoomValidations;
using Serilog;

namespace BookRoom.Application.UseCases.RooUseCases
{
    public class CreateRoomUseCase : ICreateRoomUseCase
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRoomProducer _producer;
        public CreateRoomUseCase(
            IRoomRepository repository,
            IMapper mapper,
            ILogger logger
,
            IRoomProducer producer)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _producer = producer;
        }

        public async Task<CommonResponse<RoomResponse>> HandleAsync(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Room room = await _repository.GetByNumber(request.Number, cancellationToken);

                var validation = new RoomNumberExistsValidation(room);

                var newRoom = _mapper.Map<Room>(request);

                var roomExists = await validation.ValidateAsync(newRoom, cancellationToken);
                if (!roomExists.IsValid)
                    return CommonResponse<RoomResponse>.BadRequest(ErrorMessages.RoomMessages.ROOM_EXISTS);

                room = await _repository.InsertAsync(newRoom, cancellationToken);

                await _producer.SendAsync(room, cancellationToken);

                var response = _mapper.Map<RoomResponse>(room);

                return CommonResponse<RoomResponse>.Ok(response);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{UseCase} - {Method} - Exception Thrown", nameof(CreateRoomUseCase), nameof(HandleAsync));
                return CommonResponse<RoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }            
        }
    }
}
