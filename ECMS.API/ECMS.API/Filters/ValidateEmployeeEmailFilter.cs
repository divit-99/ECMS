using ECMS.API.DTOs.Interface;
using ECMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECMS.API.Filters
{
    public class ValidateEmployeeEmailFilter : IAsyncActionFilter
    {
        private readonly IEmailValidationService _emailService;

        public ValidateEmployeeEmailFilter(IEmailValidationService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dto = context.ActionArguments
                             .Values
                             .OfType<IHasEmail>()
                             .FirstOrDefault();

            if (dto != null)
            {
                var isValid = await _emailService.IsValidAsync(dto.Email);

                if (!isValid)
                {
                    context.Result = new BadRequestObjectResult(
                        new { error = "Email is invalid or undeliverable!" }
                    );
                    return;
                }
            }

            await next();
        }
    }
}
