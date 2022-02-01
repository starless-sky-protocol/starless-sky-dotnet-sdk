using StarlessSky.Core.Network;
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
        public GenerateKeyPairResult GenerateKeyPair()
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Get,
                url: this.NetworkInstance.GetSecurePath("identity/generate-keypair"),
                data: new { }
            );

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            GenerateKeyPairResult result = new GenerateKeyPairResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                result.GeneratedKeyPair = new Entity.KeyPair()
                {
                    PrivateKey = res.response.private_key,
                    PublicKey = res.response.public_key,
                    Host = res.response.host
                };

                console.WriteKeyValue(0, "Public key", result.GeneratedKeyPair.PublicKey);
                console.WriteKeyValue(0, "Private key", result.GeneratedKeyPair.PrivateKey);
            }

            return result;
        }
    }
}
