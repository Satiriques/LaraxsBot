using LaraxsBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Common
{
    public class ManagerResult : IManagerResult
    {
        public static ManagerResult Default = new ManagerResult();
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public static IManagerResult FromErrorMessage(string message)
        {
            return new ManagerResult() { Message = message, Success = false };
        }
    }
}
