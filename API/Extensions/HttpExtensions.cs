using System.Text.Json;

namespace API.Extensions
{
    // Klasa rozszerzeń dla HttpResponse.
    public static class HttpExtensions
    {
        /// <summary>
        /// Dodaje niestandardowy nagłówek 'Pagination' do odpowiedzi HTTP.
        /// Za jego pośrednictwem przekazywane są informacje do klienta o paginacji
        /// </summary>
        /// <param name="response">Obiekt odpowiedzi HTTP, do którego dodawany jest nagłówek</param>
        /// <param name="currentPage">Aktualna strona z wynikami</param>
        /// <param name="totalPages">Liczba wszystkich stron wyników</param>
        /// <param name="totalCount">Liczba wszystkich elementów</param>
        /// <param name="pageSize">Liczba elementów do wyświetlenia na stronie</param>
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int totalPages, int totalCount, int pageSize)
        {
            // Obiekt, który posiada informacje na temat paginacji
            var paginationHeader = new
            {
                currentPage,
                totalPages,
                totalCount,
                pageSize
            };

            // Obiekt jest serializowany do JSON i dodawany jest jako wartość nagłówka Pagination
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));

            // Dodawany jest nagłówek Access-Control-Expose-Headers aby upewnić się, że niestandardowy nagłówek jest dostępny dla klienta
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
} 