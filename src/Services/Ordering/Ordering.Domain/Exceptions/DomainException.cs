
namespace Ordering.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) 
            : base($"Domian Exception: \"{message}\" throws from Domain Layer.")
        {
        }
    }
}
