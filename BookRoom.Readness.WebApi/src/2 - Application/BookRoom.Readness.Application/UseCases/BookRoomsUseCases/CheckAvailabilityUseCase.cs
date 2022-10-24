using AutoMapper;
using BookRoom.Readness.Application.UseCases.RoomUseCases;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BookRoom.Readness.Application.UseCases.BookRoomsUseCases
{
    public class CheckAvailabilityUseCase : ICheckAvailabilityUseCase
    {
        private readonly IBookRoomRepository _repository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListBooksByUserUseCase> _logger;

        public CheckAvailabilityUseCase(ILogger<ListBooksByUserUseCase> logger, IMapper mapper, IBookRoomRepository repository, IRoomRepository roomRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _roomRepository = roomRepository;
        }
        public async Task<CommonResponse<CheckAvailabilityResponse>> HandleAsync(CheckAvailabilityRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = new CheckAvailabilityResponse();
                result.Available = await _repository.CheckAvailabilityAsync(request.RoomNumber, request.StartDate, request.EndDate, cancellationToken);

                if (!result.Available)
                {
                    var room = await _roomRepository.GetByRoomNumberAsync(request.RoomNumber, cancellationToken);
                    
                    room.Books = room.Books.Where(x => x.StartDate >= DateTime.Now.Date && 
                                                       x.StartDate <= request.StartDate.AddMonths(3));

                    result.Room = _mapper.Map<RoomResponse>(room);

                    result.UnavailableMessage = ErrorMessages.BookRoomMessages.ROOM_UNAVAILABLE;
                }

                return CommonResponse<CheckAvailabilityResponse>.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(ListRoomUseCase), nameof(HandleAsync));
                return CommonResponse<CheckAvailabilityResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
