
using System;
using UnityEngine;

namespace TourHelper.Base.Manager.Calculators
{
    public interface IIntegralCalculator
    {
        Vector3 GetResult();
        void UpdateResult(Vector3 signal, float dt); //DateTime stamp);
    }
}
