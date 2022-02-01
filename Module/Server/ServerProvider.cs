using StarlessSky.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public partial class ServerProvider
    {
        StarlessSkyNetworkInstance NetworkInstance { get; set; }

        public ServerProvider(StarlessSkyNetworkInstance instance)
        {
            this.NetworkInstance = instance;
        }
    }
}
