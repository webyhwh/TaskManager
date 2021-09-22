using Swashbuckle.AspNetCore.Filters;

namespace TaskManager.Application.Common
{
    /// <summary>
    /// Ошибка валидации входных параметров
    /// </summary>
    public class InvalidRequestResponse : ErrorResponse
    {
        public InvalidRequestResponse()
            : base(10000, Consts.Errors.InvalidRequest)
        {
        }

        public InvalidRequestResponse(string cause)
            : base(10000, Consts.Errors.InvalidRequest, cause)
        {
        }
    }

    /// <summary>
    /// Пример ошибки валидации входных параметров
    /// </summary>
    public class InvalidRequestResponseExamples : IExamplesProvider<InvalidRequestResponse>
    {
        public InvalidRequestResponse GetExamples()
        {
            return new InvalidRequestResponse("Validation failed: \r\n -- PageNumber: `Page Number` must not be empty. Severity: Error\r\n -- PageSize: `Page Size` must not be empty. Severity: Error");
        }
    }
}
