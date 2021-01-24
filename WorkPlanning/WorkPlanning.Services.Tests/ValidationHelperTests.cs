using System;
using System.Collections.Generic;
using WorkPlanning.DA.Models;
using WorkPlanning.Services.Helpers;
using Xunit;

namespace WorkPlanning.Services.Tests
{
    public class ValidationHelperTests
    {

        public ValidationHelperTests()
        {

        }

        [Fact]
        public void ValidateShift_ShiftOverlaps_ThrowsException()
        {
            var start = new DateTime(2021, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var end = new DateTime(2021, 1, 1, 19, 0, 0, DateTimeKind.Utc);

            var existingShifts = new List<ShiftModel>
            {
                new ShiftModel{
                Guid=Guid.NewGuid(),
                Start=new DateTime(2021, 1, 1, 17, 0, 0, DateTimeKind.Utc),
                End=new DateTime(2021, 1, 1, 17, 5, 0, DateTimeKind.Utc)
                }
            };
            var ex = Assert.Throws<Exception>(() => ValidationHelper.ValidateShift(start, end, existingShifts));
            Assert.Equal(Errors.ShiftOverlapsWithExisting, ex.Message);
        }

        [Fact]
        public void ValidateShift_ShiftIsInARow_ThrowsException()
        {
            var start = new DateTime(2021, 1, 1, 17, 0, 0, DateTimeKind.Utc);
            var end = new DateTime(2021, 1, 1, 19, 0, 0, DateTimeKind.Utc);

            var existingShifts = new List<ShiftModel>
            {
                new ShiftModel{
                Guid=Guid.NewGuid(),
                Start=new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                End=new DateTime(2021, 1, 1, 11, 5, 0, DateTimeKind.Utc)
                }
            };
            var ex = Assert.Throws<Exception>(() => ValidationHelper.ValidateShift(start, end, existingShifts));
            Assert.Equal(Errors.ShiftIsInARow, ex.Message);
        }
    }
}
