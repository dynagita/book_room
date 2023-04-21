using AutoBogus;
using AutoMapper;
using BookRoom.Application.UseCases.BookRoomUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Unit.Tests.Utils;
using FluentAssertions;
using Moq;
using Serilog;

namespace BookRoom.Unit.Tests.UseCases.BookRoomUseCases
{
    public class UpdateBookRoomUseCaseTest
    {
        private readonly IUpdateBookRoomUseCase _useCase;
        private readonly Mock<IBookRoomsRepository> _repository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<IBookRoomProducer> _producer;
        private readonly Mock<IBookRoomRequestProducer> _requestProducer;
        private readonly Mock<IBookRoomValidationUseCase> _validationUseCase;

        public UpdateBookRoomUseCaseTest()
        {
            _repository = new Mock<IBookRoomsRepository>();
            _mapper = MapperCreate.CreateMappers();
            _logger = new Mock<ILogger>();
            _producer = new Mock<IBookRoomProducer>();
            _validationUseCase = new Mock<IBookRoomValidationUseCase>();
            _requestProducer = new Mock<IBookRoomRequestProducer>();

            _useCase = new UpdateBookRoomUseCase(
                _repository.Object,
                _mapper,
                _logger.Object,
                _producer.Object,
                _requestProducer.Object,
                _validationUseCase.Object
                );            
        }

        [Fact(DisplayName = "ShouldUpdateBook")]
        public async Task ShouldUpdateBook()
        {
            var bookRoom = new AutoFaker<BookRooms>().Generate();
            bookRoom.Status = Domain.Contract.Enums.BookStatusRoom.Requested;

            var bookRequest = _mapper.Map<UpdateBookRoomRequest>(bookRoom);

            _validationUseCase.Setup(x => x.HandleAsync(It.IsAny<BookRoomValidationDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Contract.Responses.BookRoomsResponses.BookRoomValidationResponse()
                {
                    Valid = true
                });
            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(),It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookRoom);
            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));
            _requestProducer.Setup(x => x.SendAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(bookRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(200);

            response.Data
                .Status
                .Should()
                .Be(Domain.Contract.Enums.BookStatusRoom.Requested);

            _validationUseCase.Verify(x => x.HandleAsync(It.IsAny<BookRoomValidationDTO>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _requestProducer.Verify(x => x.SendAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "ShouldNotUpdateBook")]
        public async Task ShouldNotUpdateBook()
        {
            var bookRoom = new AutoFaker<BookRooms>().Generate();
            bookRoom.Status = Domain.Contract.Enums.BookStatusRoom.Requested;

            var bookRequest = _mapper.Map<UpdateBookRoomRequest>(bookRoom);

            _validationUseCase.Setup(x => x.HandleAsync(It.IsAny<BookRoomValidationDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Contract.Responses.BookRoomsResponses.BookRoomValidationResponse()
                {
                    Valid = true
                });
            _repository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));
            _requestProducer.Setup(x => x.SendAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(bookRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(400);
            response.Data
            .Should()
            .BeNull();
            response.Error
                .Should()
                .Be(ErrorMessages.EXCEPTION_ERROR);

            _validationUseCase.Verify(x => x.HandleAsync(It.IsAny<BookRoomValidationDTO>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Once);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
            _requestProducer.Verify(x => x.SendAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "ShouldBeInvalidUpdateBook")]
        public async Task ShouldBeInvalidUpdateBook()
        {
            var bookRoom = new AutoFaker<BookRooms>().Generate();
            bookRoom.Status = Domain.Contract.Enums.BookStatusRoom.Requested;

            var bookRequest = _mapper.Map<UpdateBookRoomRequest>(bookRoom);

            _validationUseCase.Setup(x => x.HandleAsync(It.IsAny<BookRoomValidationDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Contract.Responses.BookRoomsResponses.BookRoomValidationResponse()
                {
                    Valid = false,
                    Error = ErrorMessages.BookRoomMessages.BOOK_CANT_START_30_DAYS_ADVANCED
                });
            _repository.Setup(x => x.InsertAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookRoom);
            _producer.Setup(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()));
            _requestProducer.Setup(x => x.SendAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()));

            var response = await _useCase.HandleAsync(bookRequest, new CancellationToken());

            response.Status
                .Should()
                .Be(400);

            response.Data
                .Should()
                .BeNull();

            response.Error
                .Should()
                .Be(ErrorMessages.BookRoomMessages.BOOK_CANT_START_30_DAYS_ADVANCED);

            _validationUseCase.Verify(x => x.HandleAsync(It.IsAny<BookRoomValidationDTO>(), It.IsAny<CancellationToken>()), Times.Once);
            _repository.Verify(x => x.InsertAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
            _producer.Verify(x => x.SendAsync(It.IsAny<BookRooms>(), It.IsAny<CancellationToken>()), Times.Never);
            _requestProducer.Verify(x => x.SendAsync(It.IsAny<BookRoomNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
