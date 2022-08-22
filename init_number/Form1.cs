using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Drawing.Printing;
namespace init_number
{
    public partial class Form1 : Form
    {


        static string constring = ConfigurationManager.ConnectionStrings["Conns"].ConnectionString;
        MySqlConnection dbCon = new MySqlConnection(constring);


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int initial;
            int num;
            int result;
            string nomborstring;


            int convertnum;
            int cmt;
            int qfinal1;
            string convqfinal;
            int inserstRecord;
            string convtostring;
            int insertData;

            DataTable ds;
            DataTable ds1;
            int resultinsert;

            int qtype = 1;

            ds1 = currentQueue_1();

            if (ds1.Rows.Count > 0)
            {


                ds = currentMaxQueue_1();
                if (ds.Rows.Count > 0)
                {

                    int toInteger;


                    DataRow drnama = ds.Rows[0];
                    nomborstring = drnama["currMax"].ToString();

                    toInteger = Convert.ToInt32(nomborstring);

                    toInteger = toInteger + 1;
                    string conInt;
                    DateTime time;
                    time = DateTime.Now;

                    int bilcal = 0;
                    string fs;
                    conInt = Convert.ToString(toInteger).ToString();
                    string t_type = "G";
                    fs = DateTime.Now.ToString("HH:mm:ss");



                    insertData = InsertQue(conInt, fs, bilcal, qtype, t_type);

                    printPreviewDialog1.Document = printDocument1;
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", 300, 500);
                    printPreviewDialog1.ShowDialog();

                }

                else
                {





                }



            }
            else
            {






                initial = 1000;

                initial = initial + 1;
                result = initial;

                DateTime time;
                string fs;
                fs = DateTime.Now.ToString("HH:mm:ss");
                //Dim time As DateTime = DateTime.Now
                //Dim format As String = " HH:mm:ss"
                //Dim fs As String
                int bilcal = 0;
                string t_type = "G";
                convtostring = Convert.ToString(result);
                insertData = InsertQue(convtostring, fs, bilcal, qtype, t_type);


                if (insertData > 0)
                {


                    printPreviewDialog1.Document = printDocument1;
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", 300, 500);

                    printPreviewDialog1.ShowDialog();


                }

                else
                {











                }



            }


        
        }




        public DataTable currentQueue_2()
        {

            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = " SELECT  currQue  FROM queue where qtype=2";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }



