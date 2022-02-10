using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace _1831050241
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void index_Load(object sender, EventArgs e)
        {

        }

        //首页图片生成
        private void index_Paint(object sender, PaintEventArgs e)
        {
            Graphics g1 = e.Graphics;
            Rectangle re = new Rectangle(175, 40, 1076, 157);
            Brush b1 = new LinearGradientBrush(re, Color.OliveDrab, Color.Purple, LinearGradientMode.Horizontal);
            string s1 = "欢迎来到图书管理系统";
            Font ft1 = new Font("华文行楷", 48, FontStyle.Bold);
            g1.DrawString(s1, ft1, b1, 300, 40);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //精确查询
        private void button3_Click(object sender, EventArgs e)
        {
            string bookN = textBox1.Text;

            //检测输入格式
            if (bookN == "")
            {
                MessageBox.Show("书号不能为空！");
                return;
            }
            else if (!testNum(bookN))
            {
                MessageBox.Show("输入的书号格式错误！必须是13位数字");
                return;
            }



            //在数据库中查询---------------------------------------------------------------
            DB db = new DB();
            string sql = "select * from library where booknum = '" + bookN + "';";
            IDataReader read = db.read(sql);
            
            //因为只有一条数据，直接往后访问一条即可
            read.Read();
 
            Book book = new Book();
            book.bookNum = read.GetString(0);
            book.bookName = read.GetString(1);
            book.author = read.GetString(2);
            book.intro = read.GetString(3);
            book.press = read.GetString(4);
            book.pubDate = read.GetDateTime(5);
            book.price = read.GetFloat(6);

            //将书籍信息的分类信息封装为一个对象
            Classify c = new Classify();
            DB dbc = new DB();
            IDataReader readc = dbc.read("select * from classify where classnum = '" + read.GetString(7) + "';");
            readc.Read();
            c.classNum = readc.GetString(0);
            c.className = readc.GetString(1);
            dbc.close();

            //赋值给a
            book.classify = c;
            book.stock = read.GetInt32(8);
            //---------------------------------------------------------------------------
            
            //将book传给详情页
            detail f = new detail(book);
            this.Close();
            f.Show();
        }
        //检测是否是13位纯数字
        private bool testNum(string s)
        {
            if (s.Length != 13)
            {
                return false;
            }
            char[] chars = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (chars[i] < '0' || chars[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void 首页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            index f = new index();
            this.Close();
            f.Show();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 分类管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            classifyG f = new classifyG();
            this.Close();
            f.Show();
        }

        //查找图书
        private void button1_Click(object sender, EventArgs e)
        {
            search f = new search();
            this.Close();
            f.Show();
        }

        //添加图书
        private void button2_Click(object sender, EventArgs e)
        {
            add f = new add();
            this.Close();
            f.Show();
        }
    }
}
