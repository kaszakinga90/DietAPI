using Application.BusinessLogic.DietForPatients;
using Application.BusinessLogic.DietSaleses;
using Application.BusinessLogic.MeasurementHistories;
using Application.Core;
using Application.Services.Reports;
using MediatR;

namespace Application.Services
{
    /// <summary>
    /// Serwis do tworzenia róznych typów raportów
    /// </summary>
    public class ReportService
    {
        private readonly IMediator _mediator;
        public ReportService(IMediator mediator) 
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tworzy raport na podstawie określonego typu i parametrów.
        /// </summary>
        /// <param name="reportType">Typ raportu do utworzenia.</param>
        /// <param name="dietitianId">Opcjonalne ID dietetyka.</param>
        /// <param name="dietId">Opcjonalne ID diety.</param>
        /// <param name="patientId">Opcjonalne ID pacjenta.</param>
        /// <param name="startDate">Opcjonalna data początkowa.</param>
        /// <param name="endDate">Opcjonalna data końcowa.</param>
        /// <returns>Wynik zawierający utworzony raport lub informację o błędzie.</returns>
        public async Task<Result<IReport>> CreateReport(ReportTypeEnum reportType, int? dietitianId = null, int? dietId = null, 
                                                                        int? patientId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            switch (reportType)
            {
                // Poleecenie utworzenia raportu: Historia pomiarów pacjenta
                case ReportTypeEnum.MeasurementsHistoryReport:
                    {
                        var mhdtoResult = await _mediator.Send(new MeasurementHistoryCreate.Command { 
                            DieticianId = (int)dietitianId,
                            PatientId = (int)patientId,
                            StartDate = (DateTime)startDate,
                            EndDate = (DateTime)endDate
                        });

                        if (mhdtoResult.IsSucces)
                        {
                            var mhdtoList = mhdtoResult.Value;
                            return Result<IReport>.Success(new MeasurementsHistoryReport(mhdtoList));
                        }
                        else
                        {
                            return Result<IReport>.Failure($"Nie można pobrać danych historii pomiarów. Powód: {mhdtoResult.Error}");
                        }
                    }

                // Poleecenie utworzenia raportu: Opracowane diety wraz z ich rozliczeniem
                case ReportTypeEnum.DietSalesReport:
                    {
                        var dsdtoResult = await _mediator.Send(new DietSalesCreateDetails.Command { DieticianId = (int)dietitianId });

                        if (dsdtoResult.IsSucces)
                        {
                            var dsdtoList = dsdtoResult.Value;
                            return Result<IReport>.Success(new DietSalesReport(dsdtoList));
                        }
                        else
                        {
                            return Result<IReport>.Failure($"Nie można pobrać danych sprzedaży. Powód: {dsdtoResult.Error}");
                        }
                    }

                // Poleecenie utworzenia raportu: Dieta wraz z harmonogramem i przepisami
                case ReportTypeEnum.DietForPatientToDocumentReport:
                    {
                        var dfpdtoResult = await _mediator.Send(new DietForPatientToDocumentCreateDetails.Command { DietId = (int)dietId });

                        if (dfpdtoResult.IsSucces)
                        {
                            var dfpdto = dfpdtoResult.Value;
                            return Result<IReport>.Success(new DietForPatientToDocumentReport(dfpdto));
                        }
                        else
                        {
                            return Result<IReport>.Failure($"Nie można pobrać danych diety dla pacjenta do eksportu pdf. Powód: {dfpdtoResult.Error}");
                        }
                    }

                default:
                    throw new ArgumentException("Niewspierany typ raportu");
            }
        }

        /// <summary>
        /// Określenie dostępnych typów raportów
        /// </summary>
        public enum ReportTypeEnum
        {
            MeasurementsHistoryReport,
            DietSalesReport,
            DietForPatientToDocumentReport
        }
    }
}