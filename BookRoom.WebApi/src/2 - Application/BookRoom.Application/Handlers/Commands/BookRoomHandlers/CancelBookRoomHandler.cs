using BookRoom.Domain.Contract.Handlers.Commands.BookRoomHandlers;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;

namespace BookRoom.Application.Handlers.Commands.BookRoomHandlers
{
    public class CancelBookRoomHandler : ICancelBookRoomHandler
    {
        private readonly ICancelBookRoomUseCase _useCase;

        public CancelBookRoomHandler(ICancelBookRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<BookRoomResponse>> Handle(CancelBookRoomRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request.Reference, cancellationToken);
        }
    }
}
