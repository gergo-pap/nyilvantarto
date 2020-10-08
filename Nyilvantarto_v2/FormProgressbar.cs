using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2
{
    public partial class FormProgressbar : Form
    {
        public FormProgressbar()
        {
            InitializeComponent();
        }

        public void incrementProgress(int incr, int sleep)
        {
            for (int i = 0; i < 10; i++)
            {
                progressBar1.Value += incr/10;
                Thread.Sleep(sleep);
            }
        }

        public void setProgressbarMax(int max)
        {
            progressBar1.Maximum = max;
        }

    }
}
