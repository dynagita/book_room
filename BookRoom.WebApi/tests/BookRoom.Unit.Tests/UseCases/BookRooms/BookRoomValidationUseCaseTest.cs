using AutoMapper;
using BookRoom.Application.UseCases.BookRoomUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Unit.Tests.Utils;
using FluentAssertions;

namespace BookRoom.Unit.Tests.UseCases.BookRoomUseCases
{
    public class BookRoomValidationUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly IBookRoomValidationUseCase _useCase;

        public BookRoomValidationUseCaseTest()
        {
            _mapper = MapperCreate.CreateMappers();
            _useCase = new BookRoomValidationUseCase(_mapper);
        }

        [Fact(DisplayName = "ShouldValidade")]
        public async Task ShouldValidade()
        {
            BookRoomValidationDTO dto = new BookRoomValidationDTO()
            {
                DatAlt = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                StartDate = DateTime.Now.AddDays(7),
            };

            var response = await _useCase.HandleAsync(dto, new CancellationToken());

            response.Valid
                .Should()
                .BeTrue();
            response.Error
                .Should()
                .BeNullOrEmpty();
        }

        [Fact(DisplayName = "ShouldBeLeastOneDayAfterToday")]
        public async Task ShouldBeLeastOneDayAfterToday()
        {
            BookRoomValidationDTO dto = new BookRoomValidationDTO()
            {
                DatAlt = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                StartDate = DateTime.Now,
            };

            var response = await _useCase.HandleAsync(dto, new CancellationToken());

            response.Valid
                .Should()
                .BeFalse();
            response.Error
                .Should()
                .Be(ErrorMessages.BookRoomMessages.BOOK_STARTS_AT_LEAST_1_DAY_BOOKING);
        }

        [Fact(DisplayName = "ShouldStartsBefore30DaysForward")]
        public async Task ShouldStartsBefore30DaysForward()
        {
            BookRoomValidationDTO dto = new BookRoomValidationDTO()
            {
                DatAlt = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2).AddDays(3),
                StartDate = DateTime.Now.AddMonths(2),
            };

            var response = await _useCase.HandleAsync(dto, new CancellationToken());

            response.Valid
                .Should()
                .BeFalse();
            response.Error
                .Should()
                .Be(ErrorMessages.BookRoomMessages.BOOK_CANT_START_30_DAYS_ADVANCED);
        }

        [Fact(DisplayName = "ShouldBeLowerThan3DaysBooking")]
        public async Task ShouldBeLowerThan3DaysBooking()
        {
            BookRoomValidationDTO dto = new BookRoomValidationDTO()
            {
                DatAlt = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                StartDate = DateTime.Now.AddDays(3),
            };

            var response = await _useCase.HandleAsync(dto, new CancellationToken());

            response.Valid
                .Should()
                .BeFalse();
            response.Error
                .Should()
                .Be(ErrorMessages.BookRoomMessages.BOOK_GREATER_3_DAYS);
        }
    }
}
