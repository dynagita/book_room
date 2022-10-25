using AutoMapper;
using BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Validation.BookRoomValidations;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class BookRoomValidationUseCase : IBookRoomValidationUseCase
    {
        private readonly IMapper _mapper;

        public BookRoomValidationUseCase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<BookRoomValidationResponse> HandleAsync(BookRoomValidationDTO request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<BookRooms>(request);

            var cantStart30DaysAdvancedValidation = new BookRoomCantStart30DaysAdvanced();
            var cantStart30DaysAdvancedResult = await cantStart30DaysAdvancedValidation.ValidateAsync(book, cancellationToken);
            if (!cantStart30DaysAdvancedResult.IsValid)
                return new BookRoomValidationResponse()
                {
                    Valid = false,
                    Error = cantStart30DaysAdvancedResult
                    .Errors
                    .FirstOrDefault()
                    .ErrorMessage
                };

            var bookRoomGrater3DaysValidation = new BookRoomGreater3DaysValidation();
            var bookRoomGrater3DaysResult = await bookRoomGrater3DaysValidation.ValidateAsync(book, cancellationToken);
            if (!bookRoomGrater3DaysResult.IsValid)
                return new BookRoomValidationResponse()
                {
                    Valid = false,
                    Error = bookRoomGrater3DaysResult
                    .Errors
                    .FirstOrDefault()
                    .ErrorMessage
                };

            var startsAtLeast1DayAfterBookingValidation = new BookRoomStartsAtLeast1DayAfterBooking();
            var startsAtLeast1DayAfterBookingResult = await startsAtLeast1DayAfterBookingValidation.ValidateAsync(book, cancellationToken);
            if (!startsAtLeast1DayAfterBookingResult.IsValid)
                return new BookRoomValidationResponse()
                {
                    Valid = false,
                    Error = startsAtLeast1DayAfterBookingResult
                    .Errors
                    .FirstOrDefault()
                    .ErrorMessage
                };

            return new BookRoomValidationResponse()
            {
                Valid = true
            };
        }
    }
}
