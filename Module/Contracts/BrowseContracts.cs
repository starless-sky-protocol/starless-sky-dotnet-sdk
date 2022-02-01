using StarlessSky.Core.Entity;
using StarlessSky.Core.Module.Contracts;
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
        public BrowseContractsResult BrowseContracts(string privateKey, BrowseFolder folder, PaginationData paginationData)
        {
            var res = NetworkHandler.JSONRequest(
                method: System.Net.Http.HttpMethod.Get,
                url: this.NetworkInstance.GetSecurePath("sign"),
                data: new
                {
                    private_key = privateKey,
                    folder = folder.ToString(),
                    pagination_data = new
                    {
                        skip = paginationData.Skip,
                        take = paginationData.Take
                    }
                });

            ConsoleUtil console = new ConsoleUtil(this.NetworkInstance);

            BrowseContractsResult result = new BrowseContractsResult();
            result.Success = res.success;
            result.Messages = NetworkHandler.ReadMessages(res);

            NetworkInstance.HandleOperationResult(result);
            console.WriteOperationResponse(result);

            if (result.Success)
            {
                console.WriteLine("");
                console.WriteLine($" {"From",-16} {"Id",-24} {"Date and time",-22} {"Signed",-10} {"Message",-20}");
                console.WriteLine($" {new string('=', 16)} {new string('=', 24)} {new string('=', 22)} {new string('=', 10)} {new string('=', 20)}");

                List<BriefContractInfo> briefContracts = new List<BriefContractInfo>();
                foreach (dynamic contract in res.response.messages)
                {
                    BriefContractInfo briefInfo = new BriefContractInfo()
                    {
                          Id = contract.id,
                          Issued = (DateTime)NetworkHandler.UnixTimeStampToDateTime((double)contract.issued),
                          Message = contract.message,
                          Pair = new PublicKeyBinaryEndpoint()
                          {
                              FromPublicKey = contract.from,
                              ToPublicKey = contract.to
                          },
                          SignStatus = ContractSignInformation.ParseRawStatus(contract.sign_status)
                    };

                    briefContracts.Add(briefInfo);
                    console.WriteLine($" {briefInfo.Pair.FromPublicKey.ShortKey(16),-16} {briefInfo.Id,-24} {briefInfo.Issued,-22} {briefInfo.SignStatus,-10} {briefInfo.Message,-20}");
                }

                result.Contracts = briefContracts.ToArray();
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
