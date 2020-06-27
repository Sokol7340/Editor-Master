using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Editor
{
    public partial class EditorForm : Form
    {
        string filePath = "";

        public EditorForm()
        {
            InitializeComponent();
            selectallToolStripMenuItem.Click += (s, e) => richTextBox.SelectAll();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filePath = string.Empty;
            var newFileDialog = new newFileForm();
            if (newFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                richTextBox.Enabled = true;
                filePath = Path.Combine(Directory.GetCurrentDirectory(), newFileDialog.fileName);
                richTextBox.Clear();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.LoadFile(openFileDialog.FileName);
                richTextBox.Enabled = true;
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if(filePath != string.Empty)
            {
                richTextBox.SaveFile(filePath);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDialog.FileName;
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox.CanUndo)
            {
                richTextBox.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanRedo)
            {
                richTextBox.Redo();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void selectallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fontDialog.ShowDialog() == DialogResult.OK)
                richTextBox.SelectionFont = fontDialog.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
                richTextBox.SelectionColor = colorDialog.Color;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var printDoc = new PrintDocument();
            printDoc.DocumentName = filePath;
            printDoc.PrintPage += printerHandler;
            printDialog.Document = printDoc;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
        }

        private void printerHandler(object sender, PrintPageEventArgs e)
        {
            int count = 0;
            Font printFont = fontDialog.Font;
            Brush printBrush = new SolidBrush(colorDialog.Color);
            foreach (var line in richTextBox.Lines)
            {
                float yPos = e.MarginBounds.Top + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, printBrush, e.MarginBounds.Left, yPos, new StringFormat());
                count++;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RTF редактор", "Информация", MessageBoxButtons.OK);
        }
    }
}
