using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class AttendanceHistory
    {
        public virtual Guid Id { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime CheckedIn { get; set; }
        public virtual DateTime? CheckedOut { get; set; }
    }
}
