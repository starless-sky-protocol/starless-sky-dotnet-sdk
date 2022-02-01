using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.Entity
{
    public struct PublicKeyBinaryEndpoint
    {
        public string? FromPublicKey { get; set; }
        public string ToPublicKey { get; set; }

        public PublicKeyBinaryEndpoint(string fromPublicKey, string toPublicKey)
        {
            FromPublicKey = fromPublicKey ?? throw new ArgumentNullException(nameof(fromPublicKey));
            ToPublicKey = toPublicKey ?? throw new ArgumentNullException(nameof(toPublicKey));
        }
    }
}
