using StarlessSky.Core.API;
using StarlessSky.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public class FetchMessagesResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }
        public OperationMessage[] Messages { get; set; }

        public PaginationDataResponse PaginationData { get; set; }
        public MessageBriefInformation[] FetchedMessages { get; set; }
    }
}
