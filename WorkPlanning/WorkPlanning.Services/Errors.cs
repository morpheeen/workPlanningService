using System;
using System.Collections.Generic;
using System.Text;

namespace WorkPlanning.Services
{
    public static class Errors
    {
        public static string InvalidWorkerId => "Worker id is invalid";
        public static string StartShouldBeLowerThanEnd => "Start should be lower than End";
        public static string ShiftSpansOver24Hours => "Shift spans over 24 hours.";

        public static string WorkerNotFound => "Worker not found";
        public static string ShiftOverlapsWithExisting => "Shift overlaps with existing shift";
        public static string ShiftIsInARow => "Shift is in a row with another one";
        public static string InvalidId => "Id is invalid";

        public static string ShiftNotFound => "Shift not found";
    }
}
