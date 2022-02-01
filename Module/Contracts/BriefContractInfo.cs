using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.Entity;

namespace StarlessSky.Core.Module
{
    public class BriefContractInfo
    {
        public string Id { get; init; }
        public DateTime Issued { get; init; }
        public PublicKeyBinaryEndpoint Pair { get; init; }
        public string Message { get; init; }
        public ContractSignStatus SignStatus { get; init; }
    }
}
