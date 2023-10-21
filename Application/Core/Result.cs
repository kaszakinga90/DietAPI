using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    /// <summary>
    /// Reprezentuje wynik aktualizacji pacjenta z dodatkową wartością generyczną.
    /// </summary>
    /// <typeparam name="T">Typ dodatkowej wartości zwracanej w odpowiedzi.</typeparam>
    public class PatientUpdateDTO<T>
    {
        /// <summary>
        /// Pobiera lub ustawia zaktualizowane dane pacjenta.
        /// </summary>
        public PatientDTO UpdatedPatient { get; set; }

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
        /// Tworzy nową instancję klasy <see cref="PatientUpdateDTO{T}"/> reprezentującą udaną operację.
        /// </summary>
        /// <param name="value">Wartość generyczna do zwrócenia.</param>
        /// <param name="updatedPatient">Opcjonalnie, zaktualizowane dane pacjenta.</param>
        /// <returns>Obiekt DTO reprezentujący udaną operację.</returns>
        public static PatientUpdateDTO<T> Success(T value, PatientDTO updatedPatient = null) => new PatientUpdateDTO<T> { IsSuccess = true, Value = value, UpdatedPatient = updatedPatient };

        /// <summary>
        /// Tworzy nową instancję klasy <see cref="PatientUpdateDTO{T}"/> reprezentującą nieudaną operację.
        /// </summary>
        /// <param name="error">Komunikat błędu do zwrócenia.</param>
        /// <returns>Obiekt DTO reprezentujący nieudaną operację.</returns>
        public static PatientUpdateDTO<T> Failure(string error) => new PatientUpdateDTO<T> { IsSuccess = false, Error = error };
    }
}
