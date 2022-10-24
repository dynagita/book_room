using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;

namespace BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases
{
    public interface IListBooksByUserUseCase : IUseCaseBase<ListBooksByUserRequest,CommonResponse<List<BookRoomResponse>>>
    {
    }
}
