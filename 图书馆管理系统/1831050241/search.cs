using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections;

namespace _1831050241
{
    public partial class search : Form
    {
        List<Book> books = new List<Book>();
        List<Classify> classes = new List<Classify>();
        int state = 1;              //当前的查询方式： 1 全部 2 按书名 3 按作者 4 按分类
        public search()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void search_Load(object sender, EventArgs e)
        {
            searchWay("select * from library");
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
            comboBox1.Items.Add("-请选择分类-");
            comboBox1.SelectedIndex = 0;
            for (int i = 0; i < classes.Count; i++)
            {
                comboBox1.Items.Add(classes[i].className);
            }
            
        }

        //查询图书信息
        private void searchWay(string sql)
        {
            books.Clear();                          //清空book中的数据

            DB db = new DB();
            //解析方法
            using (IDataReader read = db.read(sql))
            {
                while (read.Read())
                {
                    Book a = new Book();
                    a.bookNum = read.GetString(0);
                    a.bookName = read.GetString(1);
                    a.author = read.GetString(2);
                    a.intro = read.GetString(3);
                    a.press = read.GetString(4);
                    a.pubDate = read.GetDateTime(5);
                    a.price = read.GetFloat(6);

                    //将书籍信息的分类信息封装为一个对象
                    Classify c = new Classify();
                    DB dbc = new DB();
                    IDataReader readc = dbc.read("select * from classify where classnum = '" + read.GetString(7) + "';");
                    readc.Read();
                    c.classNum = readc.GetString(0);
                    c.className = readc.GetString(1);
                    dbc.close();

                    //赋值给a
                    a.classify = c;
                    a.stock = read.GetInt32(8);

                    books.Add(a);
                }
            }

            //将books转为DataTable绑定数据源给dataGridView1
            this.dataGridView1.DataSource = DataBindingByDataTable(books);

            //设置固定列的宽度
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[3].Width = 333;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[8].Width = 60;
        }
        private DataTable DataBindingByDataTable(List<Book> books)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("书号", typeof(string)); //数据类型为 文本
            dt.Columns.Add("书名", typeof(string));
            dt.Columns.Add("作者", typeof(string));
            dt.Columns.Add("介绍", typeof(string));
            dt.Columns.Add("出版社", typeof(string));
            dt.Columns.Add("出版日期", typeof(string));
            dt.Columns.Add("价格", typeof(float));
            dt.Columns.Add("类别", typeof(string));
            dt.Columns.Add("库存", typeof(int));

            for(int i = 0; i < books.Count(); i++){
                DataRow dr = dt.NewRow();
                dr["书号"] = books[i].bookNum;
                dr["书名"] = books[i].bookName;
                dr["作者"] = books[i].author;
                dr["介绍"] = books[i].intro;
                dr["出版社"] = books[i].press;
                dr["出版日期"] = books[i].pubDate.Year + "-" + books[i].pubDate.Month + "-" + books[i].pubDate.Day;
                dr["价格"] = books[i].price;
                dr["类别"] = books[i].classify.className;
                dr["库存"] = books[i].stock;
                dt.Rows.Add(dr);
            }
            return dt;
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

        //查看
        private void button3_Click(object sender, EventArgs e)
        {
            //检测时候选中行
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选中后操作！");
                return;
            }
            detail f = new detail(books[dataGridView1.CurrentRow.Index]);
            this.Close();
            f.Show();
        }

        //修改
        private void button2_Click(object sender, EventArgs e)
        {
            //检测时候选中行
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选中后操作！");
                return;
            }
            alter f = new alter(books[dataGridView1.CurrentRow.Index], 1);
            this.Close();
            f.Show();
        }

        //删除
        private void button4_Click(object sender, EventArgs e)
        {
            //检测时候选中行
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选中后操作！");
                return;
            }
            Book book = books[dataGridView1.CurrentRow.Index];

            string delNum = delText.Text;
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
            else if (!testNum(delNum))
            {
                MessageBox.Show("格式错误，必须输入数字！");
                return;
            }

            //创建sql语句
            string sql;
            bool isDel = false;             //库存数量是否为零了
            int oldStock = book.stock; ;
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
                MessageBox.Show("删除成功！\n原库存:" + oldStock + "\n删去" + del + "本\n当前库存:" + book.stock);
                //刷新页面库存信息
                searchBook();
                delText.Text = "";
            }
            else
            {
                MessageBox.Show("删除成功！\n删去" + book.stock + "本\n当前库存为零，已删除书本信息");
                searchBook();
            }
        }
        
        //刷新，需要先设置状态码 state
        private void searchBook()
        {
            if (state == 1)
            {
                searchWay("select * from library");
            }
            else if (state == 2)
            {
                searchWay("select * from library where bookname like '%" + nameSearchText.Text + "%';");
            }
            else if (state == 3)
            {
                searchWay("select * from library where author like '%" + authorText.Text + "%';");
            }
            else if (state == 4)
            {
                foreach (Classify c in classes)
                {
                    if (c.className == comboBox1.Text)
                        searchWay("select * from library where classnum = '" + c.classNum + "';");
                }
            }
        }

        //检测数字格式
        private bool testNum(string s)
        {
            try
            {
                Int32.Parse(s);
            }
            catch (System.FormatException)
            {
                return false;
            }
            return true;
        }

        //查询
        private void button1_Click(object sender, EventArgs e)
        {
            //最先按书名模糊插查询=============================
            if(nameSearchText.Text != "")
            {
                state = 2;
                searchBook();
                return;
            }
            //按作者模糊查询=====================================
            if(authorText.Text != "")
            {
                state = 3;
                searchBook();
                return;
            }
            //选择分类查询===================================
            Classify choseClass;

            //如果分类删除，直接查询全部书
            if (comboBox1.Text == "")
            {
                state = 1;
                searchBook();
                return;
            }

            if (comboBox1.SelectedIndex != 0)
            {
                //分类必须存在
                for (int i = 0; i < classes.Count; i++)
                {
                    if (classes[i].className == comboBox1.Text)
                    {
                        choseClass = classes[i];
                        break;
                    }
                    if (i == classes.Count - 1)
                    {
                        MessageBox.Show("不存在该分类！请选择");
                        return;
                    }
                }
                state = 4;
                searchBook();
            }
            else
            {
                state = 1;
                searchBook();
                return;
            }

        }
    }
}
