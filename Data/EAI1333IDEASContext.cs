using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using IDEASRadzenGrid.Models.EAI1333_IDEAS;

namespace IDEASRadzenGrid.Data
{
    public partial class EAI1333_IDEASContext : DbContext
    {
        public EAI1333_IDEASContext()
        {
        }

        public EAI1333_IDEASContext(DbContextOptions<EAI1333_IDEASContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.OnModelBuilding(builder);
        }

        public DbSet<IDEASRadzenGrid.Models.EAI1333_IDEAS.TmStmtRun> TmStmtRuns { get; set; }
    }
}