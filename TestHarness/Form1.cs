using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using WindowsFormsCalendar;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;

namespace TimeKontroll
{
    public partial class Form1 : Form
    {
        private DataSet ds;
        private OleDbDataAdapter da;
        private DataTableCollection tables;
        private DateTime StartDate;
        private DateTime StopDate;
        private DateTime Tid;


        #region Fields

        public List<CalendarItem> _items = new List<CalendarItem>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            Tid = DateTime.Now;

            InitializeComponent();

            //  CalendarItem Item = new CalendarItem( this.Calendar1, DateTime.Now, DateTime.Now, "TEST" );

            //  _items.Add(Item);

            LabelEmpty();




        }

        #region Calendar Methods

        /// <summary>
        /// Handles the LoadItems event of the calendar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WindowsFormsCalendar.CalendarLoadEventArgs"/> instance containing the event data.</param>

        private void Calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
        {
           
            // Debug.WriteLine(_items);


            // foreach (CalendarItem calendarItem in _items)
            //{
            //   if (Calendar1.ViewIntersects(calendarItem))
            //  {
            //      Calendar1.Items.Add(calendarItem);
            //  }
            // }
        }

        public void LabelEmpty() {
            lblMandag.Text = "";
            lblMandag_KonsOrd.Text = "";
            lblMandag_FaktOrd.Text = "";
            lblMandag_Kons50.Text = "";
            lblMandag_Fakt50.Text = "";
            lblMandag_Kons100.Text = "";
            lblMandag_Fakt100.Text = "";
            lblMandag_AVS.Text = "";
            lblMandag_Fravar.Text = "";

            lblTuesday.Text = "";
            lblTuesday_KonsOrd.Text = "";
            lblTuesday_FaktOrd.Text = "";
            lblTuesday_Kons50.Text = "";
            lblTuesday_Fakt50.Text = "";
            lblTuesday_Kons100.Text = "";
            lblTuesday_Fakt100.Text = "";
            lblTuesday_AVS.Text = "";
            lblTuesday_Fravar.Text = "";

            lblWednesday.Text = "";
            lblWednesday_KonsOrd.Text = "";
            lblWednesday_FaktOrd.Text = "";
            lblWednesday_Kons50.Text = "";
            lblWednesday_Fakt50.Text = "";
            lblWednesday_Kons100.Text = "";
            lblWednesday_Fakt100.Text = "";
            lblWednesday_AVS.Text = "";
            lblWednesday_Fravar.Text = "";

            lblThursday.Text = "";
            lblThursday_KonsOrd.Text = "";
            lblThursday_FaktOrd.Text = "";
            lblThursday_Kons50.Text = "";
            lblThursday_Fakt50.Text = "";
            lblThursday_Kons100.Text = "";
            lblThursday_Fakt100.Text = "";
            lblThursday_AVS.Text = "";
            lblThursday_Fravar.Text = "";

            lblFriday.Text = "";
            lblFriday_KonsOrd.Text = "";
            lblFriday_FaktOrd.Text = "";
            lblFriday_Kons50.Text = "";
            lblFriday_Fakt50.Text = "";
            lblFriday_Kons100.Text = "";
            lblFriday_Fakt100.Text = "";
            lblFriday_AVS.Text = "";
            lblFriday_Fravar.Text = "";

            lblSaturday.Text = "";
            lblSaturday_KonsOrd.Text = "";
            lblSaturday_FaktOrd.Text = "";
            lblSaturday_Kons50.Text = "";
            lblSaturday_Fakt50.Text = "";
            lblSaturday_Kons100.Text = "";
            lblSaturday_Fakt100.Text = "";
            lblSaturday_AVS.Text = "";
            lblSaturday_Fravar.Text = "";

            lblSunday.Text = "";
            lblSunday_KonsOrd.Text = "";
            lblSunday_FaktOrd.Text = "";
            lblSunday_Kons50.Text = "";
            lblSunday_Fakt50.Text = "";
            lblSunday_Kons100.Text = "";
            lblSunday_Fakt100.Text = "";
            lblSunday_AVS.Text = "";
            lblSunday_Fravar.Text = "";

        }

