
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface ICoordinatesConverter
    {
        double[] ConvertCoordinates(Coordinate c);
    }
}
