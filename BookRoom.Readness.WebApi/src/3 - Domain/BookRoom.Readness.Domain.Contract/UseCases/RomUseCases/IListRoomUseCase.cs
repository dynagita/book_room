using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;

namespace BookRoom.Readness.Domain.Contract.UseCases.RomUseCases
{
    public interface IListRoomUseCase : IUseCaseOnlyResponse<CommonResponse<List<RoomResponse>>>
    {
    }
}
