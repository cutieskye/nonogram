using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nonogram
{

    public partial class Form2 : Form
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void widthTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (int.TryParse(widthTextBox.Text, out int width) == false
                || width < 2 || width > 15)
            {
                errorProvider.SetError(widthTextBox, "Width must be integer number in range 2-15");
                e.Cancel = true;
                return;
            }

            Columns = width;
            errorProvider.SetError(widthTextBox, "");
            e.Cancel = false;
        }

        private void heightTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (int.TryParse(heightTextBox.Text, out int height) == false
                || height < 2 || height > 15)
            {
                errorProvider.SetError(heightTextBox, "Height must be integer number in range 2-15");
                e.Cancel = true;
                return;
            }

            Rows = height;
            errorProvider.SetError(heightTextBox, "");
            e.Cancel = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ValidateChildren();
        }
    }

}
