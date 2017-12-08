using System;
using Station.Common.Entities;

namespace Station.Modules.Signals.Domain.Entities
{
    public class Signal : Entity<long>
    {
        public decimal? Value { get; set; } = null;
        public int Type { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}