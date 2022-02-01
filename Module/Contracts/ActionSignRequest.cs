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
        public enum SignAction
        {
            Sign = 0,
            Refuse = 1
        }

        public ContractResult ActContract(string privateKey, string contractId, SignAction action)
        {
            var res = NetworkHandler.JSONRequest(
                method: System.Net.Http.HttpMethod.Post,
                url: this.NetworkInstance.GetSecurePath("sign/" + action.ToString()),
                data: new
                {
                    private_key = privateKey,
                    term = action.ToString().ToLower()
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

                console.WriteKeyValue(0, "Contract Id", result.Id);
                console.WriteKeyValue(0, "Issued", result.Issued);
                console.WriteKeyValue(0, "Expires", result.Expires);
                console.WriteKeyValue(0, "Expiration date", result.GetExpirationDate());
                console.WriteKeyValue(0, "Pair");
                console.WriteKeyValue(1, "From", result.Pair.FromPublicKey);
                console.WriteKeyValue(1, "To", result.Pair.ToPublicKey);
                console.WriteKeyValue(0, "Status");
                console.WriteKeyValue(1, "Sign status", result.SignInformation.SignStatus.ToString());
                console.WriteKeyValue(1, "Sign date", result.SignInformation.ActionDate);
            }

            return result;
        }
    }
}
