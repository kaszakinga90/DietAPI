using System.Text.Json;

namespace API.Extensions
{
    // Klasa rozszerzeń dla HttpResponse.
    public static class HttpExtensions
    {
        /// <summary>
        /// Dodaje niestandardowy nagłówek 'Pagination' do odpowiedzi HTTP.
        /// Używany do przekazywania informacji o paginacji do klienta.
        /// </summary>
        /// <param name="response">Obiekt odpowiedzi HTTP, do którego dodawany jest nagłówek.</param>
        /// <param name="currentPage">Aktualna strona wyników.</param>
        /// <param name="totalPages">Całkowita liczba stron wyników.</param>
        /// <param name="totalCount">Całkowita liczba elementów wyników.</param>
        /// <param name="pageSize">Liczba elementów na stronie.</param>
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int totalPages, int totalCount, int pageSize)
        {
            // Tworzenie obiektu z informacjami o paginacji.
            var paginationHeader = new
            {
                currentPage,
                totalPages,
                totalCount,
                pageSize
            };

            // Serializacja obiektu do formatu JSON i dodanie jako wartość nagłówka 'Pagination'.
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));

            // Dodanie nagłówka 'Access-Control-Expose-Headers' aby upewnić się, że niestandardowy nagłówek jest dostępny dla klienta.
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}