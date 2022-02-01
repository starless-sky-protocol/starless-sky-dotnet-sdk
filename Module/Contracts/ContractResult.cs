using StarlessSky.Core.API;
using StarlessSky.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public class ContractResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }
        public OperationMessage[] Messages { get; set; }

        public string Id { get; set; }
        public DateTime Issued { get; set; }
        public TimeSpan Expires { get; set; }
        public DateTime GetExpirationDate() => Issued + Expires;
        public PublicKeyBinaryEndpoint Pair { get; set; }
        public ContractSignInformation SignInformation { get; set; }
    }
}
