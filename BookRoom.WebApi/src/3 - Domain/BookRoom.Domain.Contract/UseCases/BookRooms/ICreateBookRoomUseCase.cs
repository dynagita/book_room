using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;

namespace BookRoom.Domain.Contract.UseCases.BookRooms
{
    public interface ICreateBookRoomUseCase : IUseCaseBase<CreateBookRoomRequest, CommonResponse<BookRoomResponse>>
    {
    }
}
