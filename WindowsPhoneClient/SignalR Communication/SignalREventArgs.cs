using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneClient
{
    public class SignalREventArgs : EventArgs
    {
        // Custom args.
        public string ChatMessageFromServer { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public CustomClass CustomObject { get; set; }
    }
}
