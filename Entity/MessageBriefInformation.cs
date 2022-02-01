using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Entity
{
    public class MessageBriefInformation
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsModified { get; set; }
        public PublicKeyBinaryEndpoint Pair { get; set; }
        public MessageBody Message { get; set; }

        public MessageBriefInformation(string id, DateTime createdAt, bool isModified, PublicKeyBinaryEndpoint pair, MessageBody message)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            CreatedAt = createdAt;
            IsModified = isModified;
            Pair = pair;
            Message = message;
        }
    }
}
