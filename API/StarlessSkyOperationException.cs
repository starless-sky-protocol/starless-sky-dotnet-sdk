using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.API
{
    public class StarlessSkyOperationException : Exception
    {
        public OperationMessage[] OperationMessages { get; private set; }

        public StarlessSkyOperationException(OperationMessage[] messages): base("An error occurred in your Starless Sky operation and the application execution needed to be stopped. Error: " + messages[0].Message)
        {
            this.OperationMessages = messages ?? throw new ArgumentNullException(nameof(messages));
        }
    }
}
