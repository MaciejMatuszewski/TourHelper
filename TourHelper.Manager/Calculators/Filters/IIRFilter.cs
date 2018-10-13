using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourHelper.Base.Manager.Calculators;

namespace TourHelper.Manager.Calculators
{
    public class IIRFilter : IFilter<double>
    {
        private double[] a;
        private double[] b;
        private Queue<double> _dataX, _dataY;
        private IFilter<double> _internalFilter;

        /*
        a{1,-0.85408069};b{0.07295966,0.07295966} n=1 f=0.05 low
        a{1,-0.72654253};b{0.13672874,0.13672874} n=1 f=0.1 low
        a{1,-0.50952545};b{0.24523728,0.24523728} n=1 f=0.2 low
        a{1,-0.3249197};b{0.33754015,0.33754015} n=1 f=0.3 low


        a{1,-0.99981077};b{0.99990538,-0.99990538} n=1 f=0.001/16.5 high
        a{1,-0.9999048};b{0.9999524,-0.9999524} n=1 f=0.001/33 high
        a{1,-0.9999524};b{0.9999762,-0.9999762} n=1 f=0.001/66 high
        a{1,-0.9999762};b{0.9999881,-0.9999881} n=1 f0.001/132 high
        */


        public IIRFilter(double[] _a, double[] _b)
        {
            a = _a;
            b = _b;

            _dataX = new Queue<double>(b.Length);
            _dataY = new Queue<double>(a.Length - 1);
        }
        public IIRFilter(double[] _a, double[] _b, IFilter<double> _filter) : this(_a, _b)
        {
            _internalFilter = _filter;
        }

        public double GetValue(double v)
        {
            IEnumerator<double> xE, yE;
            double _sumX = 0, _sumY = 0;

            if (_dataX.Count == b.Length)
            {
                _dataX.Dequeue();
            }

            if (!(_internalFilter==null))
            {
                v = _internalFilter.GetValue(v);
            }
            

            _dataX.Enqueue(v);

            xE = _dataX.GetEnumerator();
            yE = _dataY.GetEnumerator();

            for (int i = a.Length - 1; i > 0; i--)
            {
                if (yE.MoveNext())
                {
                    _sumY += yE.Current * a[i];
                }

            }

            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (xE.MoveNext())
                {
                    _sumX += xE.Current * b[i];
                }

            }

            double _y = (_sumX - _sumY) / a[0];

            if (_dataY.Count == a.Length - 1)
            {
                _dataY.Dequeue();
            }
            _dataY.Enqueue(_y);

            return _y;
        }
    }
}

