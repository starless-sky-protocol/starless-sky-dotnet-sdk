using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Entity
{
    public class MessageManifest
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsModified { get; set; }
        public string Blake3Digest { get; set; }
        public string MessageSize { get; set; }
    }
}
