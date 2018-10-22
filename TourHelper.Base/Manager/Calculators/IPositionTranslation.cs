
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IPositionTranslation
    {
        Coordinate Origin { get; set; }
        Vector3 GetCoordinates(Coordinate c);
    }
}
