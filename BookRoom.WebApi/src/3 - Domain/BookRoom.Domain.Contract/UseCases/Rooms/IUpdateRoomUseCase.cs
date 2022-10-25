using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;

namespace BookRoom.Domain.Contract.UseCases.Rooms
{
    public interface IUpdateRoomUseCase : IUseCaseBase<int, UpdateRoomRequest, CommonResponse<RoomResponse>>
    {
    }
}
