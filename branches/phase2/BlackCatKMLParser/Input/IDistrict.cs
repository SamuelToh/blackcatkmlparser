using System;
namespace BlackCat
{
    public interface IDistrict
    {
        bool Contains(string regionName);
        string DistrictName { get; set; }
        System.Collections.Generic.List<string> RegionNames { get; set; }
    }
}
