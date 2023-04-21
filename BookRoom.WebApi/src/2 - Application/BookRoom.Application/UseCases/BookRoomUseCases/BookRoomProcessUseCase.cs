using AutoMapper;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using Serilog;

namespace BookRoom.Application.UseCases.BookRoomUseCases
{
    public class BookRoomProcessUseCase : IBookRoomProcessUseCase
    {
        private readonly IBookRoomsRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IBookRoomProducer _producer;

        public BookRoomProcessUseCase(
            IBookRoomsRepository repository,
            ILogger logger,
            IMapper mapper,
            IBookRoomProducer producer)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _producer = producer;
        }

        public async Task HandleAsync(BookRoomNotification request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = await _repository.GetBookRoomByPeriod(request.Room.Number, request.StartDate, request.EndDate, cancellationToken);
                var book = _mapper.Map<BookRooms>(request);

                if (reservation != null && reservation.Id != request.Reference)
                {
                    book.Status = Domain.Contract.Enums.BookStatusRoom.Canceled;
                }
                else
                {
                    book.Status = Domain.Contract.Enums.BookStatusRoom.Confirmed;
                }

                book = await _repository.UpdateAsync(book.Id, book, cancellationToken);

                await _producer.SendAsync(book, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{UseCase} - {Method} - Exception Thrown", nameof(BookRoomProcessUseCase), nameof(HandleAsync));
                //Implement super resilience process here
            }
        }
    }
}
