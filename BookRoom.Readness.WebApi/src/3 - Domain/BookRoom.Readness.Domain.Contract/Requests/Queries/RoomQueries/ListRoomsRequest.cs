using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using MediatR;

namespace BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries
{
    public class ListRoomsRequest : IRequest<CommonResponse<List<RoomResponse>>>
    {
    }
}
