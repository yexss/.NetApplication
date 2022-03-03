using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    {
                        ViewBag.ErrorMessage = "抱歉，您访问的页面不存在";

                        //ViewBag.Path = statusCodeResult.OriginalPath;
                        //ViewBag.QueryStr = statusCodeResult.OriginalQueryString;


                        logger.LogWarning($"发生一个404错误。路径 ={ statusCodeResult.OriginalPath }以及查询字符串为{statusCodeResult.OriginalQueryString}");
                    }break;
            }

            return View("NotFound");
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"路径{exceptionHandlerPathFeature.Path},产生了一个错误{exceptionHandlerPathFeature.Error.Message}");

            //ViewBag.ExceptionPath= exceptionHandlerPathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }
    }
}
