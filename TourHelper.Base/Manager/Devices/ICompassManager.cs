using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TourHelper.Base.Manager.Devices
{
    public interface ICompassManager:IBaseDeviceManager
    {
        void CompassOn();
        void CompassOff();
        double GetAngleToNorth();
    }
}
