using System;
namespace BlackCat
{
    public interface IRegion
    {
        string regionName { get; set; }
        BlackCat.Style regionStyle { get; set; }
        string regionType { get; set; }
    }
}
