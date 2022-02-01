using StarlessSky.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public partial class ServerProvider
    {
        public PingResult Ping()
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Get,
                url: this.NetworkInstance.GetSecurePath("ping"),
                data: new { }
            );

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            PingResult result = new PingResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                result.StarlessSkyNetworkVersion = res.response.sls_server_version.ToString();
                result.PHPVersion = res.response.php_version.ToString();
                result.OperatingSystem = res.response.operating_system.ToString();
                result.ServerInfo = new Entity.ServerInformation()
                {
                    AllowMessageDeletion = res.response.server_info.allow_message_deletion,
                    AllowMessageEdit = res.response.server_info.allow_message_edit,
                    AllowNotIdentifiedSenders = res.response.server_info.allow_not_identified_senders,
                    MessageMaxSize = res.response.server_info.message_max_size,
                    SignMaxExpiration = TimeSpan.FromSeconds((double)res.response.server_info.sign_max_expiration),
                    SignMessageMaxSize = res.response.server_info.sign_message_max_size
                };
            }

            return result;
        }
    }
}
