using Application.BusinessLogic.DietForPatients;
using Application.BusinessLogic.DietSaleses;
using Application.Core;
using Application.DTOs.ReportsClassesDTO.Reports;
using Application.Services.Reports;
using MediatR;

namespace Application.Services
{
    public class ReportService
    {
        private readonly IMediator _mediator;
        public ReportService(IMediator mediator) 
        {
            _mediator = mediator;
        }

        public async Task<Result<IReport>> CreateReport(ReportTypeEnum reportType, int? dietitianId = null, int? dietId = null)
        {
            switch (reportType)
            {
                case ReportTypeEnum.MeasurementsHistoryReport:
                    {
                        var mhdto = new MeasurementsHistoryDTO();
                        return Result<IReport>.Success(new MeasurementsHistoryReport(mhdto));
                    }

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
                            return Result<IReport>.Failure($"Failed to get DietSales data. Reason: {dsdtoResult.Error}");
                        }
                    }

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
                            return Result<IReport>.Failure($"Failed to get DietSales data. Reason: {dfpdtoResult.Error}");
                        }
                    }

                default:
                    throw new ArgumentException("Unsupported report type");
            }
        }

        public enum ReportTypeEnum
        {
            MeasurementsHistoryReport,
            DietSalesReport,
            DietForPatientToDocumentReport
        }
    }
}