        public DataTable currentCalled()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "  SELECT *FROM called_number order by cid desc limit 1 ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

            }

            return dt;

        }



        public int InsertQue(string number, string timeCounter, int bilcal, int qtype, string t_type)
        {
            var result = 0;
            int tempval = 0;
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "insert into queue(currQue,time_counter,bil_call, qtype,trans_t)values(@number,@timeCounter,@bilcal,@qtype,@t_type)";



                MySqlCommand cmd = new MySqlCommand();
                DataTable dt = new DataTable();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                try
                {
                    cmd.Parameters.AddWithValue("@number", number);
                    cmd.Parameters.AddWithValue("@timeCounter", timeCounter);
                    cmd.Parameters.AddWithValue("@qtype", qtype);
                    cmd.Parameters.AddWithValue("@bilcal", bilcal);
                    cmd.Parameters.AddWithValue("@t_type", t_type);
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
            return result;




        }


        public DataTable currentQueue_1()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = " SELECT  currQue  FROM queue where qtype=1";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }






        public DataTable DisplayCawangan()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "select * from code_caw join cawangan on code_caw.id_caw=cawangan.caw_code ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;



        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {






            DataTable ds;
            ds = DisplayCawangan();

            string s;
            string a;
            string p;
            if (ds.Rows.Count > 0)
            {
                DataRow drnama = ds.Rows[0];
                s = drnama["nama_cawangan"].ToString();
                a = drnama["alamat"].ToString();
                p = drnama["poskod"].ToString();



                //  e.Graphics.DrawString(s, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(20, 55));

                e.Graphics.DrawString(s, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(38, 55));


                DataTable ds2;
                string w;
                ds2 = FindQueue();
                if (ds.Rows.Count > 0)
                {

                    DataRow drnama1 = ds2.Rows[0];

                    w = drnama1["currQue"].ToString();

                    e.Graphics.DrawString("Nombor Giliran Anda :", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(41, 78));
                    e.Graphics.DrawString(w, new Font("Arial", 28, FontStyle.Bold), Brushes.Black, new Point(100, 100));




                    DataTable dcalled;
                    dcalled = FindLatestCalled();
                    if (dcalled.Rows.Count > 0)
                    {
                        DataRow drcalled = dcalled.Rows[0];
                        string noLatest = drcalled["que_number"].ToString();


                        e.Graphics.DrawString("Nombor Giliran Semasa :", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(40, 148));

                        e.Graphics.DrawString(noLatest, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(198, 148));


                    }

                    else
                    {


                    }

                    e.Graphics.DrawString("SILA DUDUK & ANDA DILAYAN SEBENTAR LAGI", new Font("Arial", 6, FontStyle.Bold), Brushes.Black, new Point(45, 165));

                    DateTime now = DateTime.Now;

                    string hari_ini = now.ToString("yyyy/MM/dd");
                    e.Graphics.DrawString(hari_ini, new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(25, 189));


                    //             //Dim time1 As DateTime = DateTime.Now
                    //Dim format1 As String = " HH:mm:ss:tt"

                    DateTime time1 = DateTime.Now;

                    DateTime.Now.ToString("h:mm:ss tt");

                    e.Graphics.DrawString(DateTime.Now.ToString("h:mm:ss tt"), new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(198, 189));
                    // DateTime thisDay = DateTime.Today;
                    // e.Graphics.DrawString(thisDay.ToString("D"), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(55, 150));

                    //string words = "SILA DUDUK, ANDA AKAN DILAYAN SEBENTAR LAGI";
                    //e.Graphics.DrawString(words, new Font("Arial Black",9, FontStyle.Regular), Brushes.Black, new Point(60,180));

                }
                else
                {


                }










            }




















        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {



























        }






        public DataTable FindQueue()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "SELECT * FROM queue order by id desc limit 1";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }



        public DataTable FindLatestCalled()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "SELECT * FROM called_number order by cid desc limit 1";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;


        }

















        public DataTable DisplayCaw()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {

                string strQry = "select code_caw.nama_cawangan as cwgn from code_caw join cawangan on code_caw.id_caw=cawangan.caw_code where caw_code=1 ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                dt.Dispose();
                da.Dispose();
                cmd.Dispose();
                dbCon.Close();
            }
            return dt;
        }






        public DataTable currentMaxQueue_1()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "SELECT MAX(currQue)as currMax FROM queue where qtype=1";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;


        }














        private void timer1_Tick(object sender, EventArgs e)
        {



            DataTable d4;
            d4 = DisplayCaw();
            if (d4.Rows.Count > 0)
            {
                DataRow dr = d4.Rows[0];

                string caw = dr["cwgn"].ToString();

                label2.Text = caw;
            }

            else
            {


            }




            DataTable dtC;
            string numbergiliran;
            int qid;
            string kaunter;

            dtC = currentCalled();
            DataTable dtprev;


            if (dtC.Rows.Count > 0)
            {

                DataRow dr = dtC.Rows[0];
                numbergiliran = dr["que_number"].ToString();
                kaunter = dr["count_number"].ToString();
                qid = Convert.ToInt16(dr["cid"]);

                label1.Text = numbergiliran;
                label3.Text = kaunter;
                DataTable d2;
                int id;
                string w;
                string k;
                   d2 = PrevQueue(qid);
                   if (d2.Rows.Count > 0)
                   {

                       DataRow dr2 = d2.Rows[0];

                       id = Convert.ToInt16(dr2["cid"]);
                       w = dr2["que_number"].ToString();
                       k = dr2["count_number"].ToString();


                       label4.Text = w;
                       label5.Text = k;


                   }

            }

            else
            {





            }


         








        }







        public DataTable PrevQueue(int qid)
        {
            /*int num = qid;


            string strQry = "SELECT * FROM called_number WHERE cid < " + num + " ORDER BY cid DESC LIMIT 1";

            MySqlCommand cmd = new MySqlCommand();
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQry;
            cmd.Connection = dbCon;
            dbCon.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dt.Dispose();
            da.Dispose();
            cmd.Dispose();
            dbCon.Close();
            return dt;
            */


            DataTable dt = new DataTable();
            int num = qid;

            using (MySqlConnection dbCon = new MySqlConnection(constring))
            {
                string strQry = "SELECT * FROM called_number WHERE cid < " + num + " ORDER BY cid DESC LIMIT 1";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = dbCon;
                dbCon.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

            }

            return dt;



        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            timer1.Start();

        }



      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {




            switch (e.KeyCode)
            {
                case Keys.NumPad7:
                    e.Handled = true;
                    button1.PerformClick();
                    break;





            }






            



        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.NumPad7:
                    e.Handled = true;
                    //button1.PerformClick();
                    button1.PerformClick();
                    break;

                // And so on




            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

   
       
    }

}