using BookRoom.Domain.Contract.Responses;

namespace BookRoom.Domain.Contract.UseCases.Rooms
{
    public interface IDeleteRoomUseCase : IUseCaseBase<int, CommonResponse<bool>>
    {
    }
}
