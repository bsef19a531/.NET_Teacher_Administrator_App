using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Teacher_Administrator_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SqlConnection Connect = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Abdullah\\source\\repos\\Teacher_Administrator_App\\Teacher_Administrator_App\\Database1.mdf;Integrated Security=True;Connect Timeout=30");

        SqlCommand command = new SqlCommand();

        SqlDataReader datareader;

        SqlDataAdapter adapter;

        DataSet dataSet;

        public void Form1_Load(Object sender, EventArgs e)
        {
            command.Connection = Connect;
        }



        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        public double gradeCalculator(double s)
        {
            if(s >= 85)
            { return 4; }
            else if (s >= 80) { return 3.70; }
            else if(s >= 75) { return 3.30; }
            else if(s >= 70) { return 3.00; }
            else if (s >= 65) { return 2.70; }
            else if (s >= 60) { return 2.30; }
            else if (s >= 58) { return 2.00; }
            else if (s >= 55) { return 1.70; }
            else if (s >= 50) { return 1.00; }
            return 0;

        }

        public double gpaCalculator(double s1, double s2, double s3, double s4, double s5)
        {

            double g1 = gradeCalculator(s1);
            double g2 = gradeCalculator(s2);
            double g3 = gradeCalculator(s3);
            double g4 = gradeCalculator(s4);
            double g5 = gradeCalculator(s5);

           return (g1 + g2 + g3 + g4 + g5) * 3 / (3.00 * 5);
        }

        private void studentAddBtn_Click(object sender, EventArgs e)
        {
            Connect.Open();

            command.CommandText = "insert into Student(Name, RollNo) values('" + studentNameTxtBx.Text + "', '" + rollNoTxtBx.Text + "')";

            command.Connection = Connect;

            int status = 0;

            try { status = command.ExecuteNonQuery(); }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }
            

            if (status > 0)
            {
                studentQryLbl.Text = "Last Record Updated Successfully";
                MessageBox.Show("Record Updated Successfully");
            }
            else
            {
                MessageBox.Show("Something went Wrong");
                studentQryLbl.Text = "Something went Wrong";
            }

            Connect.Close();

            studentNameTxtBx.Clear();
            rollNoTxtBx.Clear();
            degreeTxtBx.Clear();
            sessionTxtBx.Clear();
        }

        private void marksAddBtn_Click(object sender, EventArgs e)
        {
            Connect.Open();

            command.CommandText = "insert into Course(RollNo, sub1, sub2, sub3, sub4, sub5) values('" + marksRollTxtBx.Text + "', '" + sub1TxtBx.Text + "', '" + sub2TxtBx.Text + "', '" + sub3TxtBx.Text + "', '" + sub4TxtBx.Text + "', '" + sub5TxtBx.Text + "' )";

            command.Connection = Connect;

            int status = 0;

            try { status = command.ExecuteNonQuery(); }
            catch(Exception exception) { MessageBox.Show(exception.ToString()); }

            if (status > 0)
            {
                MarksQryLbl.Text = "Last Record Updated Successfully";
                MessageBox.Show("Record Updated Successfully");
            }
            else
            {
                MessageBox.Show("Something went Wrong");
                MarksQryLbl.Text = "Something went Wrong";
            }

            Connect.Close();


            marksRollTxtBx.Clear();
            sub1TxtBx.Clear();
            sub2TxtBx.Clear();
            sub3TxtBx.Clear();
            sub4TxtBx.Clear();
            sub5TxtBx.Clear();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            String Name = "";
            String RollNo = "";
            int sub1 = 0, sub2 = 0, sub3 = 0, sub4 = 0, sub5 = 0;

            Connect.Open();

            //MessageBox.Show("Select * from course where RollNo = " + searchTxtBx.Text);


            //command.CommandText = "Select * from Course where RollNo = '" + searchTxtBx.Text + "'";
            //command.CommandText = "Select s.Name, c.RollNo, c.sub1, c.sub2, c.sub3, c.sub4, c.sub5 from Student s left join Course c on s.RollNo = c.RollNo and s.RollNo = '" + searchTxtBx.Text + "'";

            command.CommandText = "Select s.Name, s.RollNo, c.sub1, c.sub2, c.sub3, c.sub4,c.sub5 from Student s, Course c Where c.RollNo = '"+searchTxtBx.Text +"' and s.RollNo = '"+ searchTxtBx.Text + "'";
            command.Connection = Connect;

            try { datareader = command.ExecuteReader(); }
            catch(Exception exception) { MessageBox.Show(exception.ToString()); }
            

            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    Name = datareader["Name"].ToString();
                    RollNo = datareader["RollNo"].ToString();
                    sub1 = int.Parse(datareader["sub1"].ToString());
                    sub2 = int.Parse(datareader["sub2"].ToString());
                    sub3 = int.Parse(datareader["sub3"].ToString());
                    sub4 = int.Parse(datareader["sub4"].ToString());
                    sub5 = int.Parse(datareader["sub5"].ToString());
                }



                label3.Text ="Name: " +Name +"   RollNo: " +searchTxtBx.Text + "   GPA: (" + gpaCalculator(sub1, sub2, sub3, sub4, sub5) + ")" ;
                label2.Text = "DSA = " + sub1 + " , OOP = " + sub2 + " , PF = " + sub3 + " , PE = " + sub4 + " , AOA = " + sub5;

                MarksQryLbl.Text = "Result Found";

            }
            else
            {
                MessageBox.Show("No result Found");
                MarksQryLbl.Text = "No result Found";
            }
            searchTxtBx.Clear();
            Connect.Close();

        }
    }
}

