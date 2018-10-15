
using System;
using System.Collections;
using System.Collections.Generic;
using TourHelper.Base.Manager.Calculators;

namespace TourHelper.Manager.Calculators
{
    public class MeanFilter:IFilter<double>
    {
        
        private Queue<double> tabOfElements;
        public int FilterRange { get; private set; }
        public IFilter<double> _internalFilter;

        public MeanFilter(int number)
        {
            FilterRange = number;
            tabOfElements = new Queue<double>(number);
        }

        public MeanFilter(int number,IFilter<double> _filter):this(number)
        {
            _internalFilter = _filter;
        }



        public void SetZero()
        {
            tabOfElements.Clear();
        }
        public double GetValue(double e)
        {
            if (_internalFilter!=null)
            {
                e = _internalFilter.GetValue(e);
            }

            double sum = 0;
            tabOfElements.Enqueue(e);

            foreach(double el in tabOfElements)
            {
                sum += el;
            }
           
            return sum/ tabOfElements.Count;
        }
    }
}
