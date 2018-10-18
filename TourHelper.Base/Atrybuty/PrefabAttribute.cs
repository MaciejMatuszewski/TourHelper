using System;

namespace TourHelper.Base.Atrybuty
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabAttribute : Attribute {
        public string Name { get; set; }

        //public PrefabAttribute(string name ) { Name = name; }
    }

}
