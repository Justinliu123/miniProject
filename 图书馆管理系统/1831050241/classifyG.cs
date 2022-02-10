using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _1831050241
{
    public partial class classifyG : Form
    {
        List<Classify> list = new List<Classify>();
        public classifyG()
        {
            InitializeComponent();
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

        private void 首页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            index f = new index();
            this.Close();
            f.Show();
        }

        private void classifyG_Load(object sender, EventArgs e)
        {
            getAllClasses(list);
            showAllClasses(list);
        }
        //查询所有分类信息
        private void getAllClasses(List<Classify> list)
        {
            DB db = new DB();
            list.Clear();
            MySqlDataReader read = db.read("SELECT * FROM classify");
            while (read.Read())
            {
                list.Add(new Classify(read.GetString(0), read.GetString(1)));
            }
            list.Sort();
            db.close();
        }
        //将分类信息展示
        private void showAllClasses(List<Classify> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CheckBox c = new CheckBox();
                c.Name = list[i].classNum;
                c.Text = list[i].className;
                c.Margin = new Padding(20);
                panel1.Controls.Add(c);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (CheckBox ck in panel1.Controls)
                ck.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string classText = textBox1.Text;
            //文本框的内容不能为空
            if (classText == "")
            {
                MessageBox.Show("内容不能为空！");
                return;
            }
            else if (classText.Length > 10)
            {
                MessageBox.Show("内容长度需要小于10！");
                return;
            }

            //在数据库中添加
            DB db = new DB();
            MySqlCommand cmd = new MySqlCommand("insert ignore into classify(classname) values(@cn);", db.connect());
            cmd.Parameters.AddWithValue("cn", classText);//添加值
            if (cmd.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("已存在内容，请勿重复插入！");
                return;
            }
            MessageBox.Show("插入成功！");

            //文本框内容清空
            textBox1.Text = "";
            //修改显示内容
            panel1.Controls.Clear();
            getAllClasses(list);
            showAllClasses(list);
            db.close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //获取选中内容
            List<String> delList = new List<String>();
            foreach (CheckBox ck in panel1.Controls)
            {
                if (ck.Checked == true)
                {
                    delList.Add(ck.Name);
                }
            }
            
            //在数据库中删除选中内容
            DB db = new DB();
            MySqlCommand cmd = new MySqlCommand("delete from classify where classnum = @cnum;", db.connect());
            int num = 0;
            foreach (string s in delList)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("cnum", s);//添加值
                if (cmd.ExecuteNonQuery() > 0)
                {
                    num++;
                }
            }
            db.close();
            //显示删除成功信息
            MessageBox.Show("删除成功，共删除"+num+"条信息！");

            //更新显示
            panel1.Controls.Clear();
            getAllClasses(list);
            showAllClasses(list);
            
        }
    }
}
