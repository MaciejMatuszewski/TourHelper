using System;
using System.Collections;
using TourHelper.Base.Enum;
using TourHelper.Base.Manager;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager
{
    public class GpsManager : IGpsManager
    {
        private static GpsManager instance=null;
        private static readonly object key=new object();

        public static GpsManager Instance {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new GpsManager();
                    }
                    return instance;
                }
            }
        }


        public Coordinates GetCoordinates()
        {
            throw new NotImplementedException();
        }


        public bool IsReady()
        {
            throw new NotImplementedException();
        }

        public IEnumerable StartService(int timeOut)
        {
            throw new NotImplementedException();
        }

        public ServiceStatus Status()
        {
            throw new NotImplementedException();
        }
    }
}
