using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WrightTireEstimator
{
    public class SettingsPageViewModel
    {
        #region Public Properties
        public decimal LawnInstall
        {
            get { return mLawnInstall; }
            set { mLawnInstall = value; }
        }
        public decimal PassengerInstall
        {
            get { return mPassengerInstall; }
            set { mPassengerInstall = value; }
        }
        public decimal MediumTruckInstall
        {
            get { return mMediumTruckInstall; }
            set { mMediumTruckInstall = value; }
        }
        public decimal LawnDisposal
        {
            get { return mLawnDisposal; }
            set { mLawnDisposal = value; }
        }
        public decimal PassengerDisposal
        {
            get { return mPassengerDisposal; }
            set { mPassengerDisposal = value; }
        }
        public decimal MediumTruckDisposal
        {
            get { return mMediumTruckDisposal; }
            set { mMediumTruckDisposal = value; }
        }
        public decimal TaxRate
        {
            get { return mTaxRate; }
            set { mTaxRate = value; }
        }
        public decimal ServiceRate
        {
            get { return mServiceRate; }
            set { mServiceRate = value; }
        }
        public decimal Markup1Min
        {
            get { return mMarkup1Min; }
            set { mMarkup1Min = value; }
        }
        public decimal Markup1Max
        {
            get { return mMarkup1Max; }
            set { mMarkup1Max = value; }
        }
        public decimal Markup1Rate
        {
            get { return mMarkup1Rate; }
            set { mMarkup1Rate = value; }
        }
        public decimal Markup2Min
        {
            get { return mMarkup2Min; }
            set { mMarkup2Min = value; }
        }
        public decimal Markup2Max
        {
            get { return mMarkup2Max; }
            set { mMarkup2Max = value; }
        }
        public decimal Markup2Rate
        {
            get { return mMarkup2Rate; }
            set { mMarkup2Rate = value; }
        }
        public decimal Markup3Min
        {
            get { return mMarkup3Min; }
            set { mMarkup3Min = value; }
        }
        public decimal Markup3Max
        {
            get { return mMarkup3Max; }
            set { mMarkup3Max = value; }
        }
        public decimal Markup3Rate
        {
            get { return mMarkup3Rate; }
            set { mMarkup3Rate = value; }
        }
        public decimal UniversalMarkup
        {
            get { return mUniversalMarkup; }
            set { mUniversalMarkup = value; }
        }

        #endregion

        #region Private Members
        private decimal mLawnInstall;
        private decimal mPassengerInstall;
        private decimal mMediumTruckInstall;
        private decimal mLawnDisposal;
        private decimal mPassengerDisposal;
        private decimal mMediumTruckDisposal;
        private decimal mTaxRate;
        private decimal mServiceRate;
        private decimal mMarkup1Min;
        private decimal mMarkup1Max;
        private decimal mMarkup1Rate;
        private decimal mMarkup2Min;
        private decimal mMarkup2Max;
        private decimal mMarkup2Rate;
        private decimal mMarkup3Min;
        private decimal mMarkup3Max;
        private decimal mMarkup3Rate;
        private decimal mUniversalMarkup;

        #endregion

        #region Constructor

        public SettingsPageViewModel()
        {
            AssignValuesFromSettings();
            Save = new RelayCommand(SaveSettings);
            Cancel = new RelayCommand(ResetSettings);
        }

        #endregion

        #region Commands

        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }

        #endregion

        #region Helper Functions
        //Assigns all variables from Settings class to the view model properties
        public void AssignValuesFromSettings()
        {
            LawnInstall = Settings.LawnLaborRate;
            PassengerInstall = Settings.PassengerLaborRate;
            MediumTruckInstall = Settings.MediumTruckLaborRate;
            LawnDisposal = Settings.LawnDisposalRate;
            PassengerDisposal = Settings.PassengerDisposalRate;
            MediumTruckDisposal = Settings.MediumTruckDisposalRate;
            TaxRate = Settings.TaxRate;
            ServiceRate = Settings.ServiceFeeRate;
            try
            {
                Markup1Min = Settings.Markup[0].LowerRange;
                Markup1Max = Settings.Markup[0].UpperRange;
                Markup1Rate = Settings.Markup[0].Markup;
            }
            catch
            {
                Markup1Min = 0M;
                Markup1Max = 0M;
                Markup1Rate = Settings.UniversalMarkup;
            }
            try
            {
                Markup2Min = Settings.Markup[1].LowerRange;
                Markup2Max = Settings.Markup[1].UpperRange;
                Markup2Rate = Settings.Markup[1].Markup;
            }
            catch
            {
                Markup2Min = 0M;
                Markup2Max = 0M;
                Markup2Rate = Settings.UniversalMarkup;
            }
            try
            {
                Markup3Min = Settings.Markup[2].LowerRange;
                Markup3Max = Settings.Markup[2].UpperRange;
                Markup3Rate = Settings.Markup[2].Markup;
            }
            catch
            {
                Markup3Min = 0M;
                Markup3Max = 0M;
                Markup3Rate = Settings.UniversalMarkup;
            }
            UniversalMarkup = Settings.UniversalMarkup;


        }
        /// <summary>
        /// Saves changes made in the UI to the static Settings Class
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                try
                {
                    Settings.LawnLaborRate = LawnInstall;
                    Settings.PassengerLaborRate = PassengerInstall;
                    Settings.MediumTruckLaborRate = MediumTruckInstall;
                    Settings.LawnDisposalRate = LawnDisposal;
                    Settings.PassengerDisposalRate = PassengerDisposal;
                    Settings.MediumTruckDisposalRate = MediumTruckDisposal;
                    Settings.TaxRate = TaxRate;
                    Settings.ServiceFeeRate = ServiceRate;
                    Settings.UniversalMarkup = UniversalMarkup;
                    Settings.CreateOrOverwriteMarkup(new MarkupRate(Markup1Min, Markup1Max, Markup1Rate), 0);
                    Settings.CreateOrOverwriteMarkup(new MarkupRate(Markup2Min, Markup2Max, Markup2Rate), 1);
                    Settings.CreateOrOverwriteMarkup(new MarkupRate(Markup3Min, Markup3Max, Markup3Rate), 2);
                }
                catch
                {
                    throw new Exception("Settings Failed to Save: Check for invalid values");
                }
                Settings.SaveSettingsToFile();
            }
            catch
            {
                throw new Exception("Settings Failed to Save: Check for invalid values");
            }
        }
        //Assigns all values from the Settings class
        public void ResetSettings()
        {
            AssignValuesFromSettings();
        }

        #endregion
    }
}
