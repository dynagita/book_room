using BookRoom.Readness.Domain.Contract.Handlers.Queries.RoomHandlers;
using BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using BookRoom.Readness.Domain.Contract.UseCases.RomUseCases;

namespace BookRoom.Readness.Application.Handlers.Queries.RoomHandlers
{
    public class ListRoomRequestHandler : IListRoomRequestHandler
    {
        private readonly IListRoomUseCase _useCase;

        public ListRoomRequestHandler(IListRoomUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<List<RoomResponse>>> Handle(ListRoomsRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(cancellationToken);
        }
    }
}
