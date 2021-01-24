using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkPlanning.DA.Models
{
    [Table("WorkerShifts", Schema = "workPlanning")]
    public class ShiftModel : BaseEntity
    {
        public Guid WorkerId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        #region Navigation

        /// <summary>
        /// Worker
        /// </summary>
        [ForeignKey(nameof(WorkerId))]
        public WorkerModel Worker { get; set; }

        #endregion Navigation
    }
}
