
using System.Collections.Generic;

namespace TourHelper.Manager.Calculators
{
    public class LinearRegression
    {
        private Queue<double> _dataX, _dataY;
        private double _baseX,_baseY,_a,_b;
        private int _range;


        public LinearRegression(int n)
        {
            _range = n;
            _dataX=new Queue<double>(n);
            _dataY= new Queue<double>(n);
            Reset();
        }

        public void Reset()
        {
            _dataX.Clear();
            _dataY.Clear();
            _baseX = 0;
            _baseY = 0;
            _a = 0;
            _b = 0;
        }
        public void Update(double x,double y)
        {
            double _meanX, _meanY;
            double _top=0, _bottom=0;


            if (_range== _dataX.Count)
            {
                double oldX = _dataX.Dequeue();
                double oldY = _dataY.Dequeue();
                _baseX -= oldX;
                _baseY -= oldY;
            }
            _dataX.Enqueue(x);
            _dataY.Enqueue(y);

            _meanX = _baseX / _dataX.Count;
            _meanY = _baseY / _dataY.Count;

            IEnumerator<double> enX = _dataX.GetEnumerator();
            IEnumerator<double> enY = _dataY.GetEnumerator();

            while (enX.MoveNext())
            {
                enY.MoveNext();

                double diffX = enX.Current-_meanX;
                double diffY = enY.Current-_meanY;

                _bottom += diffX * diffX;
                _top += diffY * diffX;
            }


            _a = _top / _bottom;
            _b = _meanY - _a* _meanX;
        }

        public double GetValue(double x)
        {
            return _a*x+_b;
        }
        public double GetTrendValue(double x)
        {
            return _a * x ;
        }
    }




}
