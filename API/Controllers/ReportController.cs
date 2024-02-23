﻿using Application.CQRS.ReportTemplates;
using Application.DTOs.ReportTemplateDTO;
using Application.Services;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Application.Services.ReportService;

namespace API.Controllers
{
    // Kontroler odpowiedzialny za obsługę operacji związanych z raportami.
    public class ReportController : BaseApiController
    {
        private readonly DietContext _context;
        private readonly ReportService _reportService;

        // Konstruktor inicjalizujący kontroler z kontekstem bazy danych i serwisem raportów.
        public ReportController(DietContext context, ReportService reportService, IMediator mediator) : base(mediator)
        {
            _context = context;
            _reportService = reportService;
        }

        // Metoda do pobierania listy szablonów raportów.
        [HttpGet("reportTemplates")]
        public async Task<IActionResult> GetReportTemplates()
        {
            var result = await _mediator.Send(new ReportTemplatesList.Query());
            return HandleResult(result);
        }

        // Metoda do pobierania podglądu szablonów raportów.
        [HttpGet("reportTemplates/reportTemplatePreview")]
        public async Task<IActionResult> GetReportTemplatesPreview()
        {
            var result = await _mediator.Send(new ReportTemplatesPreviewList.Query());
            return HandleResult(result);
        }

        // Metoda do generowania raportu na podstawie podanych parametrów.
        // Parametry obejmują ID szablonu, typ raportu oraz opcjonalne ID dietetyka, diety, pacjenta oraz daty rozpoczęcia i zakończenia.
        [HttpGet("generate/{templateId}/{reportType}/{dieticianId}/{dietId}/{patientId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GenerateReport(int templateId, ReportTypeEnum reportType, int? dieticianId = null, int? dietId = null,
                                                        int? patientId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var template = _context.ReportTemplatesDb.Find(templateId);

            if (template == null)
            {
                return NotFound("Report template nie został znaleziony.");
            }

            try
            {
                var reportServiceResult = await _reportService.CreateReport(reportType, dieticianId, dietId, patientId, startDate, endDate);

                if (reportServiceResult.IsSucces)
                {
                    var reportService = reportServiceResult.Value;
                    var reportContent = reportService.GenerateReport();

                    return Ok(reportContent);
                }
                else
                {
                    return BadRequest($"Raport nie mógł zostać wygenerowany. Powód: {reportServiceResult.Error}");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                return StatusCode(500, "Internal server error");
            }
        }

        // Metoda do usuwania szablonu raportu.
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReportTemplate(int id, ReportTemplateDeleteDTO reportTemplate)
        {
            var command = new ReportTemplateDelete.Command
            {
                Id = id,
                ReportTemplateDeleteDTO = reportTemplate,
            };
            return HandleResult(await _mediator.Send(command));
        }
    }
}