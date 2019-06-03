using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WrightTireEstimator
{
    /// <summary>
    /// View Model Binding for the Quick Quote Tab
    /// </summary>
    public class QuickQuoteViewModel : BaseViewModel
    {
        #region Public Properties
        //Current window>>>>>>>>>>>>>>>may need to be changed when seperate page is made for Quick Quote
        public Window Window { get; set; }
        public Page Page { get; set; }
        //Cost the user entered
        public string Cost
        {
            get { return ToMoneyString(mCost); }
            set
            {
                try
                {
                    mCost = FromMoneyString(value);
                    mMarkup = Settings.CheckMarkup(mCost);
                }
                catch
                {
                    mCost = -100;
                    //Add an error message
                }
            }
        }
        //Properties for Tax Exempt
        public decimal TaxExemptOneTire
        {
            get { return mTaxExemptOneTire; }
            set { mTaxExemptOneTire = value; }
        }
        public decimal TaxExemptTwoTires
        {
            get { return mTaxExemptTwoTires; }
            set { mTaxExemptTwoTires = value; }
        }
        public decimal TaxExemptThreeTires
        {
            get { return mTaxExemptThreeTires; }
            set { mTaxExemptThreeTires = value; }
        }
        public decimal TaxExemptFourTires
        {
            get { return mTaxExemptFourTires; }
            set { mTaxExemptFourTires = value; }
        }
        public decimal TaxExemptFiveTires
        {
            get { return mTaxExemptFiveTires; }
            set { mTaxExemptFiveTires = value; }
        }
        public decimal TaxExemptSixTires
        {
            get { return mTaxExemptSixTires; }
            set { mTaxExemptSixTires = value; }
        }
        //Properties for Taxed Values
        public decimal TaxedOneTire
        {
            get { return mTaxedOneTire; }
            set { mTaxedOneTire = value; }
        }
        public decimal TaxedTwoTires
        {
            get { return mTaxedTwoTires; }
            set { mTaxedTwoTires = value; }
        }
        public decimal TaxedThreeTires
        {
            get { return mTaxedThreeTires; }
            set { mTaxedThreeTires = value; }
        }
        public decimal TaxedFourTires
        {
            get { return mTaxedFourTires; }
            set { mTaxedFourTires = value; }
        }
        public decimal TaxedFiveTires
        {
            get { return mTaxedFiveTires; }
            set { mTaxedFiveTires = value; }
        }
        public decimal TaxedSixTires
        {
            get { return mTaxedSixTires; }
            set { mTaxedSixTires = value; }
        }
        //>>>>>>>>>>>>>>>>>>>>>>Make properties for the colors of the buttons when selected
        public Brush LawnButtonBrush
        {
            get { return mLawnButtonBrush; }
            set
            {
                try
                {
                    mLawnButtonBrush = value;
                }
                catch
                {
                    mLawnButtonBrush = BaseBrush;
                }
            }
        }
        public Brush PassengerButtonBrush
        {
            get { return mPassengerButtonBrush; }
            set
            {
                try
                {
                    mPassengerButtonBrush = value;
                }
                catch
                {
                    mPassengerButtonBrush = BaseBrush;
                }
            }
        }
        public Brush MediumTruckButtonBrush
        {
            get { return mMediumTruckButtonBrush; }
            set
            {
                try
                {
                    mMediumTruckButtonBrush = value;
                }
                catch
                {
                    mMediumTruckButtonBrush = BaseBrush;
                }
            }
        }
        
        #endregion

        #region Private Members
        //decimal value of string
        private decimal mCost;
        //Markup rate
        private decimal mMarkup;
        //State to determine which labor should be used
        private QuoteState mQuoteState;
        //private members containing the tax exempt values
        private decimal mTaxExemptOneTire;
        private decimal mTaxExemptTwoTires;
        private decimal mTaxExemptThreeTires;
        private decimal mTaxExemptFourTires;
        private decimal mTaxExemptFiveTires;
        private decimal mTaxExemptSixTires;
        //private members containing the taxed values
        private decimal mTaxedOneTire;
        private decimal mTaxedTwoTires;
        private decimal mTaxedThreeTires;
        private decimal mTaxedFourTires;
        private decimal mTaxedFiveTires;
        private decimal mTaxedSixTires;
        //Brushes for the buttons
        private Brush mLawnButtonBrush;
        private Brush mPassengerButtonBrush;
        private Brush mMediumTruckButtonBrush;
        //Brushes
        private static Brush BaseBrush = new SolidColorBrush(Color.FromRgb(211, 211, 211));
        private static Brush LawnBrush = new SolidColorBrush(Color.FromRgb(18,230, 38));
        private static Brush PassengerBrush = new SolidColorBrush(Color.FromRgb(140, 140, 140));
        private static Brush MediumTruckBrush = new SolidColorBrush(Color.FromRgb(185, 156, 107));
        //Cost Box Visability
        

        

        #endregion

        #region Constructor
        public QuickQuoteViewModel(Window window, QuickQuote QQ)
        {
            Window = window;
            this.SetStateLawn = new RelayCommand(LawnState);
            this.SetStatePassenger = new RelayCommand(PassengerState);
            this.SetStateMediumTruck = new RelayCommand(MediumTruckState);

            //Event Subscribers
            QQ.CostStackPanel.MouseEnter += new MouseEventHandler(OnMouseEnter);
            QQ.CostStackPanel.MouseLeave += new MouseEventHandler(OnMouseLeave);
        }

        #endregion

        #region Commands
        //Create commands to calculate the prices
        public ICommand SetStateLawn { get; set; }
        public ICommand SetStatePassenger { get; set; }
        public ICommand SetStateMediumTruck { get; set; }
        public ICommand OpenSettings { get; set; }
        #endregion

        #region Helper Methods
        //Checks labor, calculates all 12 values and sets them to the private members
        public void CalcQuickQuote()
        {

            decimal labor = CheckQuoteStateLabor();
            decimal disposal = CheckQuoteStateDisposal();

            //Creates an array that contains grand totals for both tax exempt and taxed sales from 1 to 6 tires
            decimal[] allResults = new decimal[12];
            for (int i = 0; i < 6; i++)
            {
                allResults[i] = ((mCost * (1 + mMarkup) + (labor * Settings.ServiceFeeRate)) + labor + disposal) * (i + 1);
            }
            for (int i = 6; i < 12; i++)
            {
                allResults[i] = ((mCost * (1 + mMarkup) + (labor * Settings.ServiceFeeRate)) * Settings.TaxRate + labor + disposal) * (i + -5);
            }
            //assigns the calculated values to corresponding properties
            assignValues(allResults);
        }
        //assigns the calculated values to corresponding properties
        private void assignValues(decimal[] decArray)
        {
            TaxExemptOneTire = decArray[0];
            TaxExemptTwoTires = decArray[1];
            TaxExemptThreeTires = decArray[2];
            TaxExemptFourTires = decArray[3];
            TaxExemptFiveTires = decArray[4];
            TaxExemptSixTires = decArray[5];
            TaxedOneTire = decArray[6];
            TaxedTwoTires = decArray[7];
            TaxedThreeTires = decArray[8];
            TaxedFourTires = decArray[9];
            TaxedFiveTires = decArray[10];
            TaxedSixTires = decArray[11];
            
        }
        //Assigns the labor rate based on the QuoteState
        private decimal CheckQuoteStateLabor()
        {
            switch (this.mQuoteState)
            {
                case QuoteState.New:
                    return -1000M;

                case QuoteState.Lawn:
                    return Settings.LawnLaborRate;


                case QuoteState.Passenger:
                    return Settings.PassengerLaborRate;


                case QuoteState.MediumTruck:
                    return Settings.MediumTruckLaborRate;


                default:
                    return 16M;


            }

        }
        //Assigns the disposal rate based on QuoteState
        private decimal CheckQuoteStateDisposal()
        {
            switch (this.mQuoteState)
            {
                case QuoteState.New:
                    return -1000M;

                case QuoteState.Lawn:
                    return Settings.LawnDisposalRate;


                case QuoteState.Passenger:
                    return Settings.PassengerDisposalRate;


                case QuoteState.MediumTruck:
                    return Settings.MediumTruckDisposalRate;


                default:
                    return -1000M;
            }
        }
        //Converts from a decimal to money format ($ XX.XX)
        private string ToMoneyString(decimal dec)
        {
            return "$ " + Math.Round(dec, 2);
        }
        //Converts from the money format back to a decimal
        private decimal FromMoneyString(string str)
        {
            string temp = str.Trim('$');
            return Convert.ToDecimal(temp);
        }
        private void SetBrushesToBase()
        {
            LawnButtonBrush = BaseBrush;
            PassengerButtonBrush = BaseBrush;
            MediumTruckButtonBrush = BaseBrush;
        }
        public void LawnState()
        {
            mQuoteState = QuoteState.Lawn;
            SetBrushesToBase();
            LawnButtonBrush = LawnBrush;
            CalcQuickQuote();

        }
        public void PassengerState()
        {
            mQuoteState = QuoteState.Passenger;
            SetBrushesToBase();
            PassengerButtonBrush = PassengerBrush;
            CalcQuickQuote();
        }
        public void MediumTruckState()
        {
            mQuoteState = QuoteState.MediumTruck;
            SetBrushesToBase();
            MediumTruckButtonBrush = MediumTruckBrush;
            CalcQuickQuote();
        }
        #endregion

        #region Test Methods
        public Visibility CostBoxVisibility
        {
            get { return mCostBoxVisibility; }
            set { mCostBoxVisibility = value; }
        }

        private Visibility mCostBoxVisibility = Visibility.Visible;

        public void ChangeVisibilityOnEnter()
        {
            CostBoxVisibility = Visibility.Visible;
        }
        public void ChangeVisibilityOnLeave()
        {
            CostBoxVisibility = Visibility.Hidden;
        }
        
        public void OnMouseEnter(object sender, MouseEventArgs e)
        {
            ChangeVisibilityOnEnter();
        }
        public void OnMouseLeave(object sender, MouseEventArgs e)
        {
            ChangeVisibilityOnLeave();
        }
        
        #endregion
    }
}
