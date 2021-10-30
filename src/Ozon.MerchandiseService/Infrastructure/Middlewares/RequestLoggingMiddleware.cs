using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Ozon.MerchandiseService.Infrastructure.Middlewares
{
    internal sealed class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (! await IsGrpcRequest(context))
            {
                await LogRequest(context);
            }
            await _next(context);
        }

        private Task<bool> IsGrpcRequest(HttpContext context)
        {
            try
            {
                if (!String.IsNullOrEmpty(context.Request.ContentType))
                    return Task.FromResult(context.Request.ContentType.Equals("application/protobuf"));
                else
                    _logger.LogInformation("ContentType: empty");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось определить тип контента запроса");
            }
            
            return Task.FromResult(false);
        }
        private async Task LogRequest(HttpContext context)
        {
            try
            {
                StringBuilder st = new StringBuilder();
                foreach (var variable in context.Request.Headers)
                {
                    st.Append($"{variable.Key}: {variable.Value}\n");
                }
                _logger.LogInformation($"Request header: {st}");

                st.Clear();
                foreach (var variable in context.Request.Query)
                {
                    st.Append($"{variable.Key}: {variable.Value}\n");
                }
                _logger.LogInformation($"Request query: {st}");
                
                long contentLength = context.Request.ContentLength ?? 0;

                if (contentLength > 0)
                {
                    context.Request.EnableBuffering();

                    var buffer = new byte[contentLength];
                    await context.Request.Body.ReadAsync(buffer);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    
                    _logger.LogInformation($"Request body: {bodyAsText}");
                    
                    context.Request.Body.Position = 0;
                }
                else
                {
                    _logger.LogInformation("Request body: empty");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось залогировать тело запроса");
            }
        }
    }
}