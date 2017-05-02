using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class ComplexPropertiesCorrector
    {
        private static IEnumerable<PropertyInfo> GetComplexProperties(this Type type)
        {
            var pp = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsClass && typeof(IComplexType).IsAssignableFrom(p.PropertyType));
        }

        public static T InitComplexProperties<T>(this T parent)
            where T : class
        {
            if (parent == null)
                return null;
            InitComplexProperties(typeof(T), parent);
            return parent;
        }

        private static void InitComplexProperties(Type parentType, object parent)
        {
            foreach (var prop in parentType.GetComplexProperties())
            {
                object value = prop.GetValue(parent);
                if (value == null)
                    prop.SetValue(parent, value = Activator.CreateInstance(prop.PropertyType));
                InitComplexProperties(value.GetType(), value);
            }
        }

        public static T DropComplexProperties<T>(this T parent)
            where T : class
        {
            if (parent == null)
                return null;
            DropComplexProperties(typeof(T), parent);
            return parent;
        }

        private static void DropComplexProperties(Type parentType, object parent)
        {
            foreach (var prop in parentType.GetComplexProperties())
            {
                object value = prop.GetValue(parent);
                if (value != null)
                {
                    if (((IComplexType)value).IsEmpty())
                        prop.SetValue(parent, null);
                    else
                        DropComplexProperties(value.GetType(), value);
                }
            }
        }
    }
}
