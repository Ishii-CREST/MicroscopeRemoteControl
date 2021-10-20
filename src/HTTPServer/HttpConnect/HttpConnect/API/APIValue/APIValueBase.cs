using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.APIValue
{
    public class APIValueBase
    {
        public object Value { get; set; }
        public int ToInt { get { int.TryParse(Value.ToString(), out int ret); return ret; } }
        public double ToDouble { get { double.TryParse(Value.ToString(),out double ret); return ret; } }
        public string ToStr { get { return Value == null ? string.Empty:Value.ToString(); } }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public int MaxLength { get; set; }
        public bool isOption { get; set; } = false;

        public virtual bool AnalyzeParam(string param)
        {
            return false;
        }

        protected bool IsInRange(double val)
        {
            if (MaxValue < val) return false;
            else if (MinValue > val) return false;
            else return true;
        }
        protected bool IsInRange(string val)
        {
            if (val.Length > MaxLength) return false;
            else return true;
        }

    }
}
