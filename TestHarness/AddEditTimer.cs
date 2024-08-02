using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xaml;



namespace TimeKontroll
{
    public partial class AddEditTimer : Form
    {

        System.Windows.Controls.TextBox textBox;

        public OleDbConnection myConnToAccess;
        private OleDbCommand mySQLCommand;
        private string mySQLStrg;
        private DataSet ds;
        private DataSet ds1;
        private OleDbDataAdapter da;
        private OleDbDataAdapter da1;
        private DataTableCollection tables;
        private DateTime Tid;
        private DateTime Start;
        private DateTime Stopp;
        private string filename = @".\Standard.txt";
        private string Kunde;
        private ArrayList EkskludereteKunder = new ArrayList();

        private OleDbConnection CreateConnection()
        {
            return new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=timer.mdb");
        }

        public AddEditTimer()
        {
            InitializeComponent();
        }
        public struct StandardTekst
        {
            public string Tittel;
            public string StandardText;

            public override string ToString()
            {
                return Tittel;
            }
        }
        private void AddEditTimer_Load(object sender, EventArgs e)
        {
            lblStartTid.Text = StartTid.TextData;
            lblStopTid.Text = StopTid.TextData;

            textBox = new System.Windows.Controls.TextBox();
            textBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            textBox.AcceptsReturn = true;
            textBox.FontSize = 14;
            textBox.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            textBox.SpellCheck.IsEnabled = true;
            TbOppdrag.Child = textBox;

            EkskludereteKunder.Add("INT");
            EkskludereteKunder.Add("DRI");
            EkskludereteKunder.Add("SYK");
            EkskludereteKunder.Add("MAT");
            EkskludereteKunder.Add("NDA");
            EkskludereteKunder.Add("AVS");
            EkskludereteKunder.Add("KUR");

            try
            {
                var StandardTekstStreamReader = new StreamReader(filename, Encoding.UTF8);
                // Read all the elements into the list.
                while (StandardTekstStreamReader.Peek() != -1)
                {
                    string lineread = StandardTekstStreamReader.ReadLine();
                    string[] fields = lineread.Split(';');
                    var info = new StandardTekst()
                    {
                        Tittel = fields[0],
                        StandardText = fields[1]
                    };
                    ComboBox2.Items.Add(info);
                }
                // Close the file.
                StandardTekstStreamReader.Close();
            }

            catch (Exception ex)
            {
                // File missing.
                // ResponseDialogResult = MessageBox.Show("File not Found!", "File Not Found",
                // MessageBoxButtons.OK, MessageBoxIcon.Question)
                // If ResponseDialogResult = DialogResult.OK Then
                // Exit the program.
                // Me.Close()
                // End If
            }



            using (var myConnToAccess = CreateConnection())
            {

                myConnToAccess.Open();
                ds = new DataSet();
                tables = ds.Tables;
                da = new OleDbDataAdapter("SELECT KundeID FROM Kunde", myConnToAccess);
                da.Fill(ds, "tblname");
                var view1 = new DataView(tables[0]);
                myConnToAccess.Close();

                {
                    var withBlock = ComboBox1;
                    withBlock.DataSource = ds.Tables["tblname"];
                    withBlock.DisplayMember = "KundeID";
                    withBlock.ValueMember = "KundeID";
                    withBlock.SelectedIndex = 0;
                    withBlock.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    withBlock.AutoCompleteSource = AutoCompleteSource.ListItems;
                }

            }

            using (var myConnToAccess = CreateConnection())
            {

                myConnToAccess.Open();
                da = new OleDbDataAdapter("SELECT DISTINCT Konsulent FROM Timer", myConnToAccess);
                var dt = new DataTable();
                da.Fill(dt);

                try
                {
                    lblKonsulent.Text = Convert.ToString(dt.Rows[0][0]);
                }
                catch (Exception ex)
                {

                }
                myConnToAccess.Close();

            }

        }
        public void ByttKunde()
        {


            

                using (var myConnToAccess = CreateConnection())
                {


                    myConnToAccess.Open();
                    da = new OleDbDataAdapter("SELECT Navn FROM Kunde WHERE KundeID='" + ComboBox1.Text.ToString() + "'", myConnToAccess);
                    var dt = new DataTable();
                    da.Fill(dt);



                    try
                    {
                        lblKundeNavn.Text = Convert.ToString(dt.Rows[0][0]);

                    }


                    catch (Exception ex)
                    {
                        lblKundeNavn.Text = "Velg Kunde";
                    }

                    myConnToAccess.Close();
                }


                Kunde = ComboBox1.SelectedValue.ToString();

                //ComboBox1.Enabled = false;

                if (ComboBox1.SelectedValue.ToString() == "MAT") { textBox.Text = "Mat pause"; }



            //lblStartTid.Text = "";
            //lblStopTid.Text = "";
            /*lblDiff.Text = "";
            lblKundeTid.Text = "";
            lblKonsulentTid.Text = "";*/
            ComboBox2.Enabled = true;
            Kunde = ComboBox1.SelectedValue.ToString();
           
            ;




            using (var myConnToAccess = CreateConnection())
            {


                myConnToAccess.Open();
                da = new OleDbDataAdapter("SELECT Navn FROM Kunde WHERE KundeID='" + ComboBox1.SelectedValue.ToString() + "'", myConnToAccess);
                var dt = new DataTable();
                da.Fill(dt);

                try
                {
                    lblKundeNavn.Text = Convert.ToString(dt.Rows[0][0]);
                }
                catch (Exception ex)
                {
                    lblKundeNavn.Text = "Velg Kunde";
                }

                myConnToAccess.Close();
            }

           BtnStop.Enabled = true;

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ByttKunde();

        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            decimal TidbruktKunde;
            decimal TidbruktKonsulent;
            decimal Avspassering;
            decimal Fravar;
            decimal DiffTidKunde;
            decimal DiffTidKonsulent;
            string Dato;
            string KlokkeStart;
            string KlokkeStopp;


            //Tid = DateTime.Now;


            // split into date + time parts
            Dato =  DatoValgt.TextData;
            //var timepart = Tid.TimeOfDay;

            string Fra = lblStartTid.Text.ToString();
            string Til = lblStopTid.Text.ToString();

            string Fratimestamp = DatoValgt.TextData.ToString() + Fra + "00";
            var dtFra = DateTime.ParseExact(Fratimestamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            string Tiltimestamp = DatoValgt.TextData + Til + "00";
            var dtTil = DateTime.ParseExact(Tiltimestamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);


            KlokkeStart = lblStartTid.Text.ToString();
            KlokkeStopp = lblStopTid.Text.ToString();


            TimeSpan diffOfDates = dtTil - dtFra;


            DiffTidKunde = Convert.ToDecimal(diffOfDates.TotalMinutes.ToString());

            DiffTidKonsulent = Convert.ToDecimal(diffOfDates.TotalMinutes.ToString());




                BtnStop.Enabled = false;





            //lblDiff.Text = DiffTidKunde.ToString();

            if (EkskludereteKunder.Contains(Kunde))
            {
                TidbruktKunde = Convert.ToDecimal("0");
            }
            else
            {

                TidbruktKunde = Math.Round(DiffTidKunde / 60m, 1, MidpointRounding.AwayFromZero);

                if ((double)TidbruktKunde > 0.3)
                {
                    TidbruktKunde = (decimal)(Math.Round((double)TidbruktKunde / 0.5d) * 0.5d);
                }

            }


            TidbruktKonsulent = Math.Round(DiffTidKonsulent / 60m, 1, MidpointRounding.AwayFromZero);
            TidbruktKonsulent = (decimal)(Math.Round((double)TidbruktKonsulent / 0.5d) * 0.5d);

            //lblKundeTid.Text = TidbruktKunde.ToString();
            //lblKonsulentTid.Text = TidbruktKonsulent.ToString();

            Avspassering = 0;
            Fravar = 0;

            if (Kunde == "AVS")
            {
                Avspassering = TidbruktKonsulent;
                TidbruktKonsulent = 0;
                TidbruktKunde = 0;
            }
            else if (Kunde == "SYK")
            {
                Fravar = TidbruktKonsulent;
                TidbruktKonsulent = 0;
                TidbruktKunde = 0;
            }
            else if (Kunde == "FER")
            {
                Fravar = TidbruktKonsulent;
                TidbruktKonsulent = 0;
                TidbruktKunde = 0;
            }
            else if (Kunde == "MAT")
            {
                Avspassering = TidbruktKonsulent;
                TidbruktKonsulent = 0;
                TidbruktKunde = 0;
            }

            else { }

            using (var conn = CreateConnection())
            {
                using (var comd = new OleDbCommand("INSERT INTO Timer ([Dato], [Start], [Slutt], [FaktOrd],  [Fakt50], [Fakt100], [Oppgave], [Kunde], [Kilometer], [Konsulent], [Konsulenttimer], [Kons50], [Kons100], [Avspaseres], [AvspasT], [Fravar], [Otidutbet], [Sperre])  VALUES (@Dato, @Start, @Slutt, @FaktOrd, @Fakt50, @Fakt100, @Oppgave, @Kunde, @Kilometer, @Konsulent, @Konsulenttimer, @Kons50, @Kons100, @Avspaseres, @AvspasT, @Fravar, @Otidutbet, @Sperre)", conn))
                {
                    comd.Parameters.AddWithValue("@Dato", Dato.ToString());
                    comd.Parameters.AddWithValue("@Start", KlokkeStart.ToString());
                    comd.Parameters.AddWithValue("@Slutt", KlokkeStopp.ToString());
                    comd.Parameters.AddWithValue("@FaktOrd", TidbruktKunde.ToString());
                    comd.Parameters.AddWithValue("@Fakt50", "0");
                    comd.Parameters.AddWithValue("@Fakt100", "0");
                    comd.Parameters.AddWithValue("@Oppgave", textBox.Text);
                    comd.Parameters.AddWithValue("@Kunde", Kunde);
                    comd.Parameters.AddWithValue("@Kilometer", "0");
                    comd.Parameters.AddWithValue("@Konsulent", lblKonsulent.Text);
                    comd.Parameters.AddWithValue("@Konsulenttimer", TidbruktKonsulent.ToString());
                    comd.Parameters.AddWithValue("@Kons50", "0");
                    comd.Parameters.AddWithValue("@Kons100", "0");
                    comd.Parameters.AddWithValue("@Avspaseres", "0");
                    comd.Parameters.AddWithValue("@AvspasT", Avspassering.ToString());
                    comd.Parameters.AddWithValue("@Fravar", Fravar.ToString());
                    comd.Parameters.AddWithValue("@Otidutbet", "0");
                    comd.Parameters.AddWithValue("@Sperre", "0");
                    conn.Open();
                    try
                    {
                        comd.ExecuteNonQuery();
                        //AutoClosingMessageBox.Show("Timeføring lagret på kunde:" + Kunde + "!", "Timeføring lagret", 3000);
                    }
                    catch (Exception ex)
                    {
                        AutoClosingMessageBox.Show("Feil i føring, du kan ikke føre på samme kunde i samme tidsrom", "FEIL VED LAGRING!!!", 5000);
                        BtnStop.Enabled = true;
                        ; return;

                    }
                    this.Close();

                }
            }


         
            

          


            
            
        }
        class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }
    }
    }

