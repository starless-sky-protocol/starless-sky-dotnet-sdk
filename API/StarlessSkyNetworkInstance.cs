using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace StarlessSky.Core.API
{
    public class StarlessSkyNetworkInstance
    {
        public Uri StarlessSkyHost { get; set; }

        public bool Verbose { get; set; } = false;

        public bool ThrowOnErrorResponses { get; set; } = false;

        public bool UsingSecureLayer { get; private set; }

        internal void HandleOperationResult(IStarlessSkyOperationResult result)
        {
            if(result.Success == false && ThrowOnErrorResponses)
            {
                throw new StarlessSkyOperationException(result.Messages);
            }
        }

        public string GetSecurePath(string apiCall = "")
        {
            return $"https://{StarlessSkyHost.DnsSafeHost}/" + apiCall.TrimStart('/');
        }

        public bool Ping()
        {
            Ping p = new Ping();
            bool success = p.Send(StarlessSkyHost.DnsSafeHost).Status == IPStatus.Success;
            return success;
        }

        public StarlessSkyNetworkInstance(Uri host)
        {
            UsingSecureLayer = host.Scheme.ToLower() == "https";
            StarlessSkyHost = host;
            if(!Ping())
            {
                throw new ArgumentException("Cannot access " + host.DnsSafeHost);
            }
        }
    }
}
