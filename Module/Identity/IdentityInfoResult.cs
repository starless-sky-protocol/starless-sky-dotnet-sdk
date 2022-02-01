using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.API;
using StarlessSky.Core.Entity;

namespace StarlessSky.Core.Module
{
    public class IdentityInfoResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }
        public OperationMessage[] Messages { get; set;  }

        public IdentityInfo PublicIdentityInfo { get; set; }
    }
}
