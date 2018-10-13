
using System;
using System.Collections;
using TourHelper.Base.Manager.Calculators;

namespace TourHelper.Manager.Calculators
{
    public class MeanFilter:IFilter<double>
    {
        
        private ArrayList tabOfElements;
        private int counter = 0;
        public int FilterRange { get; set; }
        public IFilter<double> _internalFilter;

        public MeanFilter(int number)
        {
            FilterRange = number;
            tabOfElements = new ArrayList();
        }

        public MeanFilter(int number,IFilter<double> _filter):this(number)
        {
            _internalFilter = _filter;
        }

        private void IncCounter()
        {
            counter++;
            counter %= FilterRange;
        }

        public void SetZero()
        {
            for (int i = 0; i < tabOfElements.Count; i++)
            {
                tabOfElements[i] = 0;
            }
        }
        public double GetValue(double e)
        {
            if (_internalFilter!=null)
            {
                e = _internalFilter.GetValue(e);
            }

            double sum = 0;
            if (tabOfElements.Count < FilterRange)
            {
                tabOfElements.Add(e);
            }
            else
            {
                tabOfElements[counter]=e;
            }

            for (int i = 0; i < Math.Min(tabOfElements.Count, FilterRange); i++)
            {
                sum += (double)tabOfElements[i];
            }
           
            IncCounter();
            return sum/Math.Min(tabOfElements.Count,FilterRange);
        }
    }
}
