using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using IDEASRadzenGrid.Data;

namespace IDEASRadzenGrid
{
    public partial class EAI1333_IDEASService
    {
        EAI1333_IDEASContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly EAI1333_IDEASContext context;
        private readonly NavigationManager navigationManager;

        public EAI1333_IDEASService(EAI1333_IDEASContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);


        public async Task ExportTmStmtRunsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/eai1333_ideas/tmstmtruns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/eai1333_ideas/tmstmtruns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTmStmtRunsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/eai1333_ideas/tmstmtruns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/eai1333_ideas/tmstmtruns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTmStmtRunsRead(ref IQueryable<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> items);

        public async Task<IQueryable<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun>> GetTmStmtRuns(Query query = null)
        {
            var items = Context.TmStmtRuns.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTmStmtRunsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTmStmtRunGet(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);

        public async Task<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> GetTmStmtRunByEventKey(int eventkey)
        {
            var items = Context.TmStmtRuns
                              .AsNoTracking()
                              .Where(i => i.EventKey == eventkey);

  
            var itemToReturn = items.FirstOrDefault();

            OnTmStmtRunGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTmStmtRunCreated(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);
        partial void OnAfterTmStmtRunCreated(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);

        public async Task<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> CreateTmStmtRun(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun tmstmtrun)
        {
            OnTmStmtRunCreated(tmstmtrun);

            var existingItem = Context.TmStmtRuns
                              .Where(i => i.EventKey == tmstmtrun.EventKey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TmStmtRuns.Add(tmstmtrun);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tmstmtrun).State = EntityState.Detached;
                throw;
            }

            OnAfterTmStmtRunCreated(tmstmtrun);

            return tmstmtrun;
        }

        public async Task<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> CancelTmStmtRunChanges(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTmStmtRunUpdated(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);
        partial void OnAfterTmStmtRunUpdated(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);

        public async Task<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> UpdateTmStmtRun(int eventkey, IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun tmstmtrun)
        {
            OnTmStmtRunUpdated(tmstmtrun);

            var itemToUpdate = Context.TmStmtRuns
                              .Where(i => i.EventKey == tmstmtrun.EventKey)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tmstmtrun);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTmStmtRunUpdated(tmstmtrun);

            return tmstmtrun;
        }

        partial void OnTmStmtRunDeleted(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);
        partial void OnAfterTmStmtRunDeleted(IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun item);

        public async Task<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> DeleteTmStmtRun(int eventkey)
        {
            var itemToDelete = Context.TmStmtRuns
                              .Where(i => i.EventKey == eventkey)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTmStmtRunDeleted(itemToDelete);


            Context.TmStmtRuns.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTmStmtRunDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}