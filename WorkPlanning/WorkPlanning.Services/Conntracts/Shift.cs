using System;

namespace WorkPlanning.Services.Conntracts
{
    public class Shift
    {
        public Guid WorkerId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
