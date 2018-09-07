
using System;
using System.Collections;

namespace TourHelper.Logic
{
    public class MeanFilter
    {
        
        private ArrayList tabOfElements;
        private int counter = 0;
        public int FilterRange { get; set; }
        public MeanFilter(int number)
        {
            FilterRange = number;
            tabOfElements = new ArrayList();
        }
        private void IncCounter()
        {
            counter++;
            counter %= FilterRange;
        }
        public double GetValue(double e)
        {

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
