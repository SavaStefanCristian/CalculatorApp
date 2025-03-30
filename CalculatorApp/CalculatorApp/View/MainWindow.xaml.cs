using CalculatorApp.ViewModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorApp.View;

public partial class MainWindow : Window
{
    private MainVM _mainVM;
    public MainWindow()
    {
        InitializeComponent();
        _mainVM = (MainVM)DataContext;
        Focus();

        

        switch (Properties.Settings.Default.Mode)
        {
            case "Standard":
                StandardButton.IsChecked = true;
                break;
            case "Programmer":
                ProgrammerButton.IsChecked = true;
                break;
            default:
                StandardButton.IsChecked = true;
                break;
        }

        switch (Properties.Settings.Default.DigitGrouping)
        {
            case "UK":
                UkGroupingButton.IsChecked = true;
                break;
            case "RO":
                RoGroupingButton.IsChecked = true;
                break;
            default:
                UkGroupingButton.IsChecked = true;
                break;
        }
    }




    private void StandardModeClicked(object sender, RoutedEventArgs e)
    {
        _mainVM.SetStandardMode();
        Properties.Settings.Default.Mode = "Standard";
        Properties.Settings.Default.Save();
    }
    private void ProgrammerModeClicked(object sender, RoutedEventArgs e)
    {
        _mainVM.SetProgrammerMode();
        Properties.Settings.Default.Mode = "Programmer";
        Properties.Settings.Default.Save();
    }

    private void UkGroupingClicked(object sender, RoutedEventArgs e)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-UK");
        _mainVM.CommandHandler.RefreshContent();

        Properties.Settings.Default.DigitGrouping = "UK";
        Properties.Settings.Default.Save();
    }
    private void RoGroupingClicked(object sender, RoutedEventArgs e)
    {
        CultureInfo.CurrentCulture = new CultureInfo("ro-RO");
        _mainVM.CommandHandler.RefreshContent();

        Properties.Settings.Default.DigitGrouping = "RO";
        Properties.Settings.Default.Save();
    }

    private void HandleKeyDown(object sender, KeyEventArgs args)
    {
        if (args.Handled) return;
        _mainVM.CurrentView.RaiseEvent(args);
    }

    private void AboutPressed(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Calculator App\nCreator: SavaStefanCristian");
    }

}