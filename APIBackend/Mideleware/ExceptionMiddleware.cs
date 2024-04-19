
using System.Net;
using System.Text.Json;
using APIBackend.Error;


namespace APIBackend.Mideleware
{
    public class ExceptionMiddleware
    {
        
        public IHostEnvironment Evn { get; }
        public RequestDelegate Next { get; }
        private readonly ILogger<ExceptionMiddleware> logger;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment evn)
        {
            this.Evn = evn;
            this.logger = logger;
            this.Next = next;
        }
        public async Task Invoke(HttpContext context){
            try{
                await Next(context);
            }catch(Exception ex){
                logger.LogError(ex,ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                var Response=Evn.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);
                
                var _options=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                var json=JsonSerializer.Serialize(Response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}