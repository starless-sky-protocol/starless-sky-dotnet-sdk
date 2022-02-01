using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Entity
{
    public class ServerInformation
    {
        public bool AllowNotIdentifiedSenders { get; init; }
        public bool AllowMessageEdit { get; init; }
        public bool AllowMessageDeletion { get; init; }
        public string MessageMaxSize { get; init; }
        public string SignMessageMaxSize { get; init; }
        public TimeSpan SignMaxExpiration { get; set; }
    }
}
