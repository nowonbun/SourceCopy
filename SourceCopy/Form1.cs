using System;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace SourceCopy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            textBox1.Text = ConfigurationManager.AppSettings["base_path"];
            textBox2.Text = ConfigurationManager.AppSettings["target_path"];
            textBox3.Text = ConfigurationManager.AppSettings["source_file"];

            button1.Click += (sender, agrs) =>
            {
                var dialog1 = new FolderBrowserDialog();
                var result = dialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBox1.Text = dialog1.SelectedPath;
                }
            };
            button2.Click += (sender, agrs) =>
            {
                var dialog2 = new FolderBrowserDialog();
                var result = dialog2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBox2.Text = dialog2.SelectedPath;
                }
            };
            button3.Click += (sender, args) =>
            {
                var dialog3 = new OpenFileDialog();
                var result = dialog3.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBox3.Text = dialog3.FileName;
                }
            };
            button4.Click += (sender, args) =>
            {
                var filepath = textBox3.Text;
                if (String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("The source folder is not set: Error Code-0003");
                    return;
                }
                if (String.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("The destiny folder is not set: Error Code-0003");
                    return;
                }
                if (String.IsNullOrWhiteSpace(filepath))
                {
                    MessageBox.Show("The copy list file is not set: Error Code-0001");
                    return;
                }
                var info = new FileInfo(filepath);
                if (!info.Exists)
                {
                    MessageBox.Show("The copy list file is not exists: Error Code-0002");
                    return;
                }
                if (!Directory.Exists(textBox1.Text))
                {
                    MessageBox.Show("The source folder is not exists: Error Code-0004");
                    return;
                }
                try
                {
                    using (var stream = info.OpenRead())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                var str = reader.ReadLine().Trim();
                                if (String.IsNullOrEmpty(str.Trim()))
                                {
                                    continue;
                                }
                                if (str.StartsWith("file://"))
                                {
                                    str = str.Substring(7);
                                }
                                var des = str.Replace(textBox1.Text, textBox2.Text);
                                var co = new FileInfo(des);
                                if (!co.Directory.Exists)
                                {
                                    co.Directory.Create();
                                }
                                var aa = new FileInfo(str);
                                if (!aa.Exists)
                                {
                                    throw new Exception("file nothings : Error Code - 005");
                                }
                                try
                                {
                                    aa.CopyTo(co.FullName, true);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            MessageBox.Show("complete");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
        }
    }
}
