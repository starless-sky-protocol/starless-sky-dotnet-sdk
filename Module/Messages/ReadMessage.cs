using StarlessSky.Core.Entity;
using StarlessSky.Core.Module.Messages;
using StarlessSky.Core.Network;
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
        public ReadMessageResult ReadMessage(string privateKey, string messageId)
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Get,
                url: this.NetworkInstance.GetSecurePath("messages/" + messageId),
                data: new
                {
                    private_key = privateKey
                }
            );

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            ReadMessageResult result = new ReadMessageResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                result.Id = res.response.id.ToString();
                result.Manifest = new Entity.MessageManifest()
                {
                    Blake3Digest = res.response.manifest.message_blake3_digest,
                    CreatedAt = (DateTime)NetworkHandler.UnixTimeStampToDateTime((double)res.response.manifest.created_at),
                    UpdatedAt = NetworkHandler.UnixTimeStampToDateTime((double)res.response.manifest.updated_at),
                    IsModified = (bool)res.response.manifest.is_modified,
                    MessageSize = res.response.size.ToString()
                };
                result.Pair = new PublicKeyBinaryEndpoint()
                {
                    FromPublicKey = res.response.pair.from.ToString(),
                    ToPublicKey = res.response.pair.to.ToString()
                };
                result.Message = new MessageBody()
                {
                    Content = res.response.message.content.ToString(),
                    Subject = res.response.message.subject.ToString()
                };

                console.SkipLine();
                console.WriteKeyValue(0, "Message SkyId", result.Id);

                console.WriteKeyValue(0, "Manifest");
                console.WriteKeyValue(1, "Created at", result.Manifest.CreatedAt);
                console.WriteKeyValue(1, "Updated at", result.Manifest.UpdatedAt);
                console.WriteKeyValue(1, "Is modified", result.Manifest.IsModified);
                console.WriteKeyValue(1, "Blake 3 Digest", result.Manifest.Blake3Digest);
                console.WriteKeyValue(1, "Size", result.Manifest.MessageSize);

                console.WriteKeyValue(0, "Endpoints");
                console.WriteKeyValue(1, "From", result.Pair.FromPublicKey);
                console.WriteKeyValue(1, "To", result.Pair.ToPublicKey);

                console.WriteKeyValue(0, "Message");
                console.WriteKeyValue(1, "Subject", result.Message.Subject);
                console.WriteKeyValue(1, "Content", result.Message.Content);
            }

            return result;
        }
    }
}
