using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using StarlessSky.Core.API;
using StarlessSky.Core.Entity;
using StarlessSky.Core.Network;

namespace StarlessSky.Core.Module
{
    public partial class MessageProvider
    {
        public FetchMessagesResult FetchMessages(string fromPrivateKey, BrowseFolder folder, PaginationData paginationData)
        {
            var res = NetworkHandler.JSONRequest(
                method: HttpMethod.Get,
                url: this.NetworkInstance.GetSecurePath("messages"),
                data: new
                {
                    private_key = fromPrivateKey,
                    folder = folder.ToString(),
                    pagination_data = new {
                        skip = paginationData.Skip,
                        take = paginationData.Take
                    }
                }
            );

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            FetchMessagesResult result = new FetchMessagesResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                console.WriteLine("");
                console.WriteLine($" {"From",-34} {"Id",-24} {"Subject",-30} {"Created at", -20}");
                console.WriteLine($" {new string('=', 34)} {new string('=', 24)} {new string('=', 30)} {new string('=', 20)}");

                List<MessageBriefInformation> briefMessages = new List<MessageBriefInformation>();
                foreach(dynamic msg in res.response.messages)
                {
                    MessageBriefInformation briefInfo = new MessageBriefInformation(
                        id: msg.id.ToString(),
                        createdAt: (DateTime)NetworkHandler.UnixTimeStampToDateTime((double)msg.created_at),
                        isModified: (bool)msg.is_modified,
                        pair: new PublicKeyBinaryEndpoint(
                            fromPublicKey: msg.from.ToString(),
                            toPublicKey: msg.to.ToString()
                            ),
                        message: new MessageBody(
                            content: msg.message.content.ToString(),
                            subject: msg.message.subject.ToString()
                        )
                    );

                    briefMessages.Add(briefInfo);
                    console.WriteLine($" {briefInfo.Pair.FromPublicKey.ShortKey(30),-34} {briefInfo.Id,-24} {briefInfo.Message.Subject.Truncate(30),-30} {briefInfo.CreatedAt.ToString("dd/MM/yyyy H:mm:ss")}");
                }

                result.FetchedMessages = briefMessages.ToArray();
                result.PaginationData = new PaginationDataResponse()
                {
                    Query = res.response.pagination_data.query,
                    Total = res.response.pagination_data.total
                };
            }

            return result;
        }
    }
}
