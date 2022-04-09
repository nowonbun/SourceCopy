using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

            textBox1.Text = @"D:\SCP\Work";
            textBox2.Text = @"D:\SCP\Merge";
            textBox3.Text = @"D:\SCP\copylist.txt";

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
                String filepath = textBox3.Text;
                FileInfo info = new FileInfo(filepath);
                if (!info.Exists)
                {
                    MessageBox.Show("not list");
                    return;
                }
                if (!Directory.Exists(textBox1.Text))
                {
                    MessageBox.Show("not directory");
                    return;
                }
                using (FileStream stream = info.OpenRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            String str = reader.ReadLine().Trim();
                            if (String.IsNullOrEmpty(str.Trim())) {
                                continue;
                            }
                            String des = str.Replace(textBox1.Text, textBox2.Text);
                            FileInfo co = new FileInfo(des);
                            if (!co.Directory.Exists)
                            {
                                co.Directory.Create();
                            }

                            FileInfo aa = new FileInfo(str);
                            if (!aa.Exists)
                            {
                                throw new Exception("file nothings");
                            }
                            try
                            {
                                aa.CopyTo(co.FullName, true);
                            }catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        
                            //File.Copy(str, des, true);
                        }
                        MessageBox.Show("complete");
                    }
                }
            };
        }
    }
}
