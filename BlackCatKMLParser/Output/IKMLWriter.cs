using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    interface IKMLWriter
    {
        bool WriteToFile(IGeoModel model, List<String> dataFieldsToDisplay, String outputPath, ProgressBar progressBar);
    }
}
