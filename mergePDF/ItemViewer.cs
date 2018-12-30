using System.Windows.Forms;
using System.Drawing;

namespace mergePDF
{
    internal class ItemViewer
    {
        private readonly TableLayoutPanel panel;

        public ItemViewer()
        {
            panel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 0,
                Width = 200,
                Height = 20
            };
        }

        public void AddRow(string name, int index)
        {
            panel.RowCount++;
            panel.Height = 299;
            int column = 0;
            panel.Controls.Add(new Label() { Text = name, Size = new Size(20, 20) }, column, panel.RowCount - 1);
            column++;
            panel.Controls.Add(new PictureBox { Size = new Size(200, 20), Image = Properties.Resources.symbol_delete, SizeMode = PictureBoxSizeMode.StretchImage },
                column, panel.RowCount - 1);

            foreach (RowStyle style in panel.RowStyles)
            {
                // Set the row height to 20 pixels.
                style.SizeType = SizeType.Absolute;
                style.Height = 20;
            }
        }

        public TableLayoutPanel UpdateView()
        {
            return panel;
        }
    }
}
