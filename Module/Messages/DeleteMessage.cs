using StarlessSky.Core.Entity;
using StarlessSky.Core.Network;
using StarlessSky.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public partial class MessageProvider
    {
        public EmptyResponseResult DeleteMessage(string privateKey, string messageId)
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Delete,
                url: this.NetworkInstance.GetSecurePath("messages/" + messageId),
                data: new
                {
                    private_key = privateKey
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
