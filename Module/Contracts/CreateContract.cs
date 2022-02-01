using StarlessSky.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public partial class ContractsProvider
    {
        public ContractResult CreateContract(string fromPrivateKey, string toPublicKey, string message, TimeSpan expires)
        {
            var res = NetworkHandler.JSONRequest(
                method: System.Net.Http.HttpMethod.Post,
                url: this.NetworkInstance.GetSecurePath("sign"),
                data: new
                {
                    public_key = toPublicKey,
                    private_key = fromPrivateKey,
                    message = message,
                    expires = expires.TotalSeconds
                });

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            ContractResult result = new ContractResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                result.Id = res.response.id.ToString();
                result.Issued = (DateTime)NetworkHandler.UnixTimeStampToDateTime((double)res.response.issued);
                result.Expires = TimeSpan.FromSeconds((int)res.response.expires);
                result.Pair = new Entity.PublicKeyBinaryEndpoint
                {
                    FromPublicKey = res.response.issuer.public_key,
                    ToPublicKey = res.response.signer.public_key,
                };
                result.SignInformation = new ContractSignInformation()
                {
                    ActionDate = NetworkHandler.UnixTimeStampToDateTime(res.response.status.date),
                    SignStatus = ContractSignInformation.ParseRawStatus(res.response.status.sign_status)
                };

                console.WriteLine("Contract ID: " + result.Id);
            }

            return result;
        }
    }
}
