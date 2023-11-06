using Application.DTOs.AdminDTO;

namespace Application.Core
{
    /// <summary>
    /// Reprezentuje wynik aktualizacji admina z dodatkową wartością generyczną.
    /// </summary>
    /// <typeparam name="T">Typ dodatkowej wartości zwracanej w odpowiedzi.</typeparam>
    public class AdminUpdateDTO<T>
    {
        /// <summary>
        /// Pobiera lub ustawia zaktualizowane dane admina.
        /// </summary>
        public AdminDTO UpdatedAdmin { get; set; }

        /// <summary>
        /// Pobiera lub ustawia wartość wskazującą, czy operacja była udana.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Pobiera lub ustawia wartość generyczną zwróconą w odpowiedzi.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Pobiera lub ustawia komunikat błędu zwrócony w odpowiedzi.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Tworzy nową instancję klasy <see cref="AdminUpdateDTO{T}"/> reprezentującą udaną operację.
        /// </summary>
        /// <param name="value">Wartość generyczna do zwrócenia.</param>
        /// <param name="updatedAdmin">Opcjonalnie, zaktualizowane dane admina.</param>
        /// <returns>Obiekt DTO reprezentujący udaną operację.</returns>
        public static AdminUpdateDTO<T> Success(T value, AdminDTO updatedAdmin = null) => new AdminUpdateDTO<T> { IsSuccess = true, Value = value, UpdatedAdmin = updatedAdmin };

        /// <summary>
        /// Tworzy nową instancję klasy <see cref="AdminUpdateDTO{T}"/> reprezentującą nieudaną operację.
        /// </summary>
        /// <param name="error">Komunikat błędu do zwrócenia.</param>
        /// <returns>Obiekt DTO reprezentujący nieudaną operację.</returns>
        public static AdminUpdateDTO<T> Failure(string error) => new AdminUpdateDTO<T> { IsSuccess = false, Error = error };
    }
}
