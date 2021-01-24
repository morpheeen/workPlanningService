using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkPlanning.DA.Models
{
    [Table("Workers", Schema = "workPlanning")]
    public class WorkerModel : BaseEntity
    {
        public string Name { get; set; }

        #region Navigation
        /// <summary>
        /// Related shifts
        /// </summary>
        [InverseProperty(nameof(ShiftModel.Worker))]
        public ICollection<ShiftModel> Shifts { get; set; }
        #endregion
    }
}
