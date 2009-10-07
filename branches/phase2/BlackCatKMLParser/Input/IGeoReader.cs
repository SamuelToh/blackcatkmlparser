using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public interface IGeoReader
    {
        // Gets a list of Region objects found in the geographical data source. Updates bar 
        // as the operation progresses.
        // Returns a list of Region objects.
        // Pre: bar is not null
        // Post: A list of Region objects has been returned and bar updated.

        List<Region> ReadRegions(ProgressBar bar);

    }
}
