using BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using MediatR;

namespace BookRoom.Readness.Domain.Contract.Handlers.Queries.RoomHandlers
{
    public interface IListRoomRequestHandler : IRequestHandler<ListRoomsRequest, CommonResponse<List<RoomResponse>>>
    {
    }
}
