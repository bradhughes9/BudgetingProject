using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;


namespace Project_Step3_BradleyHughes
{
    public partial class Budgeting : Form
    {
   

        public Budgeting()
        {

            InitializeComponent();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculating();
            
        }

        //This method is from the user creating their own 
        //non-essential bill.
        private void Wants_Click(object sender, EventArgs e)
        {

            try
            {
                //Adding a counter to add each bill
                //to the chart by its index.
                int counter = 0;

                //converting the payment to a double
                double Payment = Validate(PaymentAmount.Text);

                //List to hold the name and amount for extra bills
                //This will also be added to the graph for the final version of my program.
                var billNames = new List<string>();
                var billAmount = new List<double>();
                if (NameOfPayment.Text == "" || PaymentAmount.Text == "" || Payment < 1)
                {
                    MessageBox.Show("You must have a name and amount for your payment.");
                }
                else
                {
                    billNames.Add(NameOfPayment.Text);
                    billAmount.Add(Payment);
                    //using the counter to add each bill accordingly.
                    //I got some code from https://betterdashboards.wordpress.com/2009/02/04/display-percentages-on-a-pie-chart/
                    //to customize my charts and make them look alot better than default.
                    //All the code is doing is customizing the way the graph will look.
                    //This exact code will also be applied to the other pie chart located further below
                    //in this program.
                    //This code is what allows the name/label for each slice of the pie to be displayed outside of the graph
                    //Otherwise the pie graph will become extremely congested.
                    chartNon.Series[0]["PieLabelStyle"] = "Outside";
                    chartNon.Series[0].BorderWidth = 1;
                    chartNon.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
                    //this code makes the percentages show.
                    chartNon.Series[0].LegendText = "#PERCENT{P2}";
                    chartNon.DataManipulator.Sort(PointSortOrder.Descending, chartNon.Series[0]);
                    //end of borrowed code. 
                    chartNon.Series["NonEssential"].Points.AddXY(billNames[counter], billAmount[counter]);
                    //Clears the text box
                    NameOfPayment.Text = "";
                    PaymentAmount.Text = "";

                    double wantsTotal = billAmount.Sum(Convert.ToInt32);
                    TotalWants.Text = wantsTotal.ToString("C");
                    counter++;


                }
            }
            catch
            {
                MessageBox.Show("You must enter a value greater than or equal to zero.");
            }           
        }

