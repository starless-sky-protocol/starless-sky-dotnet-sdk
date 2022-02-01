using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Entity
{
    public struct PaginationData
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public PaginationData(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public static PaginationData Infinite { get => new PaginationData(0, -1); }
    }
}
