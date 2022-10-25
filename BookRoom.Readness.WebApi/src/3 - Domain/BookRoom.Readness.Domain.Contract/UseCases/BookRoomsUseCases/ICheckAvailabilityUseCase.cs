using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;

namespace BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases
{
    public interface ICheckAvailabilityUseCase : IUseCaseBase<CheckAvailabilityRequest, CommonResponse<CheckAvailabilityResponse>>
    {
    }
}
