using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    interface IKMLWriter
    {
        bool WriteToFile(String outputPath, IGeoModel model, ProgressBar progressBar);
    }
}
