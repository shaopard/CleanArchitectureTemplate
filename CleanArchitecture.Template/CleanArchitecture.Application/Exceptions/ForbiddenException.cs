using CleanArchitecture.Application.Resources;

namespace CleanArchitecture.Application.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException()
        {
            UiMessage = ExceptionMessage.Forbidden;
        }

        public ForbiddenException(string resourceName)
        {
            UiMessage = $"{resourceName} is forbidden.";
        }
    }
}
