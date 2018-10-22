using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TourHelper.Manager
{
    public abstract class ClassRetrieveManager
    {

        public Type Interface { get; internal set; }
        public Type AttributeType { get; internal set; }
        public IEnumerable<Assembly> Assemblies { get; internal set; }

        public ClassRetrieveManager(Type _attribute, Type _interface, IEnumerable<Assembly> _assemblies)
        {
            AttributeType = _attribute;
            Interface = _interface;
            Assemblies = _assemblies;
        }

        internal IEnumerable<Type> RetrieveClasses()
        {
            var _list = new List<Type>();
            if (Interface != null && Assemblies != null && AttributeType != null)
            {
                foreach (Assembly assembly in Assemblies)
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        var atributes = type.GetCustomAttributes(AttributeType, false);
                        var interfaces = type.GetInterfaces();
                        if (interfaces.Contains(Interface) && atributes.Any())
                        {
                            _list.Add(type);
                        }
                    }

                }

            }
            return _list;

        }
    }
}
