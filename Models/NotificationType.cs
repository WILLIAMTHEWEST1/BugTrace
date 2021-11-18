using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class NotificationType
    {
        public int Id { get; set; }

        [DisplayName("Notification Type")]
        public string Notification { get; set; }
        
    }
}
