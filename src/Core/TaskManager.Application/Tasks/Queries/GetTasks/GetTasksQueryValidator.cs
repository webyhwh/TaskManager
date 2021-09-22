using FluentValidation;

namespace TaskManager.Application.Tasks.Queries.GetTasks
{
    public class GetTasksQueryValidator : AbstractValidator<GetTasksQuery>
    {
        public GetTasksQueryValidator()
        {
            RuleFor(t => t.PageNumber).NotNull().NotEmpty();
            RuleFor(t => t.PageSize).NotNull().NotEmpty();
        }
    }
}
