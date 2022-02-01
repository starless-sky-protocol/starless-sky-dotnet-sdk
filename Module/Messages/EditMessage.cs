using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.Entity;
using StarlessSky.Core.Network;

namespace StarlessSky.Core.Module
{
    public partial class MessageProvider
    {
        public SendMessageResult EditMessage(string fromPrivateKey, string toPublicKey, string messageId, MessageBody newMessage)
        {
            var res = NetworkHandler.JSONRequest(
                method: System.Net.Http.HttpMethod.Put,
                url: this.NetworkInstance.GetSecurePath("messages/" + messageId),
                data: new
                {
                    public_key = toPublicKey,
                    private_key = fromPrivateKey,
                    message = new
                    {
                        content = newMessage.Content,
                        subject = newMessage.Subject
                    }
                });

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            SendMessageResult result = new SendMessageResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                result.TransactionPair = new Entity.PublicKeyBinaryEndpoint(res.response.pair.from.ToString(), res.response.pair.to.ToString());
                result.Id = res.response.id.ToString();
                result.MessageSize = res.response.message_length.ToString();
                result.Blake3Digest = res.response.message_blake3_digest.ToString();

                console.WriteLine("Message ID: " + result.Id);
            }

            return result;
        }
    }
}
