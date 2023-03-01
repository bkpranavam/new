using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDEASRadzenGrid.Models.EAI1333_IDEAS
{
    [Table("tmStmtRun", Schema = "dbo")]
    public partial class TmStmtRun
    {
        [Key]
        [Required]
        public int EventKey { get; set; }

        public string Session { get; set; }

        [Required]
        public DateTime EventTime { get; set; }

        [Required]
        public string Reason { get; set; }

        public string ProcName { get; set; }

        public int? ProcVer { get; set; }

        public int? ProcNestlevel { get; set; }

        public int? ProcRunKey { get; set; }

        public string ProcessType { get; set; }

        public string ProcessKey { get; set; }

        public string StmtAction { get; set; }

        public string ErrDescr { get; set; }

        public int? EventError { get; set; }

        public int? EventRowcount { get; set; }

        public string EventIdentity { get; set; }

        public string Params { get; set; }

    }
}