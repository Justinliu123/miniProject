using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _1831050241
{
    public partial class detail : Form
    {
        Book book;
        bool isSearch = false;          //用来判断是从search页面跳转过来的还是从首页跳转过来的
        public detail(Book book)
        {
            this.book = book;
            InitializeComponent();
        }
        public detail(Book book, Form f)
        {
            this.book = book;
            isSearch = true;
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        
        private void detail_Load(object sender, EventArgs e)
        {
            //初始化显示信息
            bookname.Text = book.bookName;
            booknum.Text = string.Format("ISBN {0}-{1}-{2}-{3}-{4}", book.bookNum.Substring(0, 3),
                book.bookNum.Substring(3, 1), book.bookNum.Substring(4, 3), book.bookNum.Substring(7, 5), book.bookNum.Substring(12, 1));
            author.Text = book.author;
            classname.Text = book.classify.className;
            press.Text = book.press;
            pubdate.Text = book.pubDate.ToShortDateString().ToString();
            price.Text = string.Format("{0}元", book.price);
            stock.Text = book.stock.ToString();
            intro.Text = book.intro;
        }

        //修改
        private void button1_Click(object sender, EventArgs e)
        {
            alter f = new alter(book, 2);
            this.Close();
            f.Show();
        }

        //删除。当库存数量小于要删除的数量时，直接删除图书信息
        private void button2_Click(object sender, EventArgs e)
        {
            string delNum = textBox1.Text;
            //检查文本框格式
            if (delNum == "")
            {
                MessageBox.Show("删除数量不能为空！");
                return;
            }
            else if (delNum.Length > 10)
            {
                MessageBox.Show("删除数量不能大于9999999999！");
                return;
            }
            else if(!testNum(delNum))
            {
                MessageBox.Show("格式错误，必须输入数字！");
                return;
            }

            //创建sql语句
            string sql; 
            bool isDel = false;             //库存数量是否为零了
            int del = Int32.Parse(delNum);
            if (del < book.stock)//删除数量小于库存时
            {
                book.stock -= del;
                sql = "UPDATE library SET stock = " + book.stock + " WHERE booknum = '" + book.bookNum + "';";
            }
            else
            {
                isDel = true;
                sql = "delete from library where booknum = '" + book.bookNum + "';";
            }

            //执行删除
            DB db = new DB();
            db.Execute(sql);

            //返回删除信息
            if (!isDel)
            {
                MessageBox.Show("删除成功！\n原库存:" + stock.Text + "\n删去" + del + "本\n当前库存:" + book.stock);
                //刷新页面库存信息
                stock.Text = book.stock.ToString();
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("删除成功！\n删去" + book.stock + "本\n当前库存为零，已删除书本信息");
                if (isSearch)//当从search页面跳转过来时
                {
                    search f = new search();
                    this.Close();
                    f.Show();
                }
                else
                {
                    index indexf = new index();
                    this.Close();
                    indexf.Show();
                }
            }

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

        //检测数字格式
        private bool testNum(string s)
        {
            try{
                Int32.Parse(s);
            }catch(System.FormatException){
                return false;
            }
            return true;
        }

        private void detail_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
