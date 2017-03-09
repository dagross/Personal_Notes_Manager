using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace Personal_Notes_Manager
{
    public partial class Form1 : Form
    {
        XDocument doc;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doc = XDocument.Load(@"..\..\Logins.xml");
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, textBox2.Text);
            var res = doc.Element("root").Elements("Account").Where(x => x.Attribute("login").Value == textBox1.Text && x.Attribute("password").Value == hash);
            if(res.Count() > 0)
            {
                Form2 f2 = new Form2();
                f2.Show();
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
