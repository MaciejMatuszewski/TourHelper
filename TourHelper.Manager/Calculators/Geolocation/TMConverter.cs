using System;
using TourHelper.Base.Manager.Calculators;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Manager.Calculators
{
    public class TMConverter : ICoordinatesConverter
    {
        public IElipsoid Elipsoide { get; set; }
        public IProjection Projection { get; set; }
        public TMConverter()
        {
            Elipsoide = new WGS84Elipse();
            Projection = new UTMProjection();
        }

        public double[] ConvertCoordinates(Coordinate c)
        {

            double x, y;
            double[] r = this.EquationCoefficients(c);
            double[] result; 
            double dLonR;
 
            dLonR = MathTools.rad(c.Longitude -Projection.LonOfTrueOrigin(c));
            x = (Projection.EastingOfTrueOrigin(c) + r[4] * dLonR + r[5] * Math.Pow(dLonR, 3) +
                r[6] * Math.Pow(dLonR, 6));//Easting

            y = (r[0]+r[1]*Math.Pow(dLonR,2)+r[2] * Math.Pow(dLonR, 4) +
                r[3]*Math.Pow(dLonR, 5)); //Northing
            result = new double[2];
            result[0] = x;
            result[1] = y;
                
            return result;
        }

        //---------Main Parameters

        private double V(Coordinate c)
        {
            double temp,a,b;

            a = Elipsoide.GetSemiMajor();
            b = Elipsoide.GetEccentricity();

            temp = Elipsoide.GetSemiMajor() * Projection.ScaleFactor(c) *
                Math.Pow((1 - Elipsoide.GetEccentricity() *
                Math.Pow(Math.Sin(MathTools.rad(c.Latitude)), 2)), (-0.5));
            return temp;
        }
        private double Ro(Coordinate c)
        {
            double temp;
            temp = Elipsoide.GetSemiMajor() * Projection.ScaleFactor(c) *
                (1-Elipsoide.GetEccentricity())*
                Math.Pow((1 - Elipsoide.GetEccentricity() *
                Math.Pow(Math.Sin(MathTools.rad(c.Latitude)), 2)), (-1.5));
            return temp;
        }
        private double SqEta(Coordinate c)
        {
            double temp;
            temp =  V(c)/Ro(c)-1;
            return temp;
        }
        private double M(Coordinate c)
        {
            double n, n2, n3, dLatNeg, dLatPos, temp;
            dLatNeg = MathTools.rad(c.Latitude - Projection.LatOfTrueOrigin(c));
            dLatPos= MathTools.rad(c.Latitude + Projection.LatOfTrueOrigin(c));
            n = (Elipsoide.GetSemiMajor() - Elipsoide.GetSemiMinor()) /
                (Elipsoide.GetSemiMajor() + Elipsoide.GetSemiMinor());
            n2 =Math.Pow(n, 2);
            n3 =Math.Pow(n, 3);

            temp = Elipsoide.GetSemiMinor()* Projection.ScaleFactor(c) *((1 + n + 5 * n2 / 4 + 5 * n3 / 4) * dLatNeg - 
                (3 * n + 3 * n2 + 21 * n3 / 8) * Math.Sin(dLatNeg) * Math.Cos(dLatPos) +
                ( 15* n2/8 + 15 * n3 / 8) * Math.Sin(2*dLatNeg) * Math.Cos(2*dLatPos)-
                (35 * n3 / 24) * Math.Sin(3 * dLatNeg) * Math.Cos(3 * dLatPos));
            return temp;
        }
        private double[] EquationCoefficients(Coordinate c)
        {
            double[] coef=new double[7];
            double latR = MathTools.rad(c.Latitude);
            coef[0] = M(c)+ Projection.NorthingOfTrueOrigin(c);

            coef[1] = 0.5 * V(c) * Math.Sin(latR) *Math.Cos(latR);

            coef[2] =  (V(c)/24)* Math.Sin(latR) * Math.Pow(Math.Cos(latR),3)*
                (5-Math.Pow(Math.Tan(latR), 2)+9*SqEta(c));

            coef[3] = (V(c) / 720) * Math.Sin(latR) * Math.Pow(Math.Cos(latR), 5) *
                (61- 58*Math.Pow(Math.Tan(latR), 2) + Math.Pow(Math.Tan(latR), 4));

            coef[4] = V(c) * Math.Cos(latR);

            coef[5] = (V(c) / 6) *  Math.Pow(Math.Cos(latR), 3)*
                (V(c)/Ro(c)- Math.Pow(Math.Tan(latR), 2));

            coef[6] = (V(c) / 120) * Math.Pow(Math.Cos(latR), 5) *
                (5 - 18*Math.Pow(Math.Tan(latR), 2)+ Math.Pow(Math.Tan(latR), 4)+
                14*SqEta(c)- 58*Math.Pow(Math.Tan(latR), 2)*SqEta(c));

            return coef;
        }

    }
}
