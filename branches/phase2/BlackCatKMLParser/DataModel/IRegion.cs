using System;
namespace BlackCat
{
    interface IRegion
    {
        string regionName { get; set; }
        BlackCat.Style regionStyle { get; set; }
        string regionType { get; set; }
    }
}
