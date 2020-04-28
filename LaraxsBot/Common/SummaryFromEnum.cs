using System;
using System.Collections.Generic;
using System.Text;

namespace LaraxsBot.Common
{
    public class SummaryFromEnum : Attribute
    {
        public SummaryEnum Enum { get; }

        public SummaryFromEnum(SummaryEnum @enum)
        {
            Enum = @enum;
        }
    }
}