        #endregion

        #region Month View Methods

        /// <summary>
        /// Handles the SelectionChanged event of the monthView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>


        #endregion

        private void monthView1_SelectionChanged_1(object sender, EventArgs e)
        {
           this.Calendar1.SetViewRange(this.monthView1.SelectionStart.Date, this.monthView1.SelectionEnd.Date);

            var fraDatoValgt = this.monthView1.SelectionStart.Date.ToString("yyyyMMdd");
            var TilDatoValgt = this.monthView1.SelectionEnd.Date.ToString("yyyyMMdd");

            LabelEmpty();

            ///             CalendarItem cal = new CalendarItem(Calendar1, StartDate, EndDate, "My Text Here");
            ///             cal.ApplyColor(System.Drawing.Color.Orange);
            ///             Calendar1.Items.Add(cal);

            string query = "SELECT Dato & Start & '00' AS DatoStart, Dato & Slutt & '00' AS DatoStop, Kunde & ':'   & ' Fakt: ' & FaktOrd & ' Kon: ' & Konsulenttimer  & ', ' & Oppgave AS Tekst, Kunde FROM Timer Where (Dato >= '" + fraDatoValgt + "') AND (Dato <='" + TilDatoValgt + "')";

            ///Debug.WriteLine("query: " + query);

            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=timer.mdb"))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                
              _items.Clear();
              

                while (reader.Read())
                {
                    
                    var StartDate = DateTime.ParseExact(reader[0].ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    ///var StartDate = DateTime.Parse(reader[0].ToString());
                    ///var EndDate = DateTime.Parse(reader[1].ToString());
                    var EndDate = DateTime.ParseExact(reader[1].ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

                    ///Debug.WriteLine("Start: " + StartDate);
                    ///Debug.WriteLine("End: " + EndDate);
                    ///Debug.WriteLine(reader[2].ToString());

                    CalendarItem Item = new CalendarItem(Calendar1, StartDate, EndDate, reader[2].ToString());

                   
                    if (reader[3].ToString() == "AVS")
                    {
                        Item.ApplyColor(Color.DeepSkyBlue);
                    }
                    else if (reader[3].ToString() == "INT")
                    {
                        Item.ApplyColor(Color.LightSalmon);
                    }
                    else if (reader[3].ToString() == "SYK")
                    {
                        Item.ApplyColor(Color.Red);
                    }
                    else if (reader[3].ToString() == "FER")
                    {
                        Item.ApplyColor(Color.GreenYellow);
                    }
                    else if(reader[3].ToString() == "NDA")
                    {
                        Item.ApplyColor(Color.Orange);
                    }
                    else if (reader[3].ToString() == "NDB")
                    {
                        Item.ApplyColor(Color.Orange);
                    }
                    else if (reader[3].ToString() == "DRI")
                    {
                        Item.ApplyColor(Color.Orange);
                    }
                    else if (reader[3].ToString() == "NSR")
                    {
                        Item.ApplyColor(Color.MediumPurple);
                    }
                    else if (reader[3].ToString() == "INP")
                    {
                        Item.ApplyColor(Color.MediumTurquoise);
                    }
                    else if (reader[3].ToString() == "INF")
                    {
                        Item.ApplyColor(Color.Moccasin);
                    }
                    else if (reader[3].ToString() == "MAT")
                    {
                        Item.ApplyColor(Color.Khaki);
                    }


                    _items.Add(Item);



               
               
                }


                foreach (CalendarItem calendarItem in _items)
                {
                    if (Calendar1.ViewIntersects(calendarItem))
                    {
                        Calendar1.Items.Add(calendarItem);
                    }
                }
                
                reader.Close();

              

                ///string query1 = "SELECT Dato, SUM(Konsulenttimer) AS KonsulentTimer, SUM(FaktOrd) AS FaktOrd, SUM(Kons50) AS Kons50, SUM(Fakt50) AS Fakt50, SUM(Kons100) AS Kons100, SUM(Fakt100) AS Fakt100, SUM(AvspasT) AS AvspasT, SUM(Fravar) AS Fravar FROM Timer HAVING Dato BETWEEN '" + fraDatoValgt + "' AND '" + TilDatoValgt + "'";
                string query1 = "SELECT Dato, SUM(Konsulenttimer) AS Konsulenttimer, SUM(FaktOrd) AS FaktOrd, SUM(Kons50) AS Kons50, SUM(Fakt50) AS Fakt50, SUM(Kons100) AS Kons100, SUM(Fakt100) AS Fakt100, SUM(AvspasT) AS AvspasT, SUM(Fravar) AS Fravar FROM Timer GROUP BY Dato HAVING  (Dato BETWEEN '" + fraDatoValgt +"' AND '"+TilDatoValgt +"')";
                using (OleDbConnection connection1 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=timer.mdb"))
                {
                    OleDbCommand command1 = new OleDbCommand(query1, connection1);
                    connection1.Open();
                    OleDbDataReader reader1 = command1.ExecuteReader();
                    {
                                                            

                        while (reader1.Read())
                        {

                            
                            var Ukedag = DateTime.ParseExact(reader1[0].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            if (Ukedag.DayOfWeek == DayOfWeek.Monday)
                                {
                                lblMandag.Text= (Ukedag.DayOfWeek.ToString());
                                lblMandag_KonsOrd.Text=("Normal: " +Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblMandag_FaktOrd.Text=("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblMandag_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));   
                                lblMandag_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));      
                                lblMandag_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));       
                                lblMandag_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));  
                                lblMandag_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));    
                                lblMandag_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));    
                               
                            }

