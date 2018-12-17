using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private string tempPath = Path.Combine(Path.GetTempPath(), "mergedtmpdocument.pdf");
        private List<string> paths = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MergePDF()
        {
            using (PdfDocument one = PdfReader.Open(@"C:\Users\tomkoi\Documents\Oppari\abc.pdf", PdfDocumentOpenMode.Import))
            using (PdfDocument two = PdfReader.Open(@"C:\Users\tomkoi\Documents\Oppari\UiPath Orchestrator 2018.1 Diploma.pdf", PdfDocumentOpenMode.Import))
            using (PdfDocument outPdf = new PdfDocument())
            {
                CopyPages(one, outPdf);
                CopyPages(two, outPdf);

                outPdf.Save(tempPath);
            }

            void CopyPages(PdfDocument from, PdfDocument to)
            {
                for (int i = 0; i < from.PageCount; i++)
                {
                    to.AddPage(from.Pages[i]);
                }
            }
        }

        private void Review(string pathToPDF)
        {
            try
            {
                Process.Start(pathToPDF);
            }
            catch (Exception)
            {
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {
            Review(tempPath);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MergePDF();
        }

        void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var file in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (Path.GetExtension(file).Equals(".pdf"))
                    paths.Add(file);
            }
        }
    }
}
