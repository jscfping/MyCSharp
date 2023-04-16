using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6.Library.Models.MessageBoard
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
