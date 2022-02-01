using StarlessSky.Core.API;
using StarlessSky.Core.Entity;
using StarlessSky.Core.Module.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public class BrowseContractsResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }
        public OperationMessage[] Messages { get; set; }

        public PaginationDataResponse PaginationData { get; set; }
        public BriefContractInfo[] Contracts { get; set; }
    }
}
