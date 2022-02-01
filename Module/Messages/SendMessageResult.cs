using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.API;
using StarlessSky.Core.Entity;

namespace StarlessSky.Core.Module
{
    public sealed class SendMessageResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }

        public OperationMessage[] Messages { get; set; }

        public PublicKeyBinaryEndpoint TransactionPair { get; set; }

        public string MessageSize { get; set; }

        public string Id { get; set; }

        public string Blake3Digest { get; set; }
    }
}
