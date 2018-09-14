
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IPositionTranslation
    {
        Vector3 GetCoordinates(Coordinates c);
    }
}
