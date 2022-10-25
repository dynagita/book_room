using BookRoom.Domain.Contract.Enums;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.Responses.UserResponses;

namespace BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos
{
    public class BookRoomValidationDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookStatusRoom Status { get; set; }

        public DateTime DatAlt { get; set; }
    }
}
