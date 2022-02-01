using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Module
{
    public enum ContractSignStatus
    {
        Refused = 0,
        Signed = 1,
        Nothing = 3
    }

    public class ContractSignInformation
    {
        public DateTime? ActionDate { get; set; }

        public ContractSignStatus SignStatus { get; set; }

        public static ContractSignStatus ParseRawStatus(dynamic status)
        {
            if (status == null)
            {
                return ContractSignStatus.Nothing;
            }
            else
            {
                return status == true ? ContractSignStatus.Signed : ContractSignStatus.Refused;
            }
        }
    }
}
