using FluentValidation.Results;

namespace Carrefour.Management.Application.Extensions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
        : base(message) { }

        public BadRequestException(IEnumerable<ValidationFailure> failures)
        : base(string.Join("; ", failures.Select(x => x.ErrorMessage))) { }
    }
}