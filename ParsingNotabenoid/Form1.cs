using System;
using System.Windows.Forms;

namespace ParsingNotabenoid
{
    public partial class Form1 : Form
    {
        WebClientEx wc;
        Settings st;
        RealParsing rp;
        string url;
        string code;
        string selector;
        int pagesCount;
        string fileName;
        public Form1()
        {
            wc = new WebClientEx();
            st = new Settings();
            rp = new RealParsing();
            url = string.Empty;
            code = string.Empty;
            selector = string.Empty;
            fileName = string.Empty;
            pagesCount = 0;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wc = st.webcl;
            url = textBox1.Text;
            selector = ".t div p";
            Application.DoEvents();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                code = rp.getCodePage(wc, url);
                pagesCount = rp.getCountOfPage(code, ".chic-pages .selectable li:last-of-type");
                fileName = rp.getName(code, ".container-fluid h1", ".container-fluid h1 a");
                progressBar1.Minimum = 0;
                progressBar1.Maximum = pagesCount + 1;
                progressBar1.Value = 0;
                progressBar1.Value++;

                for (int i = 1; i <= pagesCount; i++)
                {
                    string fullURL = rp.getURLNextPage(url, i);
                    code = rp.getCodePage(wc, fullURL);
                    rp.Parse(code, selector);                   
                    progressBar1.Value++;
                }
                rp.SaveText(folderBrowserDialog1.SelectedPath + "\\" + fileName + ".txt");
            }
            MessageBox.Show("Well done!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
