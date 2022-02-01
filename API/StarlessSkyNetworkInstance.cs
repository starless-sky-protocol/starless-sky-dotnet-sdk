using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

#nullable enable

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

        public StarlessSky.Core.Module.PingResult? Ping()
        {
            try
            {
                return new StarlessSky.Core.Module.ServerProvider(this).Ping();
            } catch (Exception)
            {
                return null;
            }
        }

        public StarlessSkyNetworkInstance(string host) : this(new Uri(host), true) { }

        public StarlessSkyNetworkInstance(Uri host) : this(host, true) { }

        public StarlessSkyNetworkInstance(Uri host, bool throwOnConnectionError)
        {
            UsingSecureLayer = host.Scheme.ToLower() == "https";
            StarlessSkyHost = host;
            Module.PingResult? ping = this.Ping();
            if((ping == null || !ping!.Success) && throwOnConnectionError)
            {
                throw new ArgumentException("Cannot access " + host.DnsSafeHost);
            }
        }
    }
}
