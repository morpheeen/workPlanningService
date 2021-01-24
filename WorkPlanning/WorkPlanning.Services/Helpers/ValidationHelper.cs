using System;
using System.Collections.Generic;
using System.Linq;
using WorkPlanning.DA.Models;

namespace WorkPlanning.Services.Helpers
{
    public static class ValidationHelper
    {
        public static void ValidateShift(DateTime start, DateTime end, ICollection<ShiftModel> existingShifts)
        {
            if (existingShifts.Any(s => start >= s.Start && end > s.Start || start < s.End && end > s.End))
            {
                //3.A worker can not work two shifts at the same time
                throw new Exception(Errors.ShiftOverlapsWithExisting);
            }
            if (existingShifts.Any(s => s.End < start.AddHours(-24)))
            {
                //4.A worker never has two shifts in a row.
                throw new Exception(Errors.ShiftIsInARow);
            }
        }
    }
}
