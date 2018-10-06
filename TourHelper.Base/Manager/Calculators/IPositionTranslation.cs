
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IPositionTranslation
    {
        Coordinates Origin { get; set; }
        Vector3 GetCoordinates(Coordinates c);
    }
}
