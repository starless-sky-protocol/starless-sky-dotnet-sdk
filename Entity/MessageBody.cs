using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Entity
{
    public struct MessageBody
    {
        public string Content { get; set; }
        public string Subject { get; set; }

        public MessageBody(string content, string subject)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }
}
