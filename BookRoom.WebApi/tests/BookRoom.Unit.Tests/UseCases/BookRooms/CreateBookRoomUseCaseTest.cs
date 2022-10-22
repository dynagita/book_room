using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class CreateBookRoomUseCaseTest
    {
        public Task<CommonResponse<BookRoomResponse>> HandleAsync(CreateBookRoomRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
