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
    }
}
