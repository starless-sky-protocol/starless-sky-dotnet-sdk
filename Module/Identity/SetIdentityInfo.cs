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
        public IdentityInfoResult SetIdentityInfo(string private_key, IdentityInfo publicIdentityInfo)
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Post,
                url: this.NetworkInstance.GetSecurePath("identity"),
                data: new {
                    private_key = private_key,
                    @public = new
                    {
                        name = publicIdentityInfo.Name,
                        biography = publicIdentityInfo.Biography
                    }
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
            }

            return result;
        }
    }
}
