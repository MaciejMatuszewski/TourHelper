using System;
using TourHelper.Base.Manager.Calculators;

namespace TourHelper.Manager.Calculators
{
    public class CorrectionFilter:IFilter<double>
    {

        public long Counter { get; private set; }
        public double BaseValue { get; private set; }
        public double CutOffLimit { get; set; }
        private IFilter<double> _internalFilter;
        private double _base;

        public CorrectionFilter(double _cutOff) {

            CutOffLimit = _cutOff;
        }
        public CorrectionFilter(double _cutOff,IFilter<double> _filter):this(_cutOff)
        {
            _internalFilter = _filter;
        }
        public double GetValue(double v)
        {

            if (_internalFilter!=null)
            {
                v = _internalFilter.GetValue(v);
            }

            if (Math.Abs(v) <= CutOffLimit)
            {

                _base += v;                
                Counter++;

                BaseValue = _base/Counter;
            }
            return v-BaseValue;
        }
    }
}
