using AutoMapper;
using BookRoom.Readness.Application.UseCases.RoomUseCases;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BookRoom.Readness.Application.UseCases.BookRoomsUseCases
{
    public class ListBooksByUserUseCase : IListBooksByUserUseCase
    {
        private readonly IBookRoomRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListBooksByUserUseCase> _logger;

        public ListBooksByUserUseCase(ILogger<ListBooksByUserUseCase> logger, IMapper mapper, IBookRoomRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommonResponse<List<BookRoomResponse>>> HandleAsync(ListBooksByUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.GetAllByEmailAsync(request.Email, cancellationToken);

                return CommonResponse<List<BookRoomResponse>>.Ok(_mapper.Map<List<BookRoomResponse>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(ListRoomUseCase), nameof(HandleAsync));
                return CommonResponse<List<BookRoomResponse>>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
