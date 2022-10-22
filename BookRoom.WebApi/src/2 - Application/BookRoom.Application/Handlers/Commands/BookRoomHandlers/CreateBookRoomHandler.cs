using BookRoom.Domain.Contract.Handlers.Commands.BookRoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;

namespace BookRoom.Application.Handlers.Commands.BookRoomHandlers
{
    public class CreateBookRoomHandler : ICreateBookRoomHandler
    {
        private readonly ICreateBookRoomUseCase _useCase;

        public CreateBookRoomHandler(ICreateBookRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<BookRoomResponse>> Handle(CreateBookRoomRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request, cancellationToken);
        }
    }
}
