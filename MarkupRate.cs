using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrightTireEstimator
{
    /// <summary>
    /// Definition of a range to change markup rates
    /// </summary>
    public class MarkupRate
    {
        //Lower range for Markup Margin
        public decimal LowerRange { get; set; }
        //Upper range for Markup Margin
        public decimal UpperRange { get; set; }
        //Markup rate for given range
        public decimal Markup { get; set; }

        public MarkupRate(decimal lower, decimal upper, decimal rate)
        {
            LowerRange = lower;
            UpperRange = upper;
            Markup = rate;
        }
    }
}
