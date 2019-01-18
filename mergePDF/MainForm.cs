using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace mergePDF
{
    public partial class MainForm : Form
    {
        private readonly string tempPath = Path.Combine(Path.GetTempPath(), "mergedtmpdocument.pdf");
        private readonly List<string> paths;
        private readonly ItemViewer view;
        private readonly string errorMsg = "Failed";

        public MainForm()
        {
            InitializeComponent();
            view = new ItemViewer();
            paths = new List<string>();
            Controls.Add(view.UpdateView());
        }

        private void MergePDF(string path)
        {
            PdfDocument outPdf = new PdfDocument();

            foreach (var file in view.list)
            {
                using (PdfDocument one = PdfReader.Open(file, PdfDocumentOpenMode.Import))
                {
                    CopyPages(one, outPdf);
                }

                void CopyPages(PdfDocument from, PdfDocument to)
                {
                    for (int i = 0; i < from.PageCount; i++)
                    {
                        to.AddPage(from.Pages[i]);
                    }
                }
            }

            outPdf.Save(path);
        }

        private void Review()
        {
            try
            {
                MergePDF(tempPath);
                Process.Start(tempPath);
            }
            catch (Exception)
            {
                MessageBox.Show(errorMsg);
            }
        }

        private void ViewItems()
        {
            foreach (var item in paths)
            {
                AddNewItem(item);
            }
        }

        private void AddNewItem(string name)
        {
            view.AddRow(name);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {
            if (view.HasElements())
                Review();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (view.HasElements())
            {
                var sfd = new SaveFileDialog
                {
                    Filter = "pdf files (*.pdf)|*.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    MergePDF(sfd.FileName);
                }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var file in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (Path.GetExtension(file).Equals(".pdf"))
                {
                    paths.Add(file);
                    AddNewItem(file);
                }
            }
        }
    }
}
