using AutoBogus;
using AutoMapper;
using BookRoom.Readness.Application.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Entities;
using BookRoom.Readness.Domain.Repositories;
using BookRoom.Readness.Unit.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookRoom.Readness.Unit.Tests.UseCases.BookRoomsUseCases
{
    public class ListBooksByUserUseCaseTest
    {
        private readonly Mock<IBookRoomRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ListBooksByUserUseCase>> _logger;
        private readonly IListBooksByUserUseCase _useCase;

        public ListBooksByUserUseCaseTest()
        {
            _repository = new Mock<IBookRoomRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger<ListBooksByUserUseCase>>();

            _useCase = new ListBooksByUserUseCase(
                _logger.Object,
                _mapper,
                _repository.Object
                );
        }

        [Fact(DisplayName = "ShouldListBooks")]
        public async Task ShouldListBooks()
        {
            var request = new ListBooksByUserRequest()
            {
                Email = "fool@fool.com"
            };

            var books = new AutoFaker<BookRooms>().Generate(15);

            _repository.Setup(x => x.GetAllByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(books);

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Status
                .Should()
                .Be(200);
            response.Data
                .Should()
                .HaveCount(15);
        }

        [Fact(DisplayName = "ShouldException")]
        public async Task ShouldException()
        {
            var request = new ListBooksByUserRequest()
            {
                Email = "fool@fool.com"
            };

            var books = new AutoFaker<BookRooms>().Generate(15);

            _repository.Setup(x => x.GetAllByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var response = await _useCase.HandleAsync(request, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
                .Should()
                .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);
        }
    }
}
