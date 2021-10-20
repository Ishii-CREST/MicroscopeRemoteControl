using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.APIValue
{
    public class IntValue:APIValueBase
    {
        public IntValue()
        {
            MaxValue = int.MaxValue;
            MinValue = int.MinValue;
        }

        public override bool AnalyzeParam(string param)
        {
            if (string.IsNullOrEmpty(param)) return true;
            if (!int.TryParse(param, out int val)) return false;
            if (!IsInRange(val)) return false;
            base.Value = val;
            return true;
        }
    }
}

