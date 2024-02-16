using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace employees
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (user_name_txt.Text == "" || password_txt.Text == "")
            {
               
             
                errorProvider1.SetError(user_name_txt ,"يجب ادخال اسم المستخدم");
                errorProvider1.SetError(password_txt, "يجب ادخال كلمة المرور");
                
                
            }
            else
            {
                SqlConnection mycon = new SqlConnection(Class1.x);
                mycon.Open();
                SqlCommand mycom2 = new SqlCommand("select user_id from users where (user_name = @user) and (password = @pass)", mycon);
                SqlParameter p1 = new SqlParameter("@user", user_name_txt.Text);
                SqlParameter p2 = new SqlParameter("@pass", password_txt.Text);
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p1);
                mycom2.Parameters.Add(p2);
                SqlDataReader myreader = mycom2.ExecuteReader();
                if (myreader.HasRows == false)
                {


                    errorProvider1.SetError(button1, "اسم المستخدم او كلمة المرور خاطئة الرجاء التأكد");
                    
                    user_name_txt.Text = "";
                    password_txt.Text = "";
                }
                else
                {
                    Form1 f = new Form1();
                    f.label42.Text = user_name_txt.Text;
                    while (myreader.Read())
                    {
                        
                        f.user_id_txt.Text = Convert.ToString(myreader[0]);
                    }
                    user_name_txt.Text = "";
                    password_txt.Text = "";
                    f.ShowDialog();
                }

            }
        }

        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {
            MessageBox.Show("sdf");
        }
    }
}
    

