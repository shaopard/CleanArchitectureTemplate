using CleanArchitecture.Application.Resources;

namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
            UiMessage = ExceptionMessage.NotFound;
        }

        public NotFoundException(string message) : base(message)
        {
            UiMessage = message;
        }
    }
}
