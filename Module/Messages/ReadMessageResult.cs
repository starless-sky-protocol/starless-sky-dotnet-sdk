using StarlessSky.Core.API;
using StarlessSky.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module.Messages
{
    public class ReadMessageResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }
        public OperationMessage[] Messages { get; set; }

        public string Id { get; set; }
        public MessageManifest Manifest { get; set; }
        public PublicKeyBinaryEndpoint Pair { get; set; }
        public MessageBody Message { get; set; }
    }
}
