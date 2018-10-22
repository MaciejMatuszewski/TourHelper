
using System;
using TourHelper.Base.Manager.Calculators;

namespace TourHelper.Manager.Calculators
{
    public class CutOffFilter:IFilter<double>
    {
        public double CutOffLimit { get; set; }
        private IFilter<double> _internalFilter;

        public CutOffFilter(double limit) {

            CutOffLimit = limit;
        }
        public CutOffFilter(double limit,IFilter<double> _filter):this(limit)
        {
            _internalFilter = _filter;
        }
 

        public double GetValue(double v)
        {
            if (_internalFilter!=null)
            {
                v = _internalFilter.GetValue(v);
            }
            if (Math.Abs(v)<= CutOffLimit)
            {
                return 0;
            }
            return v;
        }
    }
}
