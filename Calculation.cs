using System;

namespace WrightTireEstimator
{
    class Calculation
    {
        #region Public Properties
        public decimal Cost { get; set; }
        public decimal Markup { get; set; }

        #endregion

        #region Constructor
        public Calculation()
        {
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Takes a string and attempts to convert it to a decimal and assigns it to private members
        /// </summary>
        /// <param name="input"></param>
        public bool GetCost(string input)
        {
            try
            {
                this.Cost = Convert.ToDecimal(input);
                Markup = 1 + Settings.CheckMarkup(Cost);
                return true;
            }
            catch
            {
                throw new Exception("Input could not be converted to a valid value");
            }
        }
        /// <summary>
        /// Calculates the prices based on settings and Markup Range
        /// </summary>
        /// <returns></returns>
        public static void Calculate(QuoteState quoteState)
        {
            decimal Labor;
            switch (quoteState)
            {
                case QuoteState.Lawn:
                    Labor = Settings.LawnLaborRate;
                    break;

                case QuoteState.Passenger:
                    Labor = Settings.PassengerLaborRate;
                    break;

                case QuoteState.MediumTruck:
                    Labor = Settings.MediumTruckLaborRate;
                    break;

                default:
                    Labor = 16M;
                    break;

            }

            //>>>>>>>>>>>>>>>>Rework to calculate based on taxed or taxed exempt
            /*
            for (int i = 0; i < 6; i++)
            {
                allResults[i] = (Cost * Markup * (i + 1)) + (Labor * (i + 1)) + (Labor * (i + 1) * Settings.ServiceFeeRate);
            }
            for (int i = 6; i < 12; i++)
            {
                allResults[i] = (Cost * Markup + (Labor * Settings.ServiceFeeRate)) * (i - 5) * Settings.TaxRate + (Labor * (i - 5));
            }
            */
        }
        #endregion
    }
}
