using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace _1831050241
{
    public partial class alter : Form
    {
        Book book;
        List<Classify> classes = new List<Classify>();
        int form;               //记录进入的入口  1 表示查询页进入， 2 表示详细页进入
        public alter(Book book, int form)
        {
            this.book = book;
            this.form = form;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //加载所有的分类
            DB dbc = new DB();
            IDataReader readc = dbc.read("select * from classify;");
            while (readc.Read())
            {
                string classNum = readc.GetString(0);
                string className = readc.GetString(1);
                classes.Add(new Classify(classNum, className));
            }
            classes.Sort();
            dbc.close();

            //将分类列表绑定下拉列表框
            classname.Items.Add("-请选择分类-");
            classname.SelectedIndex = 0;
            for (int i = 0; i < classes.Count; i++)
            {
                classname.Items.Add(classes[i].className);
            }

            pubDate.Value = book.pubDate;

            //加载书号
            booknum.Text = string.Format("ISBN {0}-{1}-{2}-{3}-{4}", book.bookNum.Substring(0, 3),
                book.bookNum.Substring(3, 1), book.bookNum.Substring(4, 3), book.bookNum.Substring(7, 5), book.bookNum.Substring(12, 1));

            //加载其他信息
            bookname.Text = book.bookName;
            author.Text = book.author;
            classname.Text = book.classify.className;
            press.Text = book.press;
            price.Text = book.price.ToString();
            stock.Text = book.stock.ToString();
            intro.Text = book.intro;
        }

        //返回
        private void button2_Click(object sender, EventArgs e)
        {
            back();
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

        //返回上一层界面
        private void back()
        {
            this.Close();
            Form f = null;
            //查询页进入
            if (form == 1)
            {
                f = new search();
            }
            else if (form == 2) //从详情页进入
            {
                f = new detail(book);
            }
            f.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //修改
        private void button1_Click(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------------
            //检测书名的输入格式,不能为空，最后长60位
            if (bookname.Text == "")
            {
                MessageBox.Show("书名不能为空！");
                return;
            }
            else if (bookname.Text.Length > 60)
            {
                MessageBox.Show("书名长度必须小于60位！");
                return;
            }
            book.bookName = bookname.Text;

            //检测作者输入格式：非空，最长20位
            if (author.Text == "")
            {
                MessageBox.Show("作者不能为空！");
                return;
            }
            else if (author.Text.Length > 20)
            {
                MessageBox.Show("作者名过长！应小于20位");
                return;
            }
            book.author = author.Text;

            //检测分类的输入格式：Text中的内容必须在classes中存在
            for (int i = 0; i < classes.Count; i++)
            {
                if (classes[i].className == classname.Text)
                {
                    book.classify = classes[i];
                    break;
                }
                if (i == classes.Count - 1)
                {
                    MessageBox.Show("不存在该分类！请选择");
                    return;
                }
            }

            //检测出版社的输入格式：非空，最长50位
            if (press.Text == "")
            {
                MessageBox.Show("出版社不能为空！");
                return;
            }
            else if (press.Text.Length > 50)
            {
                MessageBox.Show("出版社名过长！应小于50位");
                return;
            }
            book.press = press.Text;

            //检测出版日期的输入格式：时间节点不能大于当前时间
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            DateTime dt = Convert.ToDateTime(pubDate.Text, dtFormat);
            DateTime ndt = DateTime.Now;
            if (dt.CompareTo(ndt) > 0)      //不满足要求
            {
                MessageBox.Show("出版时间必须晚于当前时间！");
                return;
            }
            book.pubDate = dt;
            //检测定价输入格式：非空，最长六位（包含两位小数）
            if (price.Text == "")
            {
                MessageBox.Show("定价不能为空！");
                return;
            }
            else if (!testPrice(price.Text))
            {
                MessageBox.Show("输入金额格式错误！注意取值在0.01-999999之间");
                return;
            }
            book.price = float.Parse(price.Text);

            //检测库存输入格式：非空，最长10位整数
            if (stock.Text == "")
            {
                MessageBox.Show("库存不能为空！");
                return;
            }
            else if (!testVarNum(stock.Text, 10))
            {
                MessageBox.Show("库存格式输入错误！");
                return;
            }
            else if (Int32.Parse(stock.Text) == 0)
            {
                MessageBox.Show("库存不能为0！");
                return;
            }
            book.stock = Int32.Parse(stock.Text);

            //简介输入格式：非空，最长1000位
            if (intro.Text == "")
            {
                MessageBox.Show("简介不能为空！");
                return;
            }
            else if (intro.Text.Length > 1000)
            {
                MessageBox.Show("简介过长！应小于1000位");
                return;
            }
            book.intro = intro.Text;
            //==========================================================================================
            //插入数据库
            DB db = new DB();
            MySqlCommand cmd = new MySqlCommand("update library set `bookname`=@bookname,`author`=@author,`intro`=@intro,`press`=@press,"
                + "`pubdate`=@pubdate,`price`=@price,`classnum`=@classnum,`stock`=@stock where `booknum`=@booknum;", db.connect());
            cmd.Parameters.AddWithValue("booknum", book.bookNum);
            cmd.Parameters.AddWithValue("bookname", book.bookName);
            cmd.Parameters.AddWithValue("author", book.author);
            cmd.Parameters.AddWithValue("intro", book.intro);
            cmd.Parameters.AddWithValue("press", book.press);
            cmd.Parameters.AddWithValue("pubdate", book.pubDate.ToShortDateString());
            cmd.Parameters.AddWithValue("price", book.price);
            cmd.Parameters.AddWithValue("classnum", book.classify.classNum);
            cmd.Parameters.AddWithValue("stock", book.stock);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("修改成功！");
                back();
            }
            else
            {
                MessageBox.Show("修改失败！");
            }
        }

        //检测是否是定长纯数字
        private bool testNum(string s, int len)
        {
            if (s.Length != len)
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
        //检测是否是不定长纯数字
        private bool testVarNum(string s, int len)
        {
            if (s.Length > len)
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
        //检测价格格式
        private bool testPrice(string s)
        {
            if (Regex.IsMatch(s, "\\.") && (s.Length > 7 || Regex.IsMatch(s, @".\w{3}")))
            {
                return false;
            }
            else if (s.Length > 6)
            {
                return false;
            }
            try
            {
                float f = float.Parse(s);
                if (f <= 0 || f > 999999)
                {
                    return false;
                }
            }
            catch (System.FormatException)
            {
                return false;
            }
            return true;
        }
    }
}
