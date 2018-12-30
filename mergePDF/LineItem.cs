using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace mergePDF
{
    internal class LineItem
    {
        private readonly FlowLayoutPanel flowPanel;
        private const int top = 25;
        private const int left = 50;

        public LineItem(string name, int index)
        {
            flowPanel = new FlowLayoutPanel();
            var label = new Label
            {
                Text = name,
                Width = 200
            };
            var delete = new PictureBox
            {
                Size = new Size(20, 20),
                Image = Properties.Resources.symbol_delete,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            flowPanel.Controls.Add(label);
            flowPanel.Controls.Add(delete);
            flowPanel.Top = index * top;
            flowPanel.Left = left;
            flowPanel.Height = 20;
            flowPanel.TabIndex = index;
        }

        public FlowLayoutPanel GetLine()
        {
            return flowPanel;
        }
    }
}
