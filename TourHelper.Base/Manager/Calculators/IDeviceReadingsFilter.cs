using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IDeviceReadingsFilter
    {
        void UpdateFilter();
        Quaternion GetOrientation();

    }
}
