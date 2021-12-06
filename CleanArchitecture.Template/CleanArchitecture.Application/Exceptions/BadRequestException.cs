using CleanArchitecture.Application.Resources;

namespace CleanArchitecture.Application.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException()
        {
            UiMessage = ExceptionMessage.BadRequest;
        }

        public BadRequestException(string message) : base(message)
        {
            UiMessage = message;
        }
    }
}
