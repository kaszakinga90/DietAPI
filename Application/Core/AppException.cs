namespace Application.Core
{
    /// <summary>
    /// Reprezentuje wyjątek aplikacji z niestandardową informacją zwrotną.
    /// </summary>
    public class AppException
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="AppException"/> z określonym statusem, wiadomością i opcjonalnymi szczegółami.
        /// </summary>
        /// <param name="statusCode">Kod statusu HTTP związany z wyjątkiem.</param>
        /// <param name="message">Komunikat związany z wyjątkiem.</param>
        /// <param name="details">Szczegółowe informacje związane z wyjątkiem (opcjonalne).</param>
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        /// <summary>
        /// Pobiera lub ustawia kod statusu HTTP związany z wyjątkiem.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Pobiera lub ustawia komunikat związany z wyjątkiem.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Pobiera lub ustawia szczegółowe informacje związane z wyjątkiem.
        /// </summary>
        public string Details { get; set; }
    }
}
