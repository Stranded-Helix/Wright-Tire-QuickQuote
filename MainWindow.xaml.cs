using System;
using System.Windows;
using System.Windows.Controls;


namespace WrightTireEstimator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Initialized the Main Window//Start of Program
        public MainWindow()
        {
            //Load Settings
            Settings.LoadOrCreateSettings();

            //Intialize Dependencies
            Calculation calculation = new Calculation();
            InitializeComponent();
            MainWindowViewModel MWVM = new MainWindowViewModel(this);
            DataContext = MWVM;









        }

    }

}
