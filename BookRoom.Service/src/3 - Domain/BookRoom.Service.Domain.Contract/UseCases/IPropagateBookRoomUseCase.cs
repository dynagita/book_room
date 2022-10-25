using BookRoom.Service.Domain.Contract.Notifications;

namespace BookRoom.Service.Domain.Contract.UseCases
{
    public interface IPropagateBookRoomUseCase : IUseCaseBase<BookRoomsNotification>
    {
    }
}
