
using APIBackend.Error;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    [Route("errors/{Code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseAPIController
    {
        public IActionResult Error(int Code)
        {
            return new ObjectResult(new APIResponce(Code));
        }
    }
}