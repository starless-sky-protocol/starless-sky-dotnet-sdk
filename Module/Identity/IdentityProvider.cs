using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.API;

namespace StarlessSky.Core.Module
{
    public partial class IdentityProvider
    {
        StarlessSkyNetworkInstance NetworkInstance { get; set; }

        public IdentityProvider(StarlessSkyNetworkInstance instance)
        {
            this.NetworkInstance = instance;
        }
    }
}
