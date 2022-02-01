using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.API;

namespace StarlessSky.Core.Module
{
    public partial class ContractsProvider
    {
        StarlessSkyNetworkInstance NetworkInstance { get; set; }

        public ContractsProvider(StarlessSkyNetworkInstance instance)
        {
            this.NetworkInstance = instance;
        }
    }
}
