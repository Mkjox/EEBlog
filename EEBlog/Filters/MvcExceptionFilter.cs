using EEBlog.Shared.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlTypes;

namespace EEBlog.Mvc.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;
        private readonly ILogger _logger;

        public MvcExceptionFilter(IHostEnvironment environment, IModelMetadataProvider metadataProvider, ILogger<MvcExceptionFilter> logger)
        {
            _environment = environment;
            _metadataProvider = metadataProvider;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (_environment.IsDevelopment()) //Controls which platform is it on, If project is on live it needs to be IsProduction.
            {
                context.ExceptionHandled = true; // Did error handled?
                var mvcErrorModel = new MvcErrorModel();
                ViewResult result;

                switch (context.Exception)
                {
                    case SqlNullValueException:
                        mvcErrorModel.Message = $"Sorry, there is an error about the database. We will fix it any time soon.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" }; // It will show the Error view for this.
                        result.StatusCode = 500;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;

                    case NullReferenceException:
                        mvcErrorModel.Message = $"Sorry, encountered a null data while during your proccess. We will fix it any time soon.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 403;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;

                    case EntryPointNotFoundException:
                        mvcErrorModel.Message = $"Sorry, couldn't find the page you've searched.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 404;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;

                    default:
                        mvcErrorModel.Message = $"Sorry, there has been an unexpected error. We will fix it any time soon";
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                }
                result.ViewData = new ViewDataDictionary(_metadataProvider, context.ModelState);
                result.ViewData.Add("MvcErrorModel", mvcErrorModel);
                context.Result = result;
            }
        }
    }
}
