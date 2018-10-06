
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Manager.Calculators
{
    public class UTMLocalCoordinates : IPositionTranslation
    {

        public ICoordinatesConverter Transformation { get; }
        private double[] originCoor;
        private Coordinates origin;
        public UTMLocalCoordinates(Coordinates origin)
        {
            originCoor = new double[2];
            Transformation = new TMConverter();
            Origin = origin;
        }

        public Coordinates Origin
        {
            get
            {
                return origin;
            }
            set
            {
                originCoor = Transformation.ConvertCoordinates(value);
                origin = value;
            }
        }


        public Vector3 GetCoordinates(Coordinates c)
        {
            double[] target = Transformation.ConvertCoordinates(c);

            return new Vector3((float)(target[0]- originCoor[0]),0, (float)(target[1] - originCoor[1]));
        }


       
    }
}
