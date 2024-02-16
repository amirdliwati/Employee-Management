using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using JbsaPrintDataGridView;

namespace employees
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                dep_name_combo.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select dep_name from department ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dep_name_combo.Items.Add(reder[0]);

                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void dep_name_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select dep_no , dep_name , descreption from department where dep_name = @name", con);
                SqlParameter p = new SqlParameter("@name", dep_name_combo.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dep_no_txt.Text = Convert.ToString(reader[0]);
                    dep_name_txt.Text = Convert.ToString(reader[1]);
                    dep_desc_txt.Text = Convert.ToString(reader[2]);


                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            timer2.Enabled = false;
            dep_no_txt.Text = "";
            dep_name_txt.Text = "";
            dep_desc_txt.Text = "";
            dep_name_combo.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            
            errorProvider1.Clear();
            try
            {
                SqlConnection mycon = new SqlConnection(Class1.x);
                mycon.Open();
                SqlCommand mycom2 = new SqlCommand("select * from department where (dep_name = @name)", mycon);
                SqlParameter p15 = new SqlParameter("@name", dep_name_txt.Text);
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p15);
                SqlDataReader myreader = mycom2.ExecuteReader();
                if (myreader.HasRows)
                {
                    errorProvider1.SetError(dep_name_txt, "عذراً هذا القسم موجود الرجاء التأكد");
                    mycon.Close();
                }
                else if (dep_name_txt.Text == "" || dep_desc_txt.Text == "")
                    errorProvider1.SetError(button5, "عذراً يجب عدم ترك حقل فارغ");
                else
                {

                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("INSERT INTO department (dep_name ,descreption) VALUES (@name1, @desc)", mycon1);
                    SqlParameter p = new SqlParameter("@name1", dep_name_txt.Text);
                    SqlParameter p1 = new SqlParameter("@desc", dep_desc_txt.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p);
                    mycom1.Parameters.Add(p1);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    myreader1.Close();

                    dep_no_txt.Text = "";
                    dep_name_txt.Text = "";
                    dep_desc_txt.Text = "";


                   



                    toolTip1.Show("تم الحفظ بنجاح", button5);
                }

                
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
            timer2.Enabled = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (dep_no_txt.Text == "")
                {
                    errorProvider1.SetError(button6, "لا يوجد قسم لتعديله الرجاء التأكد");
                }
                else
                
                    if (dep_name_txt.Text == "" || dep_desc_txt.Text == "")
                        errorProvider1.SetError(button6, "عذراً يجب عدم ترك حقل فارغ");
                    
                    else 
                    {
                        
                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("UPDATE department SET descreption = @desc WHERE (dep_no = @id1) ", mycon1);
                        
                        SqlParameter p1 = new SqlParameter("@desc", dep_desc_txt.Text);
                        SqlParameter p2 = new SqlParameter("@id1", Convert.ToInt32(dep_no_txt.Text));
                        mycom1.CommandType = CommandType.Text;
                        
                        mycom1.Parameters.Add(p1);
                        mycom1.Parameters.Add(p2);


                        SqlDataReader myreader1 = mycom1.ExecuteReader();

                        dep_no_txt.Text = "";
                        dep_name_txt.Text = "";
                        dep_desc_txt.Text = "";
                        dep_name_combo.SelectedItem = "";


                        toolTip1.Show("تم التعديل بنجاح", button6);
                    }
                timer2.Enabled = true;
                }
            
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (dep_no_txt.Text == "")
                {
                    errorProvider1.SetError(button7, "لا يوجد بيانات لحذفها الرجاء التأكد");
                }
                else
                {
                    SqlConnection con = new SqlConnection(Class1.x);
                    con.Open();
                    SqlCommand com = new SqlCommand("select emp_no from employees where (dept_no = @id)", con);
                    SqlParameter p = new SqlParameter("@id", Convert.ToInt32(dep_no_txt.Text));
                    com.Parameters.Add(p);
                    com.CommandType = CommandType.Text;
                    SqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        errorProvider1.SetError(button7, "عذراً يوجد موظفين مرتبطين بهذا القسم , الرجاء نقلهم إلى قسم آخر قبل القيام بعملية الحذف");
                       
                    }
                    else if ((Convert.ToString(MessageBox.Show("هل أنت متأكد من الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes"))
                    {
                        reader.Close();
                        com.CommandText = "DELETE FROM department WHERE (dep_no = @id1)";
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        SqlParameter p1 = new SqlParameter("@id1", Convert.ToInt32(dep_no_txt.Text));
                        com.Parameters.Add(p1);
                        reader = com.ExecuteReader();
                        reader.Close();
                        con.Close();
                        dep_no_txt.Text = "";
                        dep_name_txt.Text = "";
                        dep_desc_txt.Text = "";
                        dep_name_combo.SelectedText = "";
                        toolTip1.Show("تم الحذف بنجاح", button7);
                    }

                }
                timer2.Enabled = true;
            }

            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {


                dep_name_combo.Items.Clear();
                dep_name_combo1.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select dep_name from department ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dep_name_combo.Items.Add(reder[0]);
                    dep_name_combo1.Items.Add(reder[0]);
                    dep_name_combo4.Items.Add(reder[0]);

                }
                reder.Close();
                com.CommandText = "select emp_fname from employees group by emp_fname";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    emp_fname_combo.Items.Add(reder[0]);
                    emp_fname_combo1.Items.Add(reder[0]);
                    emp_fname_combo2.Items.Add(reder[0]);
                    emp_fname_combo3.Items.Add(reder[0]);
                    emp_fname_combo4.Items.Add(reder[0]);
                    emp_fname_combo_5.Items.Add(reder[0]);
                    
                    

                }
                reder.Close();
                com.CommandText = "select mat_name from materials";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    mat_name_combo.Items.Add(reder[0]);
                    mat_name_combo2.Items.Add(reder[0]);


                }
                reder.Close();
                com.CommandText = "select dep_name , descreption from department";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dataGridView1.Rows.Add(reder[0] , reder[1]);
                    

                }
                reder.Close();
                com.CommandText = "select materials.mat_name , materials.descreption , materials.year , materials.simester , materials.mat_type , materials.houres_1 , materials.houres_2 , department.dep_name from materials INNER JOIN department ON (materials.dep_no = department.dep_no)";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dataGridView4.Rows.Add(Convert.ToString(reder[0]), Convert.ToString(reder[1]), Convert.ToString(reder[2]), Convert.ToString(reder[3]), Convert.ToString(reder[4]), Convert.ToString(reder[5]), Convert.ToString(reder[6]), Convert.ToString(reder[7]));


                }
                reder.Close();
                com.CommandText = "select  employees.emp_fname , employees.emp_lname , employees.emp_father , employees.emp_mother , employees.adress ,  employees.phone ,department.dep_name ,   employees.family_situ , employees.kids , employees.nationality , employees.certificate , employees.cer_date , employees.in_date ,employees.start_date , employees.off_date , employees.level , employees.military_situ , employees.military_dep , employees.id  , employees.sex   from employees INNER JOIN department ON (department.dep_no = employees.dept_no) ";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dataGridView2.Rows.Add(Convert.ToString(reder[0]), Convert.ToString(reder[1]), Convert.ToString(reder[2]), Convert.ToString(reder[3]), Convert.ToString(reder[4]), Convert.ToString(reder[5]), Convert.ToString(reder[6]), Convert.ToString(reder[7]), reder[8].ToString(), reder[9].ToString(), reder[10].ToString(), reder[11].ToString(), reder[12].ToString(), reder[13].ToString(), reder[14].ToString(), reder[15].ToString(), reder[16].ToString(), reder[17].ToString(), reder[18].ToString(), reder[19].ToString());


                }
                reder.Close();
                com.CommandText = "select dep , emp , sal , vac , mat , teacher , search , print_emp , per from users where (user_id = @id) ";
                com.CommandType = CommandType.Text;
                SqlParameter p = new SqlParameter("@id" , Convert.ToInt32(user_id_txt.Text));
                com.Parameters.Add(p);
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    if (Convert.ToString(reder[0]) == "False")
                    {
                        button5.Enabled = false;
                        button6.Enabled = false;
                        button7.Enabled = false;
                        button8.Enabled = false;

                    }
                     if (Convert.ToString(reder[1]) == "False")
                    {
                        button2.Enabled = false;
                        button10.Enabled = false;
                        button12.Enabled = false;
                    }
                     if (Convert.ToString(reder[2]) == "False")
                     {
                         emp_fname_combo1.Enabled = false;
                         emp_lname_combo1.Enabled = false;
                        
                     }
                     if (Convert.ToString(reder[3]) == "False")
                     {
                         button13.Enabled = false;
                        
                     }
                     if (Convert.ToString(reder[4]) == "False")
                     {
                         button14.Enabled = false;
                         button26.Enabled = false;
                         button27.Enabled = false;
                         button28.Enabled = false;

                     }
                     if (Convert.ToString(reder[5]) == "False")
                     {
                         button3.Enabled = false;
                         button16.Enabled = false;
                         button31.Enabled = false;
                     }
                     if (Convert.ToString(reder[6]) == "False")
                     {
                         button21.Enabled = false;
                         button22.Enabled = false;
                       
                     }
                     if (Convert.ToString(reder[7]) == "False")
                     {
                         button4.Enabled = false;

                     }
                     if (Convert.ToString(reder[8]) == "False")
                     {
                         emp_fname_combo_5.Enabled = false;
                         emp_lname_combo_5.Enabled = false;

                     }
                }

                con.Close();
                date.Text = Convert.ToString(system_dateandtime.Value.Date.Year) + "/" + Convert.ToString(system_dateandtime.Value.Date.Month) + "/" + Convert.ToString(system_dateandtime.Value.Date.Day);
                time.Text = Convert.ToString(system_dateandtime.Value.Hour) + ":" + Convert.ToString(system_dateandtime.Value.Minute) + ":" + Convert.ToString(system_dateandtime.Value.Second);
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Convert.ToString(MessageBox.Show("هل انت متأكد من تسجيل الخروج ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)) == "Yes"))
            {
                this.Close();
            }
        }

        

        private void dep_name_combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select dep_no from department where dep_name = @name", con);
                SqlParameter p = new SqlParameter("@name", dep_name_combo1.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dept_no_txt.Text = Convert.ToString(reader[0]);



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            timer2.Enabled = false;
            emp_no_txt.Text = "";
            emp_fname_txt.Text = "";
            emp_lname_txt.Text = "";
            emp_father_txt.Text = "";
            emp_mother_txt.Text = "";
            adress_txt.Text = "";
            phone_txt.Text = "";
            in_date_txt.Text = "";
            dept_no_txt.Text = "";
            family_combo.SelectedText = "";
            kid_txt.Text = "";
            nat_txt.Text = "";
            cer_date_txt.Text = "";
            cer_txt.Text = "";
            start_date_txt.Text = "";
            off_date_txt.Text = "";
            level_txt.Text = "";
            mili_combo.SelectedText = "";
            mili_dep_txt.Text = "";
            id_txt.Text = "";
            sex_combo.SelectedItem = "";
            emp_situ.Visible = false;
            vac_date_txt.Visible = false;
            label63.Visible = false;
            label28.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (emp_fname_txt.Text == "" || emp_lname_txt.Text == "" || emp_father_txt.Text == "" || emp_mother_txt.Text == "" || adress_txt.Text == "" || phone_txt.Text == "" || in_date_txt.Text == "" || dept_no_txt.Text == "" || nat_txt.Text == "" || family_combo.SelectedItem == "" || cer_txt.Text == "" || cer_date_txt.Text == "" || start_date_txt.Text == "" || level_txt.Text == "" || id_txt.Text == "" || sex_combo.SelectedItem == "")
                    errorProvider1.SetError(button12, "عذراً يجب عدم ترك حقل فارغ");
                else
                {
                    SqlConnection mycon = new SqlConnection(Class1.x);
                    mycon.Open();
                    SqlCommand mycom2 = new SqlCommand("select emp_no from employees where ((emp_fname = @fname) and (emp_lname = @lname) and (emp_father = @father) or (id = @id) )", mycon);
                    SqlParameter p = new SqlParameter("@fname", emp_fname_txt.Text);
                    SqlParameter p1 = new SqlParameter("@lname", emp_lname_txt.Text);
                    SqlParameter p2 = new SqlParameter("@father", emp_father_txt.Text);
                    SqlParameter p3 = new SqlParameter("@id", Convert.ToInt32(id_txt.Text));
                    mycom2.CommandType = CommandType.Text;
                    mycom2.Parameters.Add(p);
                    mycom2.Parameters.Add(p1);
                    mycom2.Parameters.Add(p2);
                    mycom2.Parameters.Add(p3);
                    SqlDataReader myreader = mycom2.ExecuteReader();
                    if (myreader.HasRows == true)
                    {
                        errorProvider1.SetError(button12, "عذراً هذا الموظف او الرقم الذاتي موجود الرجاء التأكد");

                        mycon.Close();
                    }

                    else
                    {
                        mycon.Close();
                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("INSERT INTO employees (emp_fname , emp_lname , emp_father , emp_mother , adress , phone , start_date , dept_no , family_situ , kids , nationality , certificate , cer_date , in_date , off_date , level , military_situ , military_dep , id , sex) VALUES (@fname , @lname , @father , @mother , @adress , @phone , @start_date , @dept_no , @family , @kids , @nat , @cer , @cer_date , @in_date , @off_date , @level , @mili_situ , @mili_dep , @id2 , @sex)", mycon1);
                        SqlParameter p11 = new SqlParameter("@fname", emp_fname_txt.Text);
                        SqlParameter p4 = new SqlParameter("@lname", emp_lname_txt.Text);
                        SqlParameter p5 = new SqlParameter("@father", emp_father_txt.Text);
                        SqlParameter p6 = new SqlParameter("@mother", emp_mother_txt.Text);
                        SqlParameter p7 = new SqlParameter("@adress", adress_txt.Text);
                        SqlParameter p8 = new SqlParameter("@phone", phone_txt.Text);
                        SqlParameter p9 = new SqlParameter("@start_date", Convert.ToDateTime(start_date_txt.Text));
                        SqlParameter p10 = new SqlParameter("@dept_no", Convert.ToInt32(dept_no_txt.Text));
                        SqlParameter p12 = new SqlParameter("@family", family_combo.SelectedItem);
                        SqlParameter p13 = new SqlParameter("@kids", Convert.ToInt32(kid_txt.Text));
                        SqlParameter p14 = new SqlParameter("@nat", nat_txt.Text);
                        SqlParameter p15 = new SqlParameter("@cer", cer_txt.Text);
                        SqlParameter p16 = new SqlParameter("@cer_date", Convert.ToDateTime(cer_date_txt.Text));
                        SqlParameter p17 = new SqlParameter("@in_date", Convert.ToDateTime(in_date_txt.Text));
                        SqlParameter p18 = new SqlParameter("@off_date", off_date_txt.Text);
                        SqlParameter p19 = new SqlParameter("@level", level_txt.Text);
                        SqlParameter p20 = new SqlParameter("@mili_situ", mili_combo.SelectedItem);
                        SqlParameter p21 = new SqlParameter("@mili_dep", mili_dep_txt.Text);
                        SqlParameter p22 = new SqlParameter("@id2", Convert.ToInt32(id_txt.Text));
                        SqlParameter p23 = new SqlParameter("@sex", sex_combo.SelectedItem);
                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p11);
                        mycom1.Parameters.Add(p4);
                        mycom1.Parameters.Add(p5);
                        mycom1.Parameters.Add(p6);
                        mycom1.Parameters.Add(p7);
                        mycom1.Parameters.Add(p8);
                        mycom1.Parameters.Add(p9);
                        mycom1.Parameters.Add(p10);
                        mycom1.Parameters.Add(p12);
                        mycom1.Parameters.Add(p13);
                        mycom1.Parameters.Add(p14);
                        mycom1.Parameters.Add(p15);
                        mycom1.Parameters.Add(p16);
                        mycom1.Parameters.Add(p17);
                        mycom1.Parameters.Add(p18);
                        mycom1.Parameters.Add(p19);
                        mycom1.Parameters.Add(p20);
                        mycom1.Parameters.Add(p21);
                        mycom1.Parameters.Add(p22);
                        mycom1.Parameters.Add(p23);
                        SqlDataReader myreader1 = mycom1.ExecuteReader();
                        mycon1.Close();

                        emp_no_txt.Text = "";
                        emp_fname_txt.Text = "";
                        emp_lname_txt.Text = "";
                        emp_father_txt.Text = "";
                        emp_mother_txt.Text = "";
                        adress_txt.Text = "";
                        phone_txt.Text = "";
                        in_date_txt.Text = "";
                        dept_no_txt.Text = "";
                        family_combo.SelectedText = "";
                        kid_txt.Text = "";
                        nat_txt.Text = "";
                        cer_date_txt.Text = "";
                        cer_txt.Text = "";
                        start_date_txt.Text = "";
                        off_date_txt.Text = "";
                        level_txt.Text = "";
                        mili_combo.SelectedText = "";
                        mili_dep_txt.Text = "";
                        id_txt.Text = "";
                        sex_combo.SelectedItem = "";

                        MessageBox.Show("اسم المستخدم لهذا الموظف هي كنيته وكلمة المرور هي رقم الهاتف", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

                    }
                }
                timer2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }



        }

        

        

        private void emp_fname_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                emp_lname_combo.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  emp_lname from employees where emp_fname = @name", con);
                SqlParameter p = new SqlParameter("@name", emp_fname_combo.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_lname_combo.Items.Add(Convert.ToString(reader[0]));



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void emp_lname_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select employees.emp_no , employees.emp_fname , employees.emp_lname , employees.emp_father , employees.emp_mother , employees.adress , employees.phone ,  employees.start_date , employees.family_situ , employees.kids , employees.nationality , employees.certificate , employees.cer_date , employees.in_date , employees.off_date , employees.level , employees.military_situ , employees.military_dep , employees.id , department.dep_no , department.dep_name , employees.sex   from employees INNER JOIN department ON (department.dep_no = employees.dept_no) where ((emp_fname = @fname) and (emp_lname = @lname))", con);
                SqlParameter p = new SqlParameter("@fname", emp_fname_combo.SelectedItem);
                SqlParameter p1 = new SqlParameter("@lname", emp_lname_combo.SelectedItem);
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                sex_combo.SelectedText = "";
                while (reader.Read())
                {

                    emp_no_txt.Text = Convert.ToString(reader[0]);
                    emp_fname_txt.Text = Convert.ToString(reader[1]);
                    emp_lname_txt.Text = Convert.ToString(reader[2]);
                    emp_father_txt.Text = Convert.ToString(reader[3]);
                    emp_mother_txt.Text = Convert.ToString(reader[4]);
                    adress_txt.Text = Convert.ToString(reader[5]);
                    phone_txt.Text = Convert.ToString(reader[6]);
                    start_date_txt.Text = Convert.ToString(reader[7]);
                    dept_no_txt.Text = Convert.ToString(reader[19]);
                    dep_name_combo1.Text = Convert.ToString(reader[20]);
                    family_combo.SelectedItem = Convert.ToString(reader[8]);
                    kid_txt.Text = Convert.ToString(reader[9]);
                    nat_txt.Text = Convert.ToString(reader[10]);
                    cer_txt.Text = Convert.ToString(reader[11]);
                    cer_date_txt.Text = Convert.ToString(reader[12]);
                    in_date_txt.Text = Convert.ToString(reader[13]);
                    off_date_txt.Text = Convert.ToString(reader[14]);
                    level_txt.Text = Convert.ToString(reader[15]);
                    mili_combo.SelectedItem = Convert.ToString(reader[16]);
                    mili_dep_txt.Text = Convert.ToString(reader[17]);
                    id_txt.Text = Convert.ToString(reader[18]);
                    sex_combo.SelectedItem = Convert.ToString(reader[21]);
                }
                reader.Close();
                com.CommandText = "select vac_type , vac_end from vacations where (emp_no = @num) and (vac_start < @start) and (vac_end > @end) ";
                SqlParameter p100 = new SqlParameter("@num" , Convert.ToInt32(emp_no_txt.Text));
                SqlParameter p101 = new SqlParameter("@start", System.DateTime.Now);
                SqlParameter p102 = new SqlParameter("@end", System.DateTime.Now);
                com.Parameters.Add(p100);
                com.Parameters.Add(p101);
                com.Parameters.Add(p102);
                reader = com.ExecuteReader();
                label28.Visible = true;
                emp_situ.Visible = true;
                if (reader.HasRows == false)
                {
                    
                    emp_situ.Text = "مداوم";
                }
                else
                {
                    while (reader.Read())
                    {
                        label63.Visible = true;
                        vac_date_txt.Visible = true;
                        emp_situ.Text = "إجازة" + " " + Convert.ToString(reader[0]);
                        vac_date_txt.Text = Convert.ToString(reader[1]);
                    }
                }
                
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (dept_no_txt.Text == "" || emp_no_txt.Text == "")
                {
                    errorProvider1.SetError(button2, "عذراً لا يوجد بيانات لتعديلها الرجاء التأكد");
                }


                else if (emp_fname_txt.Text == "" || emp_lname_txt.Text == "" || emp_father_txt.Text == "" || emp_mother_txt.Text == "" || adress_txt.Text == "" || phone_txt.Text == "" || in_date_txt.Text == "" || dept_no_txt.Text == "" || nat_txt.Text == "" || family_combo.SelectedItem == "" || cer_txt.Text == "" || cer_date_txt.Text == "" || mili_combo.SelectedItem == "" || mili_dep_txt.Text == "" || start_date_txt.Text == "" || level_txt.Text == "" || id_txt.Text == "" || sex_combo.SelectedItem == "")
                    errorProvider1.SetError(button2, "عذراً يجب عدم ترك حقل فارغ");
                else
                {
                    SqlConnection mycon = new SqlConnection(Class1.x);
                    mycon.Open();
                    SqlCommand mycom2 = new SqlCommand("select emp_no from employees where ((emp_fname = @fname) and (emp_lname = @lname) and (emp_father = @father) or (id = @id) )", mycon);
                    SqlParameter p = new SqlParameter("@fname", emp_fname_txt.Text);
                    SqlParameter p1 = new SqlParameter("@lname", emp_lname_txt.Text);
                    SqlParameter p2 = new SqlParameter("@father", emp_father_txt.Text);
                    SqlParameter p25 = new SqlParameter("@id", Convert.ToInt32(id_txt.Text));
                    mycom2.CommandType = CommandType.Text;
                    mycom2.Parameters.Add(p);
                    mycom2.Parameters.Add(p1);
                    mycom2.Parameters.Add(p2);
                    mycom2.Parameters.Add(p25);
                    SqlDataReader myreader = mycom2.ExecuteReader();
                    if (myreader.HasRows == true)
                    {
                        errorProvider1.SetError(button2, "عذراً هذا الموظف او الرقم الذاتي موجود الرجاء التأكد");

                        mycon.Close();
                    }
                    else
                    {

                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("UPDATE employees SET emp_fname = @fname,emp_lname = @lname , emp_father = @father , emp_mother = @mother , adress = @adress , phone = @phone , start_date = @start_date , family_situ = @family , kids = @kids , nationality = @nat , certificate = @cer , cer_date = @cer_date , in_date = @in_date , off_date = @off_date , level = @level , military_situ = @mili , military_dep = @mili_dep , id = @id , dept_no  = @dept_no , sex = @sex WHERE (emp_no = @id1) ", mycon1);
                        SqlParameter p3 = new SqlParameter("@fname", emp_fname_txt.Text);
                        SqlParameter p4 = new SqlParameter("@lname", emp_lname_txt.Text);
                        SqlParameter p5 = new SqlParameter("@father", emp_father_txt.Text);
                        SqlParameter p6 = new SqlParameter("@mother", emp_mother_txt.Text);
                        SqlParameter p7 = new SqlParameter("@adress", adress_txt.Text);
                        SqlParameter p8 = new SqlParameter("@phone", phone_txt.Text);
                        SqlParameter p9 = new SqlParameter("@start_date", Convert.ToDateTime(start_date_txt.Text));
                        SqlParameter p10 = new SqlParameter("@dept_no", Convert.ToInt32(dept_no_txt.Text));
                        SqlParameter p11 = new SqlParameter("@id1", Convert.ToInt32(emp_no_txt.Text));
                        SqlParameter p12 = new SqlParameter("@family", family_combo.SelectedItem);
                        SqlParameter p13 = new SqlParameter("@kids", Convert.ToInt32(kid_txt.Text));
                        SqlParameter p14 = new SqlParameter("@nat", nat_txt.Text);
                        SqlParameter p15 = new SqlParameter("@cer", cer_txt.Text);
                        SqlParameter p16 = new SqlParameter("@cer_date", Convert.ToDateTime(cer_date_txt.Text));
                        SqlParameter p17 = new SqlParameter("@in_date", Convert.ToDateTime(in_date_txt.Text));
                        SqlParameter p18 = new SqlParameter("@off_date", off_date_txt.Text);
                        SqlParameter p19 = new SqlParameter("@level", level_txt.Text);
                        SqlParameter p20 = new SqlParameter("@mili", mili_combo.SelectedItem);
                        SqlParameter p21 = new SqlParameter("@mili_dep", mili_dep_txt.Text);
                        SqlParameter p22 = new SqlParameter("@id", Convert.ToInt32(id_txt.Text));
                        SqlParameter p23 = new SqlParameter("@sex", sex_combo.SelectedItem);

                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p3);
                        mycom1.Parameters.Add(p4);
                        mycom1.Parameters.Add(p5);
                        mycom1.Parameters.Add(p6);
                        mycom1.Parameters.Add(p7);
                        mycom1.Parameters.Add(p8);
                        mycom1.Parameters.Add(p9);
                        mycom1.Parameters.Add(p10);
                        mycom1.Parameters.Add(p11);
                        mycom1.Parameters.Add(p12);
                        mycom1.Parameters.Add(p13);
                        mycom1.Parameters.Add(p14);
                        mycom1.Parameters.Add(p15);
                        mycom1.Parameters.Add(p16);
                        mycom1.Parameters.Add(p17);
                        mycom1.Parameters.Add(p18);
                        mycom1.Parameters.Add(p19);
                        mycom1.Parameters.Add(p20);
                        mycom1.Parameters.Add(p21);
                        mycom1.Parameters.Add(p22);
                        mycom1.Parameters.Add(p23);


                        SqlDataReader myreader1 = mycom1.ExecuteReader();


                        emp_no_txt.Text = "";
                        emp_fname_txt.Text = "";
                        emp_lname_txt.Text = "";
                        emp_father_txt.Text = "";
                        emp_mother_txt.Text = "";
                        adress_txt.Text = "";
                        phone_txt.Text = "";
                        in_date_txt.Text = "";
                        dept_no_txt.Text = "";
                        family_combo.SelectedText = "";
                        kid_txt.Text = "";
                        nat_txt.Text = "";
                        cer_date_txt.Text = "";
                        cer_txt.Text = "";
                        start_date_txt.Text = "";
                        off_date_txt.Text = "";
                        level_txt.Text = "";
                        mili_combo.SelectedText = "";
                        mili_dep_txt.Text = "";
                        id_txt.Text = "";
                        sex_combo.SelectedItem = "";
                        emp_situ.Visible = false;
                        vac_date_txt.Visible = false;
                        label63.Visible = false;
                        label28.Visible = false;

                        toolTip1.Show("تم التعديل بنجاح", button2);
                    }
                }
                timer2.Enabled = true;
            }

            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView4.Rows.Clear();
                dataGridView1.Rows.Clear();
                dep_name_combo.Items.Clear();
                dep_name_combo1.Items.Clear();
                emp_fname_combo.Items.Clear();
                emp_fname_combo1.Items.Clear();
                emp_fname_combo2.Items.Clear();
                emp_fname_combo3.Items.Clear();
                emp_fname_combo4.Items.Clear();
                mat_name_combo.Items.Clear();
                mat_name_combo2.Items.Clear();
                dataGridView2.Rows.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select dep_name from department ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dep_name_combo.Items.Add(reder[0]);
                    dep_name_combo1.Items.Add(reder[0]);
                    dep_name_combo4.Items.Add(reder[0]);

                }
                reder.Close();
                com.CommandText = "select emp_fname from employees group by emp_fname";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    emp_fname_combo.Items.Add(reder[0]);
                    emp_fname_combo1.Items.Add(reder[0]);
                    emp_fname_combo2.Items.Add(reder[0]);
                    emp_fname_combo3.Items.Add(reder[0]);
                    emp_fname_combo_5.Items.Add(reder[0]);
                    emp_fname_combo4.Items.Add(reder[0]);



                }
                reder.Close();
                com.CommandText = "select mat_name from materials";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    mat_name_combo.Items.Add(reder[0]);
                    mat_name_combo2.Items.Add(reder[0]);


                }
                reder.Close();
                com.CommandText = "select dep_name , descreption from department";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dataGridView1.Rows.Add(reder[0], reder[1]);


                }
                reder.Close();
                com.CommandText = "select materials.mat_name , materials.descreption , materials.year , materials.simester , materials.mat_type , materials.houres_1 , materials.houres_2 , department.dep_name from materials INNER JOIN department ON (materials.dep_no = department.dep_no)";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dataGridView4.Rows.Add(Convert.ToString(reder[0]), Convert.ToString(reder[1]), Convert.ToString(reder[2]), Convert.ToString(reder[3]), Convert.ToString(reder[4]), Convert.ToString(reder[5]), Convert.ToString(reder[6]), Convert.ToString(reder[7]));


                }
                reder.Close();
                com.CommandText = "select  employees.emp_fname , employees.emp_lname , employees.emp_father , employees.emp_mother , employees.adress ,  employees.phone ,department.dep_name ,   employees.family_situ , employees.kids , employees.nationality , employees.certificate , employees.cer_date , employees.in_date ,employees.start_date , employees.off_date , employees.level , employees.military_situ , employees.military_dep , employees.id  , employees.sex   from employees INNER JOIN department ON (department.dep_no = employees.dept_no) ";
                com.CommandType = CommandType.Text;
                reder = com.ExecuteReader();
                while (reder.Read())
                {
                    dataGridView2.Rows.Add(Convert.ToString(reder[0]), Convert.ToString(reder[1]), Convert.ToString(reder[2]), Convert.ToString(reder[3]), Convert.ToString(reder[4]), Convert.ToString(reder[5]), Convert.ToString(reder[6]), Convert.ToString(reder[7]), reder[8].ToString(), reder[9].ToString(), reder[10].ToString(), reder[11].ToString(), reder[12].ToString(), reder[13].ToString(), reder[14].ToString(), reder[15].ToString(), reder[16].ToString(), reder[17].ToString(), reder[18].ToString(), reder[19].ToString());


                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void emp_fname_combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                emp_lname_combo1.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  emp_lname from employees where emp_fname = @name", con);
                SqlParameter p = new SqlParameter("@name", emp_fname_combo1.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_lname_combo1.Items.Add(Convert.ToString(reader[0]));



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void emp_lname_combo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select emp_no , certificate , id , level from employees where ((emp_fname = @fname) and (emp_lname = @lname))", con);
                SqlParameter p = new SqlParameter("@fname", emp_fname_combo1.SelectedItem);
                SqlParameter p1 = new SqlParameter("@lname", emp_lname_combo1.SelectedItem);
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_no_txt2.Text = Convert.ToString(reader[0]);
                    cer_txt2.Text = Convert.ToString(reader[1]);
                    id_txt2.Text = Convert.ToString(reader[2]);
                    level_txt2.Text = Convert.ToString(reader[3]);
                }
                reader.Close();
                com.CommandText = "select sal_no , salary , notes , sal_number from salary where emp_no = @emp_no";
                SqlParameter p3 = new SqlParameter("@emp_no", Convert.ToInt32(emp_no_txt2.Text));
                com.Parameters.Add(p3);
                reader = com.ExecuteReader();
                if (reader.HasRows == false)
                {
                    button18.Enabled = true;
                    button17.Enabled = false;
                }
                else
                {
                    while (reader.Read())
                    {
                        button17.Enabled = true;
                        button18.Enabled = false;
                        sal_no_txt.Text = Convert.ToString(reader[0]);
                        salary_txt.Text = Convert.ToString(reader[1]);
                        notes_txt.Text = Convert.ToString(reader[2]);
                        sal_num_txt.Text = Convert.ToString(reader[3]);
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }


        }

       
        

       
        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (emp_no_txt2.Text == "" || salary_txt.Text == "" || sal_num_txt.Text == "")
                {
                    errorProvider1.SetError(button18, "عذراً يجب عدم ترك حقل فارغ");
                }
                else
                {

                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("INSERT INTO salary (emp_no , salary , notes , sal_number) VALUES (@emp_no , @salary , @notes , @sal_num)", mycon1);
                    SqlParameter p = new SqlParameter("@emp_no", Convert.ToInt32(emp_no_txt2.Text));
                    SqlParameter p1 = new SqlParameter("@salary", Convert.ToInt32(salary_txt.Text));
                    SqlParameter p2 = new SqlParameter("@sal_num", Convert.ToInt32(sal_num_txt.Text));
                    SqlParameter p3 = new SqlParameter("@notes", notes_txt.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p);
                    mycom1.Parameters.Add(p1);
                    mycom1.Parameters.Add(p2);
                    mycom1.Parameters.Add(p3);
                    
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    mycon1.Close();
                    emp_no_txt2.Text = "";
                    salary_txt.Text = "";
                    sal_num_txt.Text = "";
                    notes_txt.Text = "";
                    emp_fname_combo1.Text = "";
                    emp_lname_combo1.Text = "";
                    cer_txt2.Text = "";
                    id_txt2.Text = "";
                    level_txt2.Text = "";
                    toolTip1.Show("تم الحفظ بنجاح",button18);

                }

                timer2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }


        }

        private void button19_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            timer2.Enabled = false;
            emp_no_txt2.Text = "";
            salary_txt.Text = "";
            sal_num_txt.Text = "";
            notes_txt.Text = "";
            emp_fname_combo1.Text = "";
            emp_lname_combo1.Text = "";
            cer_txt2.Text = "";
            id_txt2.Text = "";
            level_txt2.Text = "";
            button18.Enabled = false;
            button17.Enabled = false;
            sal_no_txt.Text = "";
        }

        

        private void emp_fname_combo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                emp_lname_combo2.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  emp_lname from employees where emp_fname = @name", con);
                SqlParameter p = new SqlParameter("@name", emp_fname_combo2.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_lname_combo2.Items.Add(Convert.ToString(reader[0]));



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void emp_lname_combo2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select emp_no from employees where (emp_fname = @fname) and (emp_lname = @lname)  ", con);
                SqlParameter p = new SqlParameter("@fname", emp_fname_combo2.SelectedItem);
                SqlParameter p1 = new SqlParameter("@lname", emp_lname_combo2.SelectedItem);
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                dataGridView5.Rows.Clear();
                while (reader.Read())
                {

                    emp_no_txt3.Text = Convert.ToString(reader[0]);
                   

                }
                reader.Close();
                com.CommandText = "select vac_type , vac_start , vac_end , periode from vacations where (emp_no = @no)";
                SqlParameter p2 = new SqlParameter("@no" , Convert.ToInt32(emp_no_txt3.Text));
                com.Parameters.Add(p2);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView5.Rows.Add(Convert.ToString(emp_fname_combo2.SelectedItem) + " " + Convert.ToString(emp_lname_combo2.SelectedItem), Convert.ToString(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), Convert.ToString(reader[3]) + "يوم / أيام");
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void vac_start_picker_ValueChanged(object sender, EventArgs e)
        {
            vac_start_txt.Text = Convert.ToString(vac_start_picker.Text);
            
        }



        
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (emp_no_txt3.Text == "" || vac_type_combe.Text == "" || vac_start_txt.Text == "" || vac_end_txt.Text == "" || periode_txt.Text == "")
                {
                    errorProvider1.SetError(button13 , "عذراً يجب عدم ترك حقل فارغ");
                }
                


                    else
                    {

                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("INSERT INTO vacations (emp_no , vac_type , vac_start , vac_end , periode) VALUES (@emp_no , @vac_type , @vac_start , @vac_end , @periode )", mycon1);
                        SqlParameter p = new SqlParameter("@emp_no", Convert.ToInt32(emp_no_txt3.Text));
                        SqlParameter p1 = new SqlParameter("@vac_type", Convert.ToString(vac_type_combe.SelectedItem));
                        SqlParameter p2 = new SqlParameter("@vac_start", Convert.ToDateTime(vac_start_txt.Text));
                        SqlParameter p3 = new SqlParameter("@vac_end", Convert.ToDateTime(vac_end_txt.Text));
                        SqlParameter p4 = new SqlParameter("@periode", periode_txt.Text);

                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p);
                        mycom1.Parameters.Add(p1);
                        mycom1.Parameters.Add(p2);
                        mycom1.Parameters.Add(p3);
                        mycom1.Parameters.Add(p4);

                        SqlDataReader myreader1 = mycom1.ExecuteReader();
                        mycon1.Close();
                        emp_no_txt3.Text = "";

                        vac_start_txt.Text = "";
                        vac_end_txt.Text = "";
                        vac_type_combe.SelectedText = "";
                        periode_txt.Text = "";
                        emp_fname_combo2.SelectedText = "";
                        emp_lname_combo2.SelectedText = "";


                        toolTip1.Show("تم الحفظ بنجاح",button13);

                    }

                timer2.Enabled = true;
                }
            
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        
            
        
        
    }

        private void button23_Click(object sender, EventArgs e)
        {
            emp_no_txt3.Text = "";
            errorProvider1.Clear();
            timer2.Enabled = false;
            vac_start_txt.Text = "";
            vac_end_txt.Text = "";
            
            periode_txt.Text = "";
            emp_fname_combo2.SelectedText = "";
            emp_lname_combo2.SelectedText = "";

            
        }

       

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                
                SqlConnection mycon = new SqlConnection(Class1.x);
                mycon.Open();
                SqlCommand mycom2 = new SqlCommand("select * from materials where (mat_name = @name)", mycon);
                SqlParameter p15 = new SqlParameter("@name", mat_name_txt.Text);
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p15);
                SqlDataReader myreader = mycom2.ExecuteReader();
                if (myreader.HasRows)
                {
                    errorProvider1.SetError(button28, "عذراً هذه المادة موجودة الرجاء التأكد");
                    mycon.Close();
                }
                else if (mat_name_txt.Text == "" || descreption_txt.Text == "" || year_combo.SelectedItem == "" || simister_combo.SelectedItem == "" || dep_no_txt4.Text == "" || houres_1_txt.Text == "" || houres_2_txt.Text == "" || type_com.SelectedItem == "" )
                    errorProvider1.SetError(button28, "عذراً يجب عدم ترك حقل فارغ");
                else
                {

                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("INSERT INTO materials (mat_name ,descreption , year , simester , mat_type , houres_1 , houres_2 , dep_no) VALUES (@name1, @desc , @year , @simester , @type , @h1 , @h2 , @dep_no)", mycon1);
                    SqlParameter p = new SqlParameter("@name1", mat_name_txt.Text);
                    SqlParameter p1 = new SqlParameter("@desc", descreption_txt.Text);
                    SqlParameter p2 = new SqlParameter("@year", year_combo.Text);
                    SqlParameter p3 = new SqlParameter("@simester", simister_combo.Text);
                    SqlParameter p4 = new SqlParameter("@type", type_com.SelectedItem);
                    SqlParameter p5 = new SqlParameter("@h1", houres_1_txt.Text);
                    SqlParameter p6 = new SqlParameter("@h2", houres_2_txt.Text);
                    SqlParameter p7 = new SqlParameter("@dep_no", Convert.ToInt32(dep_no_txt4.Text));
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p);
                    mycom1.Parameters.Add(p1);
                    mycom1.Parameters.Add(p2);
                    mycom1.Parameters.Add(p3);
                    mycom1.Parameters.Add(p4);
                    mycom1.Parameters.Add(p5);
                    mycom1.Parameters.Add(p6);
                    mycom1.Parameters.Add(p7);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    myreader1.Close();

                    mat_no_txt.Text = "";
                    mat_name_txt.Text = "";
                    descreption_txt.Text = "";
                    year_combo.Text = "";
                    simister_combo.Text = "";
                    dep_no_txt4.Text = "";
                    houres_1_txt.Text = "";
                    houres_2_txt.Text = "";
                    type_com.SelectedText = "";





                    toolTip1.Show("تم الحفظ بنجاح" , button28);

                }

                timer2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            timer2.Enabled = false;
            mat_no_txt.Text = "";
            mat_name_txt.Text = "";
            descreption_txt.Text = "";
            year_combo.Text = "";
            simister_combo.Text = "";
        }

        private void mat_name_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select materials.mat_no , materials.mat_name , materials.descreption , materials.year , materials.simester , materials.mat_type , materials.houres_1 , materials.houres_2 , materials.dep_no , department.dep_name from materials INNER JOIN department ON (materials.dep_no = department.dep_no) where mat_name = @name", con);
                SqlParameter p = new SqlParameter("@name", mat_name_combo.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    mat_no_txt.Text = Convert.ToString(reader[0]);
                    mat_name_txt.Text = Convert.ToString(reader[1]);
                    descreption_txt.Text = Convert.ToString(reader[2]);
                    year_combo.Text = Convert.ToString(reader[3]);
                    simister_combo.Text = Convert.ToString(reader[4]);
                    type_com.Text = Convert.ToString(reader[5]);
                    houres_1_txt.Text = Convert.ToString(reader[6]);
                    houres_2_txt.Text = Convert.ToString(reader[7]);
                    dep_no_txt4.Text = Convert.ToString(reader[8]);
                    dep_name_combo4.Text = Convert.ToString(reader[9]);
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                

                if (mat_no_txt.Text == "")
                {
                    errorProvider1.SetError(button27, "عذراً لا يوجد مادة لتعديلها الرجاء التأكد");
                }

                else if (mat_name_txt.Text == "" || descreption_txt.Text == "" || year_combo.SelectedItem == "" || simister_combo.SelectedItem == "" || dep_no_txt4.Text == "" || houres_1_txt.Text == "" || houres_2_txt.Text == "" || type_com.SelectedItem == "")
                    errorProvider1.SetError(button27, "عذراً لا يجب ترك حقل فارغ");
                    else
                    {
                       
                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("UPDATE materials SET mat_name = @name,descreption = @desc , year = @year , simester = @simester , mat_type = @mat , houres_1 = @h1 , houres_2 = @h2 , dep_no = @dep_no WHERE (mat_no = @id1) ", mycon1);
                        SqlParameter p = new SqlParameter("@name", mat_name_txt.Text);
                        SqlParameter p1 = new SqlParameter("@desc", descreption_txt.Text);
                        SqlParameter p2 = new SqlParameter("@year", year_combo.Text);
                        
                        SqlParameter p3 = new SqlParameter("@simester", simister_combo.Text);
                        SqlParameter p4 = new SqlParameter("@mat", type_com.SelectedItem);
                        SqlParameter p5 = new SqlParameter("@h1", houres_1_txt.Text);
                        SqlParameter p6 = new SqlParameter("@h2", houres_2_txt.Text);
                        SqlParameter p7 = new SqlParameter("@dep_no", Convert.ToInt32(dep_no_txt4.Text));
                        SqlParameter p8 = new SqlParameter("@id1", Convert.ToInt32(mat_no_txt.Text));
                    
                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p);
                        mycom1.Parameters.Add(p1);
                        mycom1.Parameters.Add(p2);
                        mycom1.Parameters.Add(p3);
                        mycom1.Parameters.Add(p4);
                        mycom1.Parameters.Add(p5);
                        mycom1.Parameters.Add(p6);
                        mycom1.Parameters.Add(p7);
                        mycom1.Parameters.Add(p8);
                        
                        SqlDataReader myreader1 = mycom1.ExecuteReader();

                        mat_no_txt.Text = "";
                        mat_name_txt.Text = "";
                        descreption_txt.Text = "";
                        year_combo.Text = "";
                        simister_combo.Text = "";



                        toolTip1.Show("تم التعديل بنجاح", button27);
                    }
                timer2.Enabled = true;
            }

            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                
                if (mat_no_txt.Text == "")
                {
                    errorProvider1.SetError(button26, "لا يوجد بيانات لحذفها");
                }
               
                   
                    else if ((Convert.ToString(MessageBox.Show("  يوجد بيانات مرتبطة بهذه المادة سيتم حذفها أيضاً ,هل أنت متأكد من الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes"))
                    {
                         SqlConnection con = new SqlConnection(Class1.x);
                        con.Open();
                        SqlCommand com = new SqlCommand("DELETE FROM teacher_material where mat_no = @id", con);
                        com.CommandType = CommandType.Text;
                        SqlParameter p1 = new SqlParameter("@id", Convert.ToInt32(mat_no_txt.Text));
                        com.Parameters.Add(p1);
                        SqlDataReader  reader = com.ExecuteReader();
                        reader.Close();
                        com.CommandText = "DELETE FROM materials where mat_no = @id1";
                        com.Connection = con;
                        SqlParameter p2 = new SqlParameter("@id1", Convert.ToInt32(mat_no_txt.Text));
                        com.Parameters.Add(p2);
                        reader = com.ExecuteReader();
                        con.Close();
                        mat_no_txt.Text = "";
                        mat_name_txt.Text = "";
                        descreption_txt.Text = "";
                        year_combo.Text = "";
                        simister_combo.Text = "";
                        toolTip1.Show("تم الحذف بنجاح" , button26);
                    }
                timer2.Enabled = true;
                }
            

            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        
        }

      

        private void emp_fname_combo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                emp_lname_combo3.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  emp_lname from employees where emp_fname = @name", con);
                SqlParameter p = new SqlParameter("@name", emp_fname_combo3.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_lname_combo3.Items.Add(Convert.ToString(reader[0]));



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void emp_lname_combo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con1 = new SqlConnection(Class1.x);
                con1.Open();
                SqlCommand com1 = new SqlCommand("select emp_no from employees where ((emp_fname = @fname) and (emp_lname = @lname))", con1);
                SqlParameter p10 = new SqlParameter("@fname", emp_fname_combo3.SelectedItem);
                SqlParameter p11 = new SqlParameter("@lname", emp_lname_combo3.SelectedItem);
                com1.Parameters.Add(p10);
                com1.Parameters.Add(p11);
                com1.CommandType = CommandType.Text;
                SqlDataReader reader1 = com1.ExecuteReader();
                while (reader1.Read())
                {

                    emp_no_txt4.Text = Convert.ToString(reader1[0]);

                }

                con1.Close();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select materials.mat_name , teacher_material.study_year , department.dep_name from  materials INNER JOIN teacher_material ON (materials.mat_no = teacher_material.mat_no) INNER JOIN department ON (materials.dep_no = department.dep_no)  where (teacher_material.emp_no = @no)", con);
                SqlParameter p = new SqlParameter("@no",Convert.ToInt32(emp_no_txt4.Text));
                
                com.Parameters.Add(p);
                
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                dataGridView6.Rows.Clear();
                while (reader.Read())
                {

                    
                    dataGridView6.Rows.Add(Convert.ToString(emp_fname_combo3.SelectedItem) + " " + Convert.ToString(emp_lname_combo3.SelectedItem), Convert.ToString(reader[0]), Convert.ToString(reader[2]), Convert.ToString(reader[1]));

                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }


        private void mat_name_combo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con1 = new SqlConnection(Class1.x);
                con1.Open();
                SqlCommand com1 = new SqlCommand("select  mat_no from materials where mat_name = @name", con1);
                SqlParameter p2 = new SqlParameter("@name", mat_name_combo2.SelectedItem);
                com1.Parameters.Add(p2);
                com1.CommandType = CommandType.Text;
                SqlDataReader reader1 = com1.ExecuteReader();
                while (reader1.Read())
                {
                    mat_num_txt2.Text = Convert.ToString(reader1[0]);
                }
                con1.Close();

                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  study_year from teacher_material  where (emp_no = @no) and (mat_no = @no1)", con);
                SqlParameter p = new SqlParameter("@no1", Convert.ToInt32(mat_num_txt2.Text));
                SqlParameter p1 = new SqlParameter("@no", Convert.ToInt32(emp_no_txt4.Text));
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {


                        study_year_txt.Text = Convert.ToString(reader[0]);
                        button3.Enabled = true;
                    }

                }
                else
                {
                    button3.Enabled = false;
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (mat_num_txt2.Text == "" || emp_no_txt4.Text == "" || study_year_txt.Text == "")
                    errorProvider1.SetError(button31, "عذراً يجب عدم ترك حقل فارغ");
                else
                {
                    SqlConnection mycon = new SqlConnection(Class1.x);
                    mycon.Open();
                    SqlCommand mycom2 = new SqlCommand("select * from teacher_material where ((mat_no = @mat_no) and (emp_no = @emp_no) and (study_year = @year) ) ", mycon);
                    SqlParameter p15 = new SqlParameter("@mat_no", Convert.ToInt32(mat_num_txt2.Text));
                    SqlParameter p14 = new SqlParameter("@emp_no", Convert.ToInt32(emp_no_txt4.Text));
                    SqlParameter p13 = new SqlParameter("@year", study_year_txt.Text);
                    mycom2.CommandType = CommandType.Text;
                    mycom2.Parameters.Add(p15);
                    mycom2.Parameters.Add(p14);
                    mycom2.Parameters.Add(p13);
                    SqlDataReader myreader = mycom2.ExecuteReader();
                    if (myreader.HasRows)
                    {
                        errorProvider1.SetError(button31, "عذراً هذا المدرس في العام الدراسي الحالي يدرس نفس المادة الرجاء الانتباه");

                        mycon.Close();
                    }

                    else
                    {

                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("INSERT INTO teacher_material (mat_no ,emp_no , study_year) VALUES ( @mat_no1 , @emp_no1 , @year1)", mycon1);
                        SqlParameter p12 = new SqlParameter("@mat_no1", Convert.ToInt32(mat_num_txt2.Text));
                        SqlParameter p11 = new SqlParameter("@emp_no1", Convert.ToInt32(emp_no_txt4.Text));
                        SqlParameter p10 = new SqlParameter("@year1", study_year_txt.Text);
                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p12);
                        mycom1.Parameters.Add(p11);
                        mycom1.Parameters.Add(p10);
                        SqlDataReader myreader1 = mycom1.ExecuteReader();
                        myreader1.Close();

                        mat_num_txt2.Text = "";
                        emp_no_txt4.Text = "";
                        study_year_txt.Text = "";
                        toolTip1.Show("تم الحفظ بنجاح", button31);

                    }
                }
                timer2.Enabled = true;

            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (emp_no_txt.Text == "")
                {
                    errorProvider1.SetError(button10,"عذراً لا يوجد بيانات لحذفها");
                }
               
                    else if ((Convert.ToString(MessageBox.Show(" يوجد بيانات مرتبطة بهذا الموظف في جداول أخرى سيتم حذفها ,هل أنت متأكد من الحذف؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes"))
                    {
                        SqlConnection con = new SqlConnection (Class1.x);
                        con.Open();
                        SqlCommand com = new SqlCommand();
                        com.CommandText = "DELETE FROM teacher_material WHERE (emp_no = @id)";
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        SqlParameter p1 = new SqlParameter("@id", Convert.ToInt32(emp_no_txt.Text));
                        com.Parameters.Add(p1);
                        SqlDataReader reader;
                        reader = com.ExecuteReader();
                        reader.Close();
                        com.CommandText = "DELETE FROM vacations WHERE (emp_no = @id1)";
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        SqlParameter p2 = new SqlParameter("@id1", Convert.ToInt32(emp_no_txt.Text));
                        com.Parameters.Add(p2);
                        reader = com.ExecuteReader();
                        reader.Close();
                        com.CommandText = "DELETE FROM salary WHERE (emp_no = @id2)";
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        SqlParameter p3 = new SqlParameter("@id2", Convert.ToInt32(emp_no_txt.Text));
                        com.Parameters.Add(p3);
                        reader = com.ExecuteReader();
                        reader.Close();
                        
                        com.CommandText = "DELETE FROM users WHERE (emp_no = @id4)";
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        SqlParameter p5 = new SqlParameter("@id4", Convert.ToInt32(emp_no_txt.Text));
                        com.Parameters.Add(p5);
                        reader = com.ExecuteReader();
                        reader.Close();
                        com.CommandText = "DELETE FROM employees WHERE (emp_no = @id3)";
                        com.Connection = con;
                        com.CommandType = CommandType.Text;
                        SqlParameter p4 = new SqlParameter("@id3", Convert.ToInt32(emp_no_txt.Text));
                        com.Parameters.Add(p4);
                        reader = com.ExecuteReader();
                        
                        con.Close();
                        sex_combo.SelectedItem = "";
                        emp_no_txt.Text = "";
                        emp_fname_txt.Text = "";
                        emp_lname_txt.Text = "";
                        emp_father_txt.Text = "";
                        emp_mother_txt.Text = "";
                        adress_txt.Text = "";
                        phone_txt.Text = "";
                        in_date_txt.Text = "";
                        dept_no_txt.Text = "";
                        family_combo.SelectedText = "";
                        kid_txt.Text = "";
                        nat_txt.Text = "";
                        cer_date_txt.Text = "";
                        cer_txt.Text = "";
                        start_date_txt.Text = "";
                        off_date_txt.Text = "";
                        level_txt.Text = "";
                        mili_combo.SelectedText = "";
                        mili_dep_txt.Text = "";
                        id_txt.Text = "";
                        emp_situ.Visible = false;
                        vac_date_txt.Visible = false;
                        label63.Visible = false;
                        label28.Visible = false;
                        toolTip1.Show("تم الحذف بنجاح" , button10);
                    }
                timer2.Enabled = true;
            }


            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

       

        private void emp_fname_combo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                emp_lname_combo4.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  emp_lname from employees where emp_fname = @name", con);
                SqlParameter p = new SqlParameter("@name", emp_fname_combo4.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_lname_combo4.Items.Add(Convert.ToString(reader[0]));



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        
        }

        private void emp_lname_combo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select emp_no from employees where ((emp_fname = @fname) and (emp_lname = @lname))", con);
                SqlParameter p = new SqlParameter("@fname", emp_fname_combo4.SelectedItem);
                SqlParameter p1 = new SqlParameter("@lname", emp_lname_combo4.SelectedItem);
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_no_txt5.Text = Convert.ToString(reader[0]);

                }

                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }


        }

        private void general_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (general_radio.Checked == true)
            {
                groupBox7.Visible = false;
                groupBox4.Visible = false;

                dataGridView3.Columns[1].Visible = false;
                dataGridView3.Columns[2].Visible = false;
                dataGridView3.Columns[3].Visible = false;
                dataGridView3.Columns[4].Visible = false;
                dataGridView3.Columns[5].Visible = false;
                dataGridView3.Columns[6].Visible = false;
                dataGridView3.Columns[7].Visible = false;
                dataGridView3.Columns[8].Visible = false;
                dataGridView3.Columns[9].Visible = false;
                dataGridView3.Columns[10].Visible = false;
                dataGridView3.Columns[11].Visible = false;
                dataGridView3.Columns[12].Visible = false;
                dataGridView3.Columns[13].Visible = false;
                dataGridView3.Columns[14].Visible = false;
                dataGridView3.Columns[15].Visible = true;
                dataGridView3.Columns[16].Visible = true;
                dataGridView3.Columns[17].Visible = true;
                dataGridView3.Columns[18].Visible = true;
                dataGridView3.Columns[19].Visible = true;
                dataGridView3.Columns[20].Visible = true;
                dataGridView3.Columns[21].Visible = true;
                dataGridView3.Columns[22].Visible = true;
                dataGridView3.Columns[23].Visible = true;
                dataGridView3.Columns[24].Visible = true;
                dataGridView3.Columns[25].Visible = true;
                dataGridView3.Columns[26].Visible = true;
                dataGridView3.Columns[27].Visible = true;
                dataGridView3.Columns[28].Visible = true;
                dataGridView3.Columns[29].Visible = true;
                dataGridView3.Columns[30].Visible = true;
                dataGridView3.Columns[31].Visible = true;
                dataGridView3.Columns[32].Visible = true;
                
                
            }
            else if (speacial_radio.Checked == true)
            {
                groupBox7.Visible = true;
                groupBox4.Visible = false;
                
                
            }
            else
            { }
        }

        private void vac_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (vac_radio.Checked == true)
            {

                groupBox4.Visible = true;




                dataGridView3.Columns[1].Visible = false;
                dataGridView3.Columns[2].Visible = false;
                dataGridView3.Columns[3].Visible = false;
                dataGridView3.Columns[4].Visible = false;
                dataGridView3.Columns[5].Visible = false;
                dataGridView3.Columns[6].Visible = false;
                dataGridView3.Columns[7].Visible = false;
                dataGridView3.Columns[8].Visible = false;
                dataGridView3.Columns[9].Visible = false;
                dataGridView3.Columns[10].Visible = false;
                dataGridView3.Columns[11].Visible = true;
                dataGridView3.Columns[12].Visible = true;
                dataGridView3.Columns[13].Visible = true;
                dataGridView3.Columns[14].Visible = true;
                dataGridView3.Columns[15].Visible = false;
                dataGridView3.Columns[16].Visible = false;
                dataGridView3.Columns[17].Visible = false;
                dataGridView3.Columns[18].Visible = false;
                dataGridView3.Columns[19].Visible = false;
                dataGridView3.Columns[20].Visible = false;
                dataGridView3.Columns[21].Visible = false;
                dataGridView3.Columns[22].Visible = false;
                dataGridView3.Columns[23].Visible = false;
                dataGridView3.Columns[24].Visible = false;
                dataGridView3.Columns[25].Visible = false;
                dataGridView3.Columns[26].Visible = false;
                dataGridView3.Columns[27].Visible = false;
                dataGridView3.Columns[28].Visible = false;
                dataGridView3.Columns[29].Visible = false;
                dataGridView3.Columns[30].Visible = false;
                dataGridView3.Columns[31].Visible = false;
                dataGridView3.Columns[32].Visible = false;

            }
            
        }

        private void sal_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (sal_radio.Checked == true)
            {

                groupBox4.Visible = false;

                dataGridView3.Columns[1].Visible = false;
                dataGridView3.Columns[2].Visible = false;
                dataGridView3.Columns[3].Visible = false;
                dataGridView3.Columns[4].Visible = false;
                dataGridView3.Columns[5].Visible = false;
                dataGridView3.Columns[6].Visible = false;
                dataGridView3.Columns[7].Visible = false;
                dataGridView3.Columns[8].Visible = true;
                dataGridView3.Columns[9].Visible = true;
                dataGridView3.Columns[10].Visible = true;
                dataGridView3.Columns[11].Visible = false;
                dataGridView3.Columns[12].Visible = false;
                dataGridView3.Columns[13].Visible = false;
                dataGridView3.Columns[14].Visible = false;
                dataGridView3.Columns[15].Visible = false;

                dataGridView3.Columns[16].Visible = false;
                dataGridView3.Columns[17].Visible = false;
                dataGridView3.Columns[18].Visible = false;
                dataGridView3.Columns[19].Visible = false;
                dataGridView3.Columns[20].Visible = false;
                dataGridView3.Columns[21].Visible = false;
                dataGridView3.Columns[22].Visible = false;
                dataGridView3.Columns[23].Visible = false;
                dataGridView3.Columns[24].Visible = false;
                dataGridView3.Columns[25].Visible = false;
                dataGridView3.Columns[26].Visible = false;
                dataGridView3.Columns[27].Visible = false;
                dataGridView3.Columns[28].Visible = false;
                dataGridView3.Columns[29].Visible = false;
                dataGridView3.Columns[30].Visible = false;
                dataGridView3.Columns[31].Visible = false;
                dataGridView3.Columns[32].Visible = false;

            }
            
        }

        private void mat_radio_CheckedChanged(object sender, EventArgs e)
        {
             
            if (mat_radio.Checked == true)
            {

                groupBox4.Visible = false;
               
                dataGridView3.Columns[1].Visible = true;
                dataGridView3.Columns[2].Visible = true;
                dataGridView3.Columns[3].Visible = true;
                dataGridView3.Columns[4].Visible = true;
                dataGridView3.Columns[5].Visible = true;
                dataGridView3.Columns[6].Visible = true;
                dataGridView3.Columns[7].Visible = true;
                dataGridView3.Columns[8].Visible = false;
                dataGridView3.Columns[9].Visible = false;
                dataGridView3.Columns[10].Visible = false;
                dataGridView3.Columns[11].Visible = false;
                dataGridView3.Columns[12].Visible = false;
                dataGridView3.Columns[13].Visible = false;
                dataGridView3.Columns[14].Visible = false;
                dataGridView3.Columns[15].Visible = false;
                dataGridView3.Columns[16].Visible = false;
                dataGridView3.Columns[17].Visible = false;
                dataGridView3.Columns[18].Visible = false;
                dataGridView3.Columns[19].Visible = false;
                dataGridView3.Columns[20].Visible = false;
                dataGridView3.Columns[21].Visible = false;
                dataGridView3.Columns[22].Visible = false;
                dataGridView3.Columns[23].Visible = false;
                dataGridView3.Columns[24].Visible = false;
                dataGridView3.Columns[25].Visible = false;
                dataGridView3.Columns[26].Visible = false;
                dataGridView3.Columns[27].Visible = false;
                dataGridView3.Columns[28].Visible = false;
                dataGridView3.Columns[29].Visible = false;
                dataGridView3.Columns[30].Visible = false;
                dataGridView3.Columns[31].Visible = false;
                dataGridView3.Columns[32].Visible = false;
                
            }
        }

        private void speacial_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (general_radio.Checked == true)
            {
                groupBox7.Visible = false;
                groupBox4.Visible = false;
               
                
            }
            else if (speacial_radio.Checked == true)
            {
                groupBox7.Visible = true;
                groupBox4.Visible = false;
                
                
            }
            else
            { }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
                
            if (emp_no_txt5.Text == "")
            {
                errorProvider1.SetError(button21, "الرجاء اختيار اسم الموظف اولاً");
            }

            else  if (general_radio.Checked == true)
            {
                dataGridView3.Rows.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  employees.emp_father , employees.emp_mother , employees.adress , employees.phone ,  employees.start_date , employees.family_situ , employees.kids , employees.nationality , employees.certificate , employees.cer_date , employees.in_date , employees.off_date , employees.level , employees.military_situ , employees.military_dep , employees.id , department.dep_name , employees.sex   from employees INNER JOIN department ON (department.dep_no = employees.dept_no) where ((emp_fname = @fname) and (emp_lname = @lname))", con);
                SqlParameter p = new SqlParameter("@fname", emp_fname_combo4.SelectedItem);
                SqlParameter p1 = new SqlParameter("@lname", emp_lname_combo4.SelectedItem);
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                
                while (reader.Read())
                {

                    dataGridView3.Rows.Add(Convert.ToString(emp_fname_combo4.SelectedItem) + " " + Convert.ToString(emp_lname_combo4.SelectedItem), "", "", "", "", "", "", "", "", "", "", "", "", "", "", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[16].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[4].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[17].ToString());

                }
                con.Close();
            }
            
            else if (mat_radio.Checked == true)
            {
                dataGridView3.Rows.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select mat_name , descreption , year , simester , mat_type , houres_1 , houres_2 from materials INNER JOIN teacher_material ON (teacher_material.mat_no = materials.mat_no)  where teacher_material.emp_no = @id", con);
                SqlParameter p = new SqlParameter("@id", Convert.ToInt32(emp_no_txt5.Text));
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                dataGridView3.Rows.Add(emp_fname_combo4.SelectedItem+ " " + emp_lname_combo4.SelectedItem);
                while (reader.Read())
                {

                    dataGridView3.Rows.Add("", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());

                }
                con.Close();
            }
            else if (start_txt.Text == "" || end_txt.Text == "")
            {
                errorProvider1.SetError(button21, "الرجاء تحديد تاريخ البحث اولاً");
            }
            else if (vac_radio.Checked == true)
            {

                dataGridView3.Rows.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select vac_type , vac_start , vac_end , periode from vacations  where (emp_no = @id) and (vac_start >= @start) and (vac_end <= @end)", con);
                SqlParameter p = new SqlParameter("@id", Convert.ToInt32(emp_no_txt5.Text));
                SqlParameter p1 = new SqlParameter("@start", Convert.ToDateTime(start_txt.Text));
                SqlParameter p2 = new SqlParameter("@end", Convert.ToDateTime(end_txt.Text));
                com.Parameters.Add(p);
                com.Parameters.Add(p1);
                com.Parameters.Add(p2);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                dataGridView3.Rows.Add(emp_fname_combo4.SelectedItem + " " + emp_lname_combo4.SelectedItem);

                while (reader.Read())
                {

                    dataGridView3.Rows.Add("", "", "", "", "", "", "", "", "", "", "", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());

                }
                con.Close();

            }
            else if (sal_radio.Checked == true)
            {
                dataGridView3.Rows.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select salary , notes , sal_number from salary  where (emp_no = @id) ", con);
                SqlParameter p = new SqlParameter("@id", Convert.ToInt32(emp_no_txt5.Text));

                com.Parameters.Add(p);

                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                dataGridView3.Rows.Add(emp_fname_combo4.SelectedItem + " " + emp_lname_combo4.SelectedItem);

                while (reader.Read())
                {

                    dataGridView3.Rows.Add("", "", "", "", "", "", "", "", reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                }
                con.Close();

            }
            else
            {
                errorProvider1.SetError(button21, "الرجاء تحديد تاريخ البحث اولاً");
            }
            timer2.Enabled = true;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
           start_txt.Text = Convert.ToString(dateTimePicker3.Value);
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            end_txt.Text = Convert.ToString(dateTimePicker4.Value);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
             
               
                    
                    PrintJbsaDataGridView.Print_Grid(dataGridView3);
                   

                
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                
            if (study_year_txt.Text == "")
            {
                errorProvider1.SetError(button3, "عذراً يجب عدم ترك حقل فارغ");
            }   
                else
                    {
                        
                        SqlConnection mycon1 = new SqlConnection(Class1.x);
                        mycon1.Open();
                        SqlCommand mycom1 = new SqlCommand("UPDATE teacher_material SET study_year = @year WHERE (mat_no = @id1) and (emp_no = @id2)", mycon1);
                        SqlParameter p = new SqlParameter("@year", study_year_txt.Text);
                        SqlParameter p1 = new SqlParameter("@id1", Convert.ToInt32(mat_num_txt2.Text));
                        SqlParameter p2 = new SqlParameter("@id2", Convert.ToInt32(emp_no_txt4.Text));
                        mycom1.CommandType = CommandType.Text;
                        mycom1.Parameters.Add(p);
                        mycom1.Parameters.Add(p1);
                        mycom1.Parameters.Add(p2);


                        SqlDataReader myreader1 = mycom1.ExecuteReader();





                        toolTip1.Show("تم التعديل بنجاح" , button3);
                    }
            timer2.Enabled = true;
            }

            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void dep_name_combo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select dep_no from department where dep_name = @name", con);
                SqlParameter p = new SqlParameter("@name", dep_name_combo4.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    dep_no_txt4.Text = Convert.ToString(reader[0]);
                    


                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void family_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (family_combo.SelectedItem == "متزوج")
            {
                kid_txt.Visible = true;
                label30.Visible = true;
            }
            else
            {
                kid_txt.Visible = false;
                label30.Visible = false;
            }
        }

        private void cer_date_picker_ValueChanged(object sender, EventArgs e)
        {
            cer_date_txt.Text = Convert.ToString(cer_date_picker.Value);
        }

        private void in_date_picker_ValueChanged(object sender, EventArgs e)
        {
            in_date_txt.Text = Convert.ToString(in_date_picker.Value);
        }

        private void start_date_picker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (start_date_picker.Value < in_date_picker.Value)
                {
                    errorProvider1.SetError(start_date_picker, "الرجاء الانتباه تاريخ المباشرة يجب ان يكون بعد تاريخ التعيين");
                    
                }
                else
                {
                    start_date_txt.Text = Convert.ToString(start_date_picker.Value);
                }
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه ");
            }
        }

        private void off_date_picker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (off_date_picker.Value < start_date_picker.Value)
                {
                    errorProvider1.SetError(off_date_picker, "الرجاء الانتباه تاريخ الانفكاك يجب ان يكون بعد تاريخ التعيين");
                    
                }
                else
                {
                    off_date_txt.Text = Convert.ToString(off_date_picker.Value);
                }
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه ");
            }
        }

        private void kid_txt_Leave(object sender, EventArgs e)
        {
            if (kid_txt.Text == "")
            { 
            kid_txt.Text = "0";
            }
        }

        private void net_txt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (salary_txt.Text == "" || Convert.ToInt32(salary_txt.Text) < 0)
                {
                    salary_txt.Text = "0";
                }
            }
            catch 
            {
                MessageBox.Show("الرجاء الانتباه ");
            }
        }

        

        


        private void vac_end_picker1_ValueChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (vac_end_picker1.Value <= vac_start_picker.Value)
            {
                errorProvider1.SetError(vac_end_picker1, "عذراً تاريخ انتهاء الاجازة يجب ان يكون بعد تاريخ البداية");
            }
            else
            {
                vac_end_txt.Text = Convert.ToString(vac_end_picker1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PrintJbsaDataGridView.Print_Grid(dataGridView2);
        }

        

        private void button8_Click_1(object sender, EventArgs e)
        {
            PrintJbsaDataGridView.Print_Grid(dataGridView1);
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            PrintJbsaDataGridView.Print_Grid(dataGridView6);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PrintJbsaDataGridView.Print_Grid(dataGridView4);
        }

        private void mili_dep_txt_TextChanged(object sender, EventArgs e)
        {
            if (mili_dep_txt.Text == "")
            {
                mili_dep_txt.Text = " ";
            }
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;
                progressBar1.Increment(100);
                errorProvider1.Clear();
                if (salary_txt.Text == "" || sal_num_txt.Text == ""  )
                    errorProvider1.SetError(button17, "عذراً يجب عدم ترك حقل فارغ");
                else
                {

                    SqlConnection mycon1 = new SqlConnection(Class1.x);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("UPDATE salary SET salary = @salary,notes = @notes ,sal_number = @number WHERE (sal_no = @id1) ", mycon1);
                    SqlParameter p = new SqlParameter("@salary",Convert.ToInt32(salary_txt.Text));
                    SqlParameter p1 = new SqlParameter("@notes", notes_txt.Text);
                   
                    SqlParameter p2 = new SqlParameter("@number", Convert.ToInt32(sal_num_txt.Text));
                    SqlParameter p3 = new SqlParameter("@id1", Convert.ToInt32(sal_no_txt.Text));

                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p);
                    mycom1.Parameters.Add(p1);
                    mycom1.Parameters.Add(p2);
                    mycom1.Parameters.Add(p3);
                    

                    SqlDataReader myreader1 = mycom1.ExecuteReader();

                    emp_no_txt2.Text = "";
                    salary_txt.Text = "";
                    sal_num_txt.Text = "";
                    notes_txt.Text = "";
                    emp_fname_combo1.Text = "";
                    emp_lname_combo1.Text = "";
                    cer_txt2.Text = "";
                    id_txt2.Text = "";
                    level_txt2.Text = "";
                    button18.Enabled = false;
                    button17.Enabled = false;
                    sal_no_txt.Text = "";


                    toolTip1.Show("تم التعديل بنجاح" , button17);
                }
                timer2.Enabled = true;
            }

            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
                
            if (pass_txt.Text == "")
            {
                errorProvider1.SetError(button20, "الرجاء إدخال كلمة مرور");
            }
            else
            {
                SqlConnection mycon = new SqlConnection(Class1.x);
                mycon.Open();
                SqlCommand mycom2 = new SqlCommand("select user_name  from users where (user_id = @user) and (password = @pass)", mycon);
                SqlParameter p1 = new SqlParameter("@user", Convert.ToInt32(user_id_txt.Text));
                SqlParameter p2 = new SqlParameter("@pass", pass_txt.Text);
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p1);
                mycom2.Parameters.Add(p2);
                SqlDataReader myreader = mycom2.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    errorProvider1.SetError(button20, "كلمة المرور خاطئة الرجاء التأكد");
                    pass_txt.Text = "";
                }
                else
                {
                    groupBox12.Enabled = true;
                    while (myreader.Read())
                    {
                        user_txt.Text = Convert.ToString(myreader[0]);
                    }
                }
                mycon.Close();
            }
            timer2.Enabled = true;


        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
                
            if (user_txt.Text == "" || pass_1_txt.Text == "" || val_pass_txt.Text == "")
            {
                errorProvider1.SetError(button25,"الرجاء عدم ترك حقل فارغ");
            }
            else if (val_pass_txt.Text == pass_1_txt.Text)
            {
                SqlConnection mycon1 = new SqlConnection(Class1.x);
                mycon1.Open();
                SqlCommand mycom1 = new SqlCommand("UPDATE users SET user_name = @user ,password = @pass  WHERE (user_id = @id) ", mycon1);
                SqlParameter p = new SqlParameter("@user", user_txt.Text);
                SqlParameter p1 = new SqlParameter("@pass", pass_1_txt.Text);
                SqlParameter p2 = new SqlParameter("@id", Convert.ToInt32(user_id_txt.Text));

                mycom1.CommandType = CommandType.Text;
                mycom1.Parameters.Add(p);
                mycom1.Parameters.Add(p1);
                mycom1.Parameters.Add(p2);
                SqlDataReader myreader1 = mycom1.ExecuteReader();
                toolTip1.Show("تم التعديل بنجاح",button25);
                mycon1.Close();
                label42.Text = user_txt.Text;
                user_txt.Text = "";
                pass_1_txt.Text = "";
                val_pass_txt.Text = "";
            }
            else
            {
                errorProvider1.SetError(button25, "الرجاء تأكيد كلمة المرور");
            }
            timer2.Enabled = true;
        }

        private void emp_lname_combo_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Class1.x);
            con.Open();
            SqlCommand com = new SqlCommand("select users.user_id , users.user_name , users.password , users.dep , users.emp , users.sal , users.vac , users.mat , users.teacher , users.search , users.print_emp , users.per from users INNER JOIN employees ON (employees.emp_no = users.emp_no) where (employees.emp_fname = @fname) and (employees.emp_lname = @lname)",con);
            SqlParameter p = new SqlParameter("@fname", emp_fname_combo_5.SelectedItem);
            SqlParameter p2 = new SqlParameter("@lname", emp_lname_combo_5.SelectedItem);
            com.Parameters.Add(p);
            com.Parameters.Add(p2);
            com.CommandType = CommandType.Text;
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                user_id2.Text = Convert.ToString(reader[0]);
                user_txt2.Text = Convert.ToString(reader[1]);
                pass_txt_2.Text = Convert.ToString(reader[2]);
                dep.SelectedItem = Convert.ToString(reader[3]);
                emp.SelectedItem = Convert.ToString(reader[4]);
                sal.SelectedItem = Convert.ToString(reader[5]);
                vac.SelectedItem = Convert.ToString(reader[6]);
                mat.SelectedItem = Convert.ToString(reader[7]);
                teacher.SelectedItem = Convert.ToString(reader[8]);
                search.SelectedItem = Convert.ToString(reader[9]);
                print_emp.SelectedItem = Convert.ToString(reader[10]);
                per.SelectedItem = Convert.ToString(reader[11]);
            }
            groupBox14.Enabled = true;
            groupBox15.Enabled = true;
            con.Close();
        }

        private void emo_fname_combo_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                emp_lname_combo.Items.Clear();
                SqlConnection con = new SqlConnection(Class1.x);
                con.Open();
                SqlCommand com = new SqlCommand("select  emp_lname from employees where emp_fname = @name", con);
                SqlParameter p = new SqlParameter("@name", emp_fname_combo_5.SelectedItem);
                com.Parameters.Add(p);
                com.CommandType = CommandType.Text;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    emp_lname_combo_5.Items.Add(Convert.ToString(reader[0]));



                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("الرجاء الانتباه");
            }

        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
                

            if (user_txt2.Text == "" || pass_txt_2.Text == "" || val_pass_txt_2.Text == "")
            {
                errorProvider1.SetError(button29, "الرجاء عدم ترك حقل فارغ");
            }
            else if (val_pass_txt_2.Text == pass_txt_2.Text)
            {
                SqlConnection mycon1 = new SqlConnection(Class1.x);
                mycon1.Open();
                SqlCommand mycom1 = new SqlCommand("UPDATE users SET user_name = @user ,password = @pass  WHERE (user_id = @id) ", mycon1);
                SqlParameter p = new SqlParameter("@user", user_txt2.Text);
                SqlParameter p1 = new SqlParameter("@pass", pass_txt_2.Text);
                SqlParameter p2 = new SqlParameter("@id", Convert.ToInt32(user_id2.Text));

                mycom1.CommandType = CommandType.Text;
                mycom1.Parameters.Add(p);
                mycom1.Parameters.Add(p1);
                mycom1.Parameters.Add(p2);
                SqlDataReader myreader1 = mycom1.ExecuteReader();
                toolTip1.Show("تم التعديل بنجاح",button29);
                mycon1.Close();
                
                user_txt2.Text = "";
                pass_txt_2.Text = "";
                val_pass_txt_2.Text = "";
            }
            else
            {
                errorProvider1.SetError(button29, "الرجاء تأكيد كلمة المرور");
            }
            timer2.Enabled = true;
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            progressBar1.Increment(100);
            errorProvider1.Clear();
                
            SqlConnection mycon1 = new SqlConnection(Class1.x);
            mycon1.Open();
            SqlCommand mycom1 = new SqlCommand("UPDATE users SET dep = @dep ,emp = @emp ,sal = @sal ,vac = @vac ,mat = @mat ,teacher = @teacher ,search = @search ,print_emp = @print_emp ,per = @per  WHERE (user_id = @id) ", mycon1);
            SqlParameter p = new SqlParameter("@dep",dep.SelectedItem);
            SqlParameter p1 = new SqlParameter("@emp", emp.SelectedItem);
            SqlParameter p2 = new SqlParameter("@sal", sal.SelectedItem);
            SqlParameter p3 = new SqlParameter("@vac", vac.SelectedItem);
            SqlParameter p4 = new SqlParameter("@mat", mat.SelectedItem);
            SqlParameter p5 = new SqlParameter("@teacher", teacher.SelectedItem);
            SqlParameter p6 = new SqlParameter("@search", search.SelectedItem);
            SqlParameter p7 = new SqlParameter("@print_emp", print_emp.SelectedItem);
            SqlParameter p8 = new SqlParameter("@per", per.SelectedItem);
            SqlParameter p9 = new SqlParameter("@id", Convert.ToInt32(user_id2.Text));

            mycom1.CommandType = CommandType.Text;
            mycom1.Parameters.Add(p);
            mycom1.Parameters.Add(p1);
            mycom1.Parameters.Add(p2);
            mycom1.Parameters.Add(p3);
            mycom1.Parameters.Add(p4);
            mycom1.Parameters.Add(p5);
            mycom1.Parameters.Add(p6);
            mycom1.Parameters.Add(p7);
            mycom1.Parameters.Add(p8);
            mycom1.Parameters.Add(p9);
            SqlDataReader myreader1 = mycom1.ExecuteReader();
            toolTip1.Show("تم التعديل بنجاح",button30);
            mycon1.Close();

            groupBox14.Enabled = false;
            groupBox15.Enabled = false;
            timer2.Enabled = true;
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            system_dateandtime.Value = System.DateTime.Now;
            date.Text = Convert.ToString(system_dateandtime.Value.Date.Year) + "/" + Convert.ToString(system_dateandtime.Value.Date.Month) + "/" + Convert.ToString(system_dateandtime.Value.Date.Day);
            time.Text = Convert.ToString(system_dateandtime.Value.Hour) + ":" + Convert.ToString(system_dateandtime.Value.Minute) + ":" + Convert.ToString(system_dateandtime.Value.Second);
            
        }

        

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button6);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button5);
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button7);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button12);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button2);
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button10);
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button18);
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button17);
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button13);
        }

        private void button28_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button28);
        }

        private void button27_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button27);
        }

        private void button26_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button26);
        }

        private void button31_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button31);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button3);
        }

        private void button21_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button21);
        }

        private void button29_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button29);
        }

        private void button30_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button30);
        }

        private void button25_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button25);
        }

        
       

       


      

        

        

       

       

       

        

        

        

        }
    }

        