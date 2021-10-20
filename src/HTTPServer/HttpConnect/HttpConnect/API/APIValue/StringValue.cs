using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.APIValue
{
    public class StringValue:APIValueBase
    {
        public StringValue()
        {
            MaxLength = 4096;
        }

        public override bool AnalyzeParam(string param)
        {
            if (!IsInRange(param)) return false;
            base.Value = param;
            return true;
        }
    }
}
