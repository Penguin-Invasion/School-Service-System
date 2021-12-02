using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Exceptions;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Filters
{
    public class ExceptionHandlingFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is NotFoundException notFoundException)
            {
                var response = new ServiceResponse<Object>() { Message = notFoundException.Message,Success=false };
                context.Result = new NotFoundObjectResult(response);
            }
            else if (context.Exception is NotCreatedException notCreatedException)
            {
                context.Result = new StatusCodeResult(500);
            }
            else if (context.Exception is NotDeletedException notDeletedException)
            {
                context.Result = new StatusCodeResult(500);
            }
            else if (context.Exception is NotUpdatedException notUpdatedException)
            {
                context.Result = new StatusCodeResult(500);
            }

            return Task.CompletedTask;
        }
    }
}
