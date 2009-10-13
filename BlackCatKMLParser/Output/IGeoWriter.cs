using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    public interface IGeoWriter
    {
        // Writes a KML file to outputPath using the data held in model and displaying the 
        // MapInfo data fields listed in dataFieldsToDisplay (note, this can be an empty list if // the geographical data source was a KML file). progressBar is updated as the 
        // operation  progresses.
        // Returns a boolean indicating whether the operation was successful or not.
        // Pre: model is not null, outputPath is not null and progressBar is not null
        // Post: The data contained in model have been written to a KML file at outputPath. 
        // This file’s content complies with the requirements set down in Appendices 1, 2 and // 3 of the Requirements Specification. The file contains the data specified in 
        // dataFieldsToDisplay. progressBar was also updated.

        bool WriteToFile(IGeoModel model, List<String> dataFieldsToDisplay, String outputPath, ProgressWrapper progressBar);

    }
}
