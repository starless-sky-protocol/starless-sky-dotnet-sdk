using StarlessSky.Core.Network;
using StarlessSky.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public partial class IdentityProvider
    {
        public IdentityInfoResult GetIdentityInfo(string public_key)
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Get,
                url: this.NetworkInstance.GetSecurePath("identity"),
                data: new
                {
                    public_key = public_key
                }
            );

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            IdentityInfoResult result = new IdentityInfoResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                result.PublicIdentityInfo = new IdentityInfo()
                {
                    Biography = res.response.biography,
                    Name = res.response.name
                };

                console.WriteKeyValue(0, "Public key", public_key);
                console.WriteKeyValue(0, "Name", result.PublicIdentityInfo.Name);
                console.WriteKeyValue(0, "Biography", result.PublicIdentityInfo.Biography);
            }

            return result;
        }
    }
}
