using BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;

namespace BookRoom.Domain.Contract.UseCases.BookRooms
{
    public interface IBookRoomValidationUseCase : IUseCaseBase<BookRoomValidationDTO, BookRoomValidationResponse>
    {
    }
}
