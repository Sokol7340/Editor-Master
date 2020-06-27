using System;
using System.Windows.Forms;

namespace Editor
{
    public partial class newFileForm : Form
    {
        public string fileName = "";
        public newFileForm()
        {
            InitializeComponent();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            fileName = textBox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
