using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace IDEASRadzenGrid.Pages
{
    public partial class TmStmtRuns
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public EAI1333_IDEASService EAI1333_IDEASService { get; set; }

        protected IEnumerable<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> tmStmtRuns;

        protected RadzenDataGrid<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> grid0;
        protected bool isEdit = true;
        protected override async Task OnInitializedAsync()
        {
            tmStmtRuns = await EAI1333_IDEASService.GetTmStmtRuns();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            isEdit = false;
            tmStmtRun = new IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun();
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EAI1333_IDEASService.ExportTmStmtRunsToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "TmStmtRuns");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EAI1333_IDEASService.ExportTmStmtRunsToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
}, "TmStmtRuns");
            }
        }
        protected bool errorVisible;
        protected IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun tmStmtRun;

        protected async Task FormSubmit()
        {
            try
            {
                var result = isEdit ? await EAI1333_IDEASService.UpdateTmStmtRun(tmStmtRun.EventKey, tmStmtRun) : await EAI1333_IDEASService.CreateTmStmtRun(tmStmtRun);

            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {

        }
    }
}