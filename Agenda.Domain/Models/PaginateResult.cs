using FluentValidation.Results;

namespace Agenda.Domain.Models
{
    public class PaginateResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
        public T Object { get; set; }
        public IList<ValidationFailure> Errors { get; set; }
    }
}
