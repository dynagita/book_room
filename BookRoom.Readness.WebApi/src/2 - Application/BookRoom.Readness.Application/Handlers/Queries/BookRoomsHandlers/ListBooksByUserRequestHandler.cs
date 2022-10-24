using BookRoom.Readness.Domain.Contract.Handlers.Queries.BookRoomsHandlers;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;

namespace BookRoom.Readness.Application.Handlers.Queries.BookRoomsHandlers
{
    public class ListBooksByUserRequestHandler : IListBooksByUserRequestHandler
    {
        private readonly IListBooksByUserUseCase _useCase;

        public ListBooksByUserRequestHandler(IListBooksByUserUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CommonResponse<List<BookRoomResponse>>> Handle(ListBooksByUserRequest request, CancellationToken cancellationToken)
        {
            return await _useCase.HandleAsync(request, cancellationToken);
        }
    }
}
