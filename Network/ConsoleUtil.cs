using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.API;

namespace StarlessSky.Core.Network
{
    internal class ConsoleUtil
    {
        public StarlessSkyNetworkInstance Instance { get; set; }

        public ConsoleUtil(StarlessSkyNetworkInstance instance)
        {
            Instance = instance;
        }

        public void WriteOperationResponse(IStarlessSkyOperationResult result)
        {
            if (!Instance.Verbose) return;
            foreach(OperationMessage message in result.Messages)
            {
                switch(message.Level)
                {
                    case OperationMessageLevel.Information:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(message.Message);
                        break;
                    case OperationMessageLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(message.Message);
                        break;
                    case OperationMessageLevel.Fatal:
                    case OperationMessageLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(message.Message);
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void SkipLine()
        {
            if (!Instance.Verbose) return;
            Console.WriteLine();
        }

        public void WriteKeyValue(int padding, string key, params object[] values)
        {
            if (!Instance.Verbose) return;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new String(' ', padding * 3) + key);
            foreach(object value in values)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new String(' ', (padding + 1) * 3) + "> " + (value ?? "<null>").ToString());
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void WriteLine(string s)
        {
            if (!Instance.Verbose) return;
            Console.WriteLine(s);
        }
    }
}
