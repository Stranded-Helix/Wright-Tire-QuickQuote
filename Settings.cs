using System;
using System.Collections.Generic;
using System.IO;

namespace WrightTireEstimator
{
    /// <summary>
    /// The settings used to caclulate estimates
    /// </summary>
    public static class Settings
    {
        #region Public Properties
        //Tax Rate
        public static decimal TaxRate { get; set; }
        //Lawn Labor install costs
        public static decimal LawnLaborRate { get; set; }
        //Passenger Labor install costs
        public static decimal PassengerLaborRate { get; set; }
        //Light Truck Labor install costs
        public static decimal MediumTruckLaborRate { get; set; }
        //Lawn Tire Disposal rate
        public static decimal LawnDisposalRate { get; set; }
        //Passenger Tire Disposal rate
        public static decimal PassengerDisposalRate { get; set; }
        //Medium Truck Tire Disposal rate
        public static decimal MediumTruckDisposalRate { get; set; }
        //Percentage of labor charges to charge as service fee
        public static decimal ServiceFeeRate { get; set; }
        //Universal markup rate
        public static decimal UniversalMarkup { get; set; }
        //Increased Markup rates and ranges
        public static List<MarkupRate> Markup = new List<MarkupRate>();

        #endregion

        #region Helper Methods
        /// <summary>
        /// Checks if the settings file exists, then loads it or creates a new file with default values
        /// </summary>
        public static void LoadOrCreateSettings()
        {
            //Checks if file exists
            if (File.Exists("settings.txt"))
            {
                //Loads values from file
                ReadSettings();
            }
            else
            {
                //Creates a text file with default values
                File.Create("settings.txt").Close();
                StreamWriter writer = new StreamWriter("settings.txt");
                writer.WriteLine("1.0825");
                writer.WriteLine("10.00");
                writer.WriteLine("15.00");
                writer.WriteLine("25.00");
                writer.WriteLine(".15");
                writer.WriteLine(".20");
                writer.WriteLine("0,20,.50");
                writer.WriteLine("20,25,.40");
                writer.WriteLine("25,35,.30");
                writer.Close();
                TaxRate = 1.0825M;
                LawnLaborRate = 10.00M;
                PassengerLaborRate = 15.00M;
                MediumTruckLaborRate = 25.00M;
                ServiceFeeRate = .15M;
                UniversalMarkup = .20M;

                Markup.Add(new MarkupRate(0M, 20M, .50M));
                Markup.Add(new MarkupRate(20M, 25M, .50M));
                Markup.Add(new MarkupRate(25M, 35M, .50M));

            }
        }
        //Checks if the cost of an item is in any range within the MarkupRate List and returns markup percentage
        public static decimal CheckMarkup(decimal cost)
        {
            foreach(var v in Markup)
            {
                if(cost>v.LowerRange && cost<=v.UpperRange)
                {
                    return v.Markup;
                }
                
            }
            return UniversalMarkup;
        }
        /// <summary>
        /// Assigns values from text file to Settings class properties
        /// </summary>
        private static void ReadSettings()
        {
            //reads strings from file
            var text = File.ReadAllLines("settings.txt");
            foreach (var a in text)
            {
                a.ToUpper();
                string[] settings = new string[2];
                settings = a.Split(':');
                switch(settings[0])
                {
                    case "LAWNLABORRATE":
                        LawnLaborRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "PASSENGERLABORRATE":
                        PassengerLaborRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "MEDIUMTRUCKLABORRATE":
                        MediumTruckLaborRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "LAWNDISPOSAL":
                        LawnDisposalRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "PASSENGERDISPOSAL":
                        PassengerDisposalRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "MEDIUMTRUCKDISPOSAL":
                        MediumTruckDisposalRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "TAXRATE":
                        TaxRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "SERVICERATE":
                        ServiceFeeRate = Convert.ToDecimal(settings[1]);
                        break;

                    case "UNIVERSALMARKUP":
                        UniversalMarkup  = Convert.ToDecimal(settings[1]);
                        break;

                    case "MARKUP":

                        string[] temp = new string[3];
                            temp = settings[1].Split(',');
                            Markup.Add(new MarkupRate(Convert.ToDecimal(temp[0]), Convert.ToDecimal(temp[1]), Convert.ToDecimal(temp[2])));

                        break;
                }
            }
        }
        /// <summary>
        /// Writes Identifiers with values into a text file
        /// </summary>
        public static void SaveSettingsToFile()
        {
            
            StreamWriter writer = new StreamWriter("settings.txt");
            writer.WriteLine("TAXRATE:{0}",TaxRate);
            writer.WriteLine("LAWNLABORRATE:{0}",LawnLaborRate);
            writer.WriteLine("PASSENGERLABORRATE:{0}",PassengerLaborRate);
            writer.WriteLine("MEDIUMTRUCKLABORRATE:{0}",MediumTruckLaborRate);
            writer.WriteLine("LAWNDISPOSAL:{0}", LawnDisposalRate);
            writer.WriteLine("PASSENGERDISPOSAL:{0}", PassengerDisposalRate);
            writer.WriteLine("MEDIUMTRUCKDISPOSAL:{0}", MediumTruckDisposalRate);
            writer.WriteLine("SERVICERATE:{0}",ServiceFeeRate);
            writer.WriteLine("UNIVERSALMARKUP:{0}",UniversalMarkup);
            writer.WriteLine("MARKUP:{0},{1},{2}",Markup[0].LowerRange,Markup[0].UpperRange,Markup[0].Markup);
            writer.WriteLine("MARKUP:{0},{1},{2}",Markup[1].LowerRange, Markup[1].UpperRange, Markup[1].Markup);
            writer.WriteLine("MARKUP:{0},{1},{2}",Markup[2].LowerRange, Markup[2].UpperRange, Markup[2].Markup);
            writer.Close();
            
        }
        /// <summary>
        /// ardcoded overwrite function for the SettingsPageViewModel
        /// </summary>
        /// <param name="markup">MarkupRate object that contains lower, upper, and rate values</param>
        /// <param name="index">the index in the MarkupRate List<></param>
        public static void CreateOrOverwriteMarkup(MarkupRate markup, int index)
        {
            Markup[index] = markup;
        }
        #endregion
    }
}
