using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace WrightTireEstimator
{
    class MainWindowViewModel : BaseViewModel
    {
        public Page CurrentPage
        {
            get { return mCurrentPage; }
            set { mCurrentPage = value; }
        }
        

        private Page mCurrentPage;
        private Window mWindow;
        private QuickQuote QQ;
        private SettingsPage SP;
        private QuickQuoteViewModel mQQVM;
        private SettingsPageViewModel mSPVM;

        public MainWindowViewModel(Window window)
        {
            mWindow = window;
            QQ = new QuickQuote();
            SP = new SettingsPage();
            mSPVM = new SettingsPageViewModel(); 
            mQQVM = new QuickQuoteViewModel(mWindow, QQ);
            CurrentPage = QQ;
            CurrentPage.DataContext = mQQVM;
            OpenSettings = new RelayCommand(SwitchPageToSettings);
            OpenQuickQuote = new RelayCommand(SwitchPageToQuickQuote);

        }

        #region Commands
        public ICommand OpenSettings { get; set; }
        public ICommand OpenQuickQuote { get; set; }
        #endregion

        public void SwitchPageToSettings()
        {
            CurrentPage = SP;
            CurrentPage.DataContext = mSPVM;
        }
        public void SwitchPageToQuickQuote()
        { 
            CurrentPage = QQ;
            CurrentPage.DataContext = mQQVM;
        }

    }
}
