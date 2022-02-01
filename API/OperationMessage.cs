using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlessSky.Core.API
{
    public enum OperationMessageLevel
    {
        Information,
        Warning,
        Error,
        Fatal
    }

    public sealed class OperationMessage
    {
        public OperationMessageLevel Level { get; private set; }
        public string Message { get; private set; }

        public OperationMessage(OperationMessageLevel level, string message)
        {
            Level = level;
            Message = message ?? throw new ArgumentNullException(message);
        }
    }
}
