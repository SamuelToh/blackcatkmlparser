using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace BlackCat
{
    public class ProgressWrapper
    {
        private ProgressBar bar;
        private ILog log;

        public ProgressWrapper(ProgressBar bar)
        {
            this.bar = bar;
            bar.Minimum = 0;
            bar.Maximum = 100;
            log = LogManager.GetLogger(this.ToString());
        }

        public void SetPercentage(int percentage)
        {
            if (percentage >= 0 && percentage <= 100)
            {
                this.bar.Value = percentage;
            }
        }

        public int GetPercentage()
        {
            return bar.Value;
        }
                
        public void Increment(int value)
        {
            int newPercentage = GetPercentage() + value;
            SetPercentage(newPercentage);
        }

    }
}
