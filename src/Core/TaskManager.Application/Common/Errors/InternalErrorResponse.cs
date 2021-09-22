using Swashbuckle.AspNetCore.Filters;

namespace TaskManager.Application.Common
{
    /// <summary>
    /// Неопределенная ошибка
    /// </summary>
    public class InternalErrorResponse : ErrorResponse
    {
        public InternalErrorResponse()
            : base(5050, Consts.Errors.InternalError)
        {
        }

        public InternalErrorResponse(string cause)
            : base(5050, Consts.Errors.InternalError, cause)
        {
        }
    }

    /// <summary>
    /// Пример неопределенной ошибки
    /// </summary>
    public class InternalErrorResponseExamples : IExamplesProvider<InternalErrorResponse>
    {
        public InternalErrorResponse GetExamples()
        {
            return new InternalErrorResponse("System.Data.SqlClient.SqlException (0x80131904): Invalid object name 'test'.\r\n   at System.Data.SqlClient.SqlCommand");
        }
    }
}