        //The button Calculates totals.
        public void Calculating()
        {

            //List to hold all the bills values for later and easier use
            var allBills = new List<double>();
            try
            {
                //if the user enters nothing the box will default to 0
                if (NetIncome.Text == "") { NetIncome.Text = "0"; }
                if (ToSavings.Text == "") { ToSavings.Text = "0"; }
                if (Mortgage.Text == "") { Mortgage.Text = "0"; }
                if (Water.Text == "") { Water.Text = "0"; }
                if (Electric.Text == "") { Electric.Text = "0"; }
                if (Gas.Text == "") { Gas.Text = "0"; }
                if (InternetCable.Text == "") { InternetCable.Text = "0"; }
                if (HomeInsurance.Text == "") { HomeInsurance.Text = "0"; }
                if (CarInsurance.Text == "") { CarInsurance.Text = "0"; }
                if (Phone.Text == "") { Phone.Text = "0"; }
                if (HOA.Text == "") { HOA.Text = "0"; }
                if (Other.Text == "") { Other.Text = "0"; }


                //Take all the user input and tryparse to doubles.
                double netIncome = Validate(NetIncome.Text);
                double savings = Validate(ToSavings.Text); savings = savings / 100;
                double mortgage = Validate(Mortgage.Text);
                double water = Validate(Water.Text);
                double electric = Validate(Electric.Text);
                double gas = Validate(Gas.Text);
                double internet = Validate(InternetCable.Text);
                double homeInsurance = Validate(HomeInsurance.Text);
                double carInsurance = Validate(CarInsurance.Text);
                double phone = Validate(Phone.Text);
                double hoa = Validate(HOA.Text);
                double other = Validate(Other.Text);


                //After validation add all to the list if
                //their value is greater than 0.
                if (mortgage > 0) { allBills.Add(mortgage); }
                if (water > 0) { allBills.Add(water); }
                if (electric > 0) { allBills.Add(electric); }
                if (gas > 0) { allBills.Add(gas); }
                if (internet > 0) { allBills.Add(internet); }
                if (homeInsurance > 0) { allBills.Add(homeInsurance); }
                if (carInsurance > 0) { allBills.Add(carInsurance); }
                if (phone > 0) { allBills.Add(phone); }
                if (hoa > 0) { allBills.Add(hoa); }
                if (other > 0) { allBills.Add(other); }

                //multiply the savings amount with net income
                //to get the percent to save.
                double savingsAmount = savings * netIncome;
                TotalSavings.Text = savingsAmount.ToString("C");

                //finally add them all together to get a total.
                double totalBills = allBills.Sum(Convert.ToInt32);
                TotalBills.Text = totalBills.ToString("C");

                //The essential bills added to the graph
                //if their value is greater than zero.
                if (mortgage > 0) {PieChart.Series["Essential"].Points.AddXY("Mortgage/Rent", mortgage);}
                if (water > 0) {PieChart.Series["Essential"].Points.AddXY("Water", water);}
                if (electric > 0) {PieChart.Series["Essential"].Points.AddXY("Electric", electric);}
                if (gas > 0){PieChart.Series["Essential"].Points.AddXY("Gas", gas);}
                if (internet > 0) {PieChart.Series["Essential"].Points.AddXY("Internet/Cable", internet);}
                if(homeInsurance > 0){PieChart.Series["Essential"].Points.AddXY("Home Insurance", homeInsurance);}
                if(carInsurance > 0){PieChart.Series["Essential"].Points.AddXY("Car Insurance", carInsurance);}
                if(phone > 0){PieChart.Series["Essential"].Points.AddXY("Phone", phone);}
                if (hoa > 0){PieChart.Series["Essential"].Points.AddXY("HOA", hoa);}
                if(other > 0){PieChart.Series["Essential"].Points.AddXY("Other", other);}
                PieChart.Series[0]["PieLabelStyle"] = "Outside";
                PieChart.Series[0].BorderWidth = 1;
                PieChart.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
                PieChart.Legends[0].Enabled = true;
                //this code makes the percentages show.
                PieChart.Series[0].LegendText = "#PERCENT{P2}";

                //Total leftover
                double leftoverMoney = netIncome - (savingsAmount + totalBills + double.Parse(TotalWants.Text));

                
            }
            catch
            {
                //MessageBox.Show("You must enter integers greater than or equal to zero.");
            }
        }



        public static double Validate(string Input)
        {
            //holding the user input sent to method.
            string userInput = Input;
            //variable to return if the input is valid.
            double validInput;

            //if the user leaves the text empty then we assume they have no bill
            //and we set it to 0.
            if (userInput == "")
            {
                userInput = "0";
            }
        
            //Now we convert the string to a double.
            if (double.TryParse(userInput, out validInput))
            {
                if (validInput < 0)
                {
                    MessageBox.Show("You must enter a number that is greater than or equal to zero.");
                }
                
            }
            else
            {
                MessageBox.Show("You must enter a number that is greater than or equal to zero.");
            }

                return validInput;
        }

        //Clear Essential
        private void button1_Click_1(object sender, EventArgs e)
        {
            PieChart.Series.Clear();
        }
        //Clear Non-Essential
        private void button2_Click(object sender, EventArgs e)
        {
            chartNon.Series.Clear();
        }
        //Clears all the output textboxes.
        private void button3_Click(object sender, EventArgs e)
        {
            TotalSavings.Text = "";
            TotalBills.Text = "";
            TotalWants.Text = "";
            LeftOverMoney.Text = "";
        }





        //Accidentally added this, and if removed the program has lots of errors. Ctrl+z removes items from form.
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Budgeting_Load(object sender, EventArgs e)
        {
          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
