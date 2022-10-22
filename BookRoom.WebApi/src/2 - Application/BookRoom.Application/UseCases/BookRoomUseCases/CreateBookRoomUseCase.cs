using AutoMapper;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class CreateBookRoomUseCase : ICreateBookRoomUseCase
    {
        private readonly IBookRoomsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CancelBookRoomUseCase> _logger;

        public CreateBookRoomUseCase(
            IBookRoomsRepository repository,
            IMapper mapper,
            ILogger<CancelBookRoomUseCase> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CommonResponse<BookRoomResponse>> HandleAsync(CreateBookRoomRequest request, CancellationToken cancellationToken)
        {
			try
			{
                var book = _mapper.Map<BookRooms>(request);

                var created = _repository.InsertAsync(book, cancellationToken);

                var response = _mapper.Map<BookRoomResponse>(created);

                return CommonResponse<BookRoomResponse>.Ok(response);
			}
			catch (Exception ex)
			{
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(CreateBookRoomUseCase), nameof(HandleAsync));
                return CommonResponse<BookRoomResponse>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
