using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.API
{
    public class EmptyResponseResult : IStarlessSkyOperationResult
    {
        public bool Success { get; set; }
        public OperationMessage[] Messages { get; set; }
    }
}
