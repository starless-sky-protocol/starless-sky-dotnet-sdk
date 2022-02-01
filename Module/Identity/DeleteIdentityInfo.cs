using StarlessSky.Core.Network;
using StarlessSky.Core.Entity;
using StarlessSky.Core.API;
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
        public EmptyResponseResult DeleteIdentityInfo(string private_key)
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Delete,
                url: this.NetworkInstance.GetSecurePath("identity"),
                data: new
                {
                    private_key = private_key
                }
            );

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            EmptyResponseResult result = new EmptyResponseResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            return result;
        }
    }
}
