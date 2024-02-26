using Microsoft.EntityFrameworkCore;

namespace Application.Core
{
    /// <summary>
    /// Klasa reprezentuje ile elementów jest wyświetlanych na jednej stronie
    /// Dziedziczy po klasie List<T> dodając własne właściwości do modyfikowania paginacji
    /// </summary>
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        // Aktualna strona wyników
        public int CurrentPage { get; private set; }
        // Całkowita liczba stron wyników
        public int TotalPages { get; private set; }
        // Liczba elementów na stronie
        public int PageSize { get; private set; }
        // Liczba wszystkich wyników
        public int TotalCount { get; private set; }

        /// <summary>
        /// Statyczna metoda do tworzenia listy elementów do wyświetlania na jednej stronie
        /// Przyjmuje dane jako IQueryable<T>, numer oraz rozmiar strony
        /// </summary>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); // Pobranie liczby wszystkich elementów
            var items = await source.Skip((pageNumber - 1) * pageSize) // Pominięcie elementów na poprzednich stronach
                                     .Take(pageSize) // Pobranie elementów dla aktualnie wyświetlanej strony
                                     .ToListAsync(); // Konwersja wyniku do listy
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}