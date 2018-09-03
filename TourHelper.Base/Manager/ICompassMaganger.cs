﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TourHelper.Base.Manager
{
    public interface ICompassMaganger:IBaseDeviceManager
    {

        double GetAngleToNorth();
    }
}
