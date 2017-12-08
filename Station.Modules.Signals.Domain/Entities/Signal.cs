using System;
using Station.Common.Entities;

namespace Station.Modules.Signals.Domain.Entities
{
    public class Signal : CodedEntity<long>
    {
        public decimal Value { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{this.Type} от {this.Date.ToString("G")}";
        }
    }
}
