using System;

namespace putting_challenge.Lucioli
{
    public class DescriptorAttributeBatType : Attribute
    {
        public double Strength { get; }
        public DescriptorAttributeBatType(double strength)
        {
            Strength = strength;
        }
    }
    public enum BatType
    {
        [DescriptorAttributeBatType(strength: 1.4)]
        Hybrid,
        [DescriptorAttributeBatType(strength: 0.6)]
        Wedge,
        [DescriptorAttributeBatType(strength: 0.8)]
        Iron,
        [DescriptorAttributeBatType(strength: 0.2)]
        Putter,
        [DescriptorAttributeBatType(strength: 2)]
        Wood
    }
}
