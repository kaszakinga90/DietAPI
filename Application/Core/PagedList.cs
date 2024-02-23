using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Core
{
    // Klasa reprezentująca stronnicowaną listę elementów.
    // Dziedziczy po klasie List<T> dodając własne właściwości do zarządzania paginacją.
    public class PagedList<T> : List<T>
    {
        // Konstruktor inicjalizujący stronnicowaną listę.
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        // Aktualna strona wyników.
        public int CurrentPage { get; private set; }
        // Całkowita liczba stron wyników.
        public int TotalPages { get; private set; }
        // Liczba elementów na stronie.
        public int PageSize { get; private set; }
        // Całkowita liczba elementów wyników.
        public int TotalCount { get; private set; }

        // Statyczna metoda do asynchronicznego tworzenia stronnicowanej listy.
        // Przyjmuje źródło danych jako IQueryable<T>, numer strony i rozmiar strony.
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); // Pobieranie całkowitej liczby elementów.
            var items = await source.Skip((pageNumber - 1) * pageSize) // Pomijanie elementów poprzednich stron.
                                     .Take(pageSize) // Pobieranie elementów dla bieżącej strony.
                                     .ToListAsync(); // Konwersja wyniku na listę.
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}