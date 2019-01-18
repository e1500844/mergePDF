using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace mergePDF
{
    internal class ItemViewer
    {
        private readonly TableLayoutPanel panel;
        public List<string> list;
        private Size buttonSize = new Size(15, 20);

        public ItemViewer()
        {
            list = new List<string>();
            panel = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 0,
                Width = 400,
                Height = 400
            };
        }

        public void AddRow(string name)
        {
            list.Add(name);
            AddToTable(name);
        }

        private void AddToTable(string name)
        {
            int column = 0;

            panel.Controls.Add(new Label()
            {
                Text = name,
                Size = new Size(320, 20),
            },
                column, panel.RowCount);

            column++;

            PictureBox delete = CreateDelete();
            delete.MouseClick += new MouseEventHandler((o, a) => DeleteRow(delete.Name));

            panel.Controls.Add(delete, column, panel.RowCount);

            PictureBox up = CreateUp();
            up.MouseClick += new MouseEventHandler((o, a) => MoveUp(up.Name));

            panel.Controls.Add(up, column, panel.RowCount);

            PictureBox down = CreateDown();
            down.MouseClick += new MouseEventHandler((o, a) => MoveDown(down.Name));

            panel.Controls.Add(down, column, panel.RowCount);

            panel.RowCount++;
        }

        private void UpdateTable(List<string> list)
        {
            // Restore panel
            panel.Controls.Clear();
            panel.RowStyles.Clear();
            panel.ColumnCount = 4;
            panel.RowCount = 0;

            // Update panel content
            foreach (var name in list)
            {
                int column = 0;

                panel.Controls.Add(new Label()
                {
                    Text = name,
                    Size = new Size(320, 20),
                },
                column, panel.RowCount);

                column++;

                PictureBox delete = CreateDelete();
                delete.MouseClick += new MouseEventHandler((o, a) => DeleteRow(delete.Name));

                panel.Controls.Add(delete, column, panel.RowCount);

                PictureBox up = CreateUp();
                up.MouseClick += new MouseEventHandler((o, a) => MoveUp(up.Name));

                panel.Controls.Add(up, column, panel.RowCount);

                PictureBox down = CreateDown();
                down.MouseClick += new MouseEventHandler((o, a) => MoveDown(down.Name));

                panel.Controls.Add(down, column, panel.RowCount);

                panel.RowCount++;
            }
        }

        /// <summary>
        /// Show panel content in MainForm
        /// </summary>
        /// <returns>TableLayoutPanel</returns>
        public TableLayoutPanel UpdateView()
        {
            return panel;
        }

        /// <summary>
        /// Check does panel contain .pdf documents
        /// </summary>
        /// <returns>True if documents added, otherwise false.</returns>
        public bool HasElements()
        {
            return panel.RowCount > 0;
        }

        private void DeleteRow(string index)
        {
            int idx = Convert.ToInt16(index);
            list.RemoveAt(idx);
            UpdateTable(list);
        }

        private void MoveUp(string index)
        {
            int idx = Convert.ToInt16(index);
            var tmp = list[idx];
            list.RemoveAt(idx);
            idx--;
            list.Insert(idx, tmp);
            UpdateTable(list);
        }

        private void MoveDown(string index)
        {
            int idx = Convert.ToInt16(index);
            var tmp = list[idx];
            list.RemoveAt(idx);
            idx++;
            list.Insert(idx, tmp);
            UpdateTable(list);
        }

        private PictureBox CreateDown()
        {
            return new PictureBox
            {
                Size = buttonSize,
                Image = Properties.Resources.symbol_down,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = panel.RowCount.ToString()
            };
        }

        private PictureBox CreateUp()
        {
            return new PictureBox
            {
                Size = buttonSize,
                Image = Properties.Resources.symbol_up,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = panel.RowCount.ToString()
            };
        }

        private PictureBox CreateDelete()
        {
            return new PictureBox
            {
                Size = buttonSize,
                Image = Properties.Resources.symbol_delete,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Name = panel.RowCount.ToString()
            };
        }
    }
}
