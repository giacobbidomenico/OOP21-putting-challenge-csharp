using System;
using System.ComponentModel;
using System.Reflection;

namespace putting_challenge.Lucioli
{
    public class Bat
    {
        private BatType _batType;

        public Bat(BatType batType)
        {
            _batType = batType;
        }

        public double GetBatStrength()
        {
            Type type = _batType.GetType();

            string name = Enum.GetName(type, _batType);
            FieldInfo field = type.GetField(name);

            DescriptionAttribute attr = (DescriptionAttribute) field.GetCustomAttribute<Attribute>();
            return double.Parse(attr.Description);
        }
    }
}
