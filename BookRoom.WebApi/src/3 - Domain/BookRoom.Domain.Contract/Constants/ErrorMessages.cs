namespace BookRoom.Domain.Contract.Constants
{
    public class ErrorMessages
    {
        public const string EXCEPTION_ERROR = "Our servers are facing a problem, try again in few minutes and if this problem persists please, let us know.";

        public class UserMessages
        {
            public const string USER_REGISTERED = "Email has been already registered";
        }

        public class AuthMessages
        {
            public const string USER_NOTFOUND = "It wasn't found an user with provided e-mail";
            public const string PASSWORD_NOTMATCH = "Wrong password.";
        }

        public class RoomMessages
        {
            public const string ROOM_EXISTS = "There is already a room using this number.";            
        }

        public class BookRoomMessages
        {
            public const string BOOK_GREATER_3_DAYS = "Reservations can't be greater than 3 days.";
            public const string BOOK_CANT_START_30_DAYS_ADVANCED = "Reservations can't be done with more than 30 days advanced.";
            public const string BOOK_STARTS_AT_LEAST_1_DAY_BOOKING = "Reservation must start at least one day after booking!";
        }
    }
}
