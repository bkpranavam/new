using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using IDEASRadzenGrid.Data;

namespace IDEASRadzenGrid.Controllers
{
    public partial class ExportEAI1333_IDEASController : ExportController
    {
        private readonly EAI1333_IDEASContext context;
        private readonly EAI1333_IDEASService service;

        public ExportEAI1333_IDEASController(EAI1333_IDEASContext context, EAI1333_IDEASService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/EAI1333_IDEAS/tmstmtruns/csv")]
        [HttpGet("/export/EAI1333_IDEAS/tmstmtruns/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmStmtRunsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTmStmtRuns(), Request.Query), fileName);
        }

        [HttpGet("/export/EAI1333_IDEAS/tmstmtruns/excel")]
        [HttpGet("/export/EAI1333_IDEAS/tmstmtruns/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTmStmtRunsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTmStmtRuns(), Request.Query), fileName);
        }
    }
}
