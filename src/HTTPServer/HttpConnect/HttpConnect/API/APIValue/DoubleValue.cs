using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.APIValue
{
    public class DoubleValue:APIValueBase
    {
        public DoubleValue()
        {
            MinValue = double.MinValue;
            MaxValue = double.MaxValue;
        }

        public override bool AnalyzeParam(string param)
        {
            if (string.IsNullOrEmpty(param)) return true;
            if (!double.TryParse(param, out double val)) return false;
            if (!IsInRange(val)) return false;
            base.Value = val;
            return true;
        }
    }
}
