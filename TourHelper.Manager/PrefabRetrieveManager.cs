using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TourHelper.Base.Atrybuty;

namespace TourHelper.Manager
{
    public class PrefabRetrieveManager : ClassRetrieveManager
    {
        public IEnumerable<Type> BufforedTypes { get; private set; }

        public PrefabRetrieveManager(Type _interface, IEnumerable<Assembly> _assemblies)
            : base(typeof(PrefabAttribute), _interface, _assemblies) { }


        public IEnumerable<Type> GetTypesByName(string name)
        {
            if (BufforedTypes == null)
            {
                BufforedTypes = RetrieveClasses();
            }

            var listByName = new List<Type>();
            foreach (Type type in BufforedTypes)
            {
                
                if (((PrefabAttribute[])(type.GetCustomAttributes(AttributeType, false))).Any(x => x.Name == name))
                {
                    listByName.Add(type);
                }

            }
            return listByName;
        }

    }
}