                            if (Ukedag.DayOfWeek == DayOfWeek.Tuesday)
                            {
                                lblTuesday.Text = (Ukedag.DayOfWeek.ToString());
                                lblTuesday_KonsOrd.Text = ("Normal: " + Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_FaktOrd.Text = ("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblTuesday_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));

                            }
                            if (Ukedag.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                lblWednesday.Text = (Ukedag.DayOfWeek.ToString());
                                lblWednesday_KonsOrd.Text = ("Normal: " + Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_FaktOrd.Text = ("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblWednesday_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));

                            }
                            if (Ukedag.DayOfWeek == DayOfWeek.Thursday)
                            {
                                lblThursday.Text = (Ukedag.DayOfWeek.ToString());
                                lblThursday_KonsOrd.Text = ("Normal: " + Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_FaktOrd.Text = ("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblThursday_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));

                            }
                            if (Ukedag.DayOfWeek == DayOfWeek.Friday)
                            {
                                lblFriday.Text = (Ukedag.DayOfWeek.ToString());
                                lblFriday_KonsOrd.Text = ("Normal: " + Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_FaktOrd.Text = ("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblFriday_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));

                            }
                            if (Ukedag.DayOfWeek == DayOfWeek.Saturday)
                            {
                                lblSaturday.Text = (Ukedag.DayOfWeek.ToString());
                                lblSaturday_KonsOrd.Text = ("Normal: " + Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_FaktOrd.Text = ("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSaturday_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));

                            }
                            if (Ukedag.DayOfWeek == DayOfWeek.Sunday)
                            {
                                lblSunday.Text = (Ukedag.DayOfWeek.ToString());
                                lblSunday_KonsOrd.Text = ("Normal: " + Math.Round(Double.Parse(reader1[1].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_FaktOrd.Text = ("Fakt: " + Math.Round(Double.Parse(reader1[2].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_Kons50.Text = ("Kons50: " + Math.Round(Double.Parse(reader1[3].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_Fakt50.Text = ("Fakt50: " + Math.Round(Double.Parse(reader1[4].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_Kons100.Text = ("Kons100: " + Math.Round(Double.Parse(reader1[5].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_Fakt100.Text = ("Fakt100:" + Math.Round(Double.Parse(reader1[6].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_AVS.Text = ("AVS: " + Math.Round(Double.Parse(reader1[7].ToString()), 1, MidpointRounding.AwayFromZero));
                                lblSunday_Fravar.Text = ("Fravær: " + Math.Round(Double.Parse(reader1[8].ToString()), 1, MidpointRounding.AwayFromZero));

                            }

                        }

                       

                        reader1.Close();

                    }
                    }
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    
    }
}



