using Application.Core;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    /// <summary>
    /// Middleware obsługujący wyjątki, które pojawiają się podczas przetwarzania żądania.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="ExceptionMiddleware"/>.
        /// </summary>
        /// <param name="next">Następny middleware w potoku.</param>
        /// <param name="logger">Logger używany do rejestrowania informacji o wyjątkach.</param>
        /// <param name="env">Określa środowisko, w którym działa aplikacja (np. produkcja, rozwój).</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Metoda obsługująca żądanie i przechwytująca wyjątki.
        /// </summary>
        /// <param name="context">Kontekst żądania HTTP.</param>
        /// <returns>Zadanie reprezentujące operację asynchroniczną.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Rejestruje informacje o wyjątku.
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Tworzy odpowiedź w zależności od środowiska.
                var response = _env.IsDevelopment()
                    ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new AppException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
