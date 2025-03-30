using CalculatorApp.ViewModel;
using CalculatorApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorApp.View
{
    public partial class ProgrammerModeView : UserControl
    {
        private readonly MainVM _mainVM;
        public ProgrammerModeVM ProgrammerModeVM
        {
            get;
            private set;
        }
        public ProgrammerModeView(MainVM mainVM)
        {
            InitializeComponent();
            ProgrammerModeVM = (ProgrammerModeVM) DataContext;
            ProgrammerModeVM.CommandHandler = mainVM.CommandHandler;
            _mainVM = mainVM;
            MemoryList.Visibility = Visibility.Collapsed;


            switch (Properties.Settings.Default.Base)
            {
                case 16:
                    HexButton.IsChecked = true;
                    break;
                case 10:
                    DecButton.IsChecked = true;
                    break;
                case 8:
                    OctButton.IsChecked = true;
                    break;
                case 2:
                    BinButton.IsChecked = true;
                    break;
                default:
                    DecButton.IsChecked = true;
                    break;
            }


        }


        private void HandleKeyDown(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("0");
                    break;
                case Key.D1:
                case Key.NumPad1:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    ProgrammerModeVM.CommandHandler.DigitCommand.Execute("9");
                    break;
                case Key.Add:
                case Key.OemPlus:
                    ProgrammerModeVM.CommandHandler.OperationCommand.Execute("+");
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    ProgrammerModeVM.CommandHandler.OperationCommand.Execute("-");
                    break;
                case Key.Multiply:
                    ProgrammerModeVM.CommandHandler.OperationCommand.Execute("×");
                    break;
                case Key.Divide:
                case Key.Oem2:
                    ProgrammerModeVM.CommandHandler.OperationCommand.Execute("/");
                    break;
                case Key.Decimal:
                case Key.OemPeriod:
                    ProgrammerModeVM.CommandHandler.DotCommand.Execute("");
                    break;
                case Key.Enter:
                    ProgrammerModeVM.CommandHandler.EqualsCommand.Execute("=");
                    break;
                case Key.Back:
                    ProgrammerModeVM.CommandHandler.BackspaceCommand.Execute("");
                    break;
                case Key.Escape:
                    ProgrammerModeVM.CommandHandler.ClearCommand.Execute("C");
                    break;
            }
            args.Handled = true;
        }

        private void ShowMemory(object sender, RoutedEventArgs e)
        {
            MemoryList.Visibility = MemoryList.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MemoryListMouseDown(object sender, SelectionChangedEventArgs e)
        {
            _mainVM.CommandHandler.MemoryItemClicked(((ListBox)sender).SelectedItem);
        }


        private void HexButtonPressed(object sender, RoutedEventArgs e)
        {
            _mainVM.CommandHandler.ChangeBase(16);
            Properties.Settings.Default.Base = 16;
            Properties.Settings.Default.Save();

            Button0.IsEnabled = true;
            Button1.IsEnabled = true;
            Button2.IsEnabled = true;
            Button3.IsEnabled = true;
            Button4.IsEnabled = true;
            Button5.IsEnabled = true;
            Button6.IsEnabled = true;
            Button7.IsEnabled = true;
            Button8.IsEnabled = true;
            Button9.IsEnabled = true;

            ButtonA.IsEnabled = true;
            ButtonB.IsEnabled = true;
            ButtonC.IsEnabled = true;
            ButtonD.IsEnabled = true;
            ButtonE.IsEnabled = true;
            ButtonF.IsEnabled = true;

        }
        private void DecButtonPressed(object sender, RoutedEventArgs e)
        {
            _mainVM.CommandHandler.ChangeBase(10);
            Properties.Settings.Default.Base = 10;
            Properties.Settings.Default.Save();

            Button0.IsEnabled = true;
            Button1.IsEnabled = true;
            Button2.IsEnabled = true;
            Button3.IsEnabled = true;
            Button4.IsEnabled = true;
            Button5.IsEnabled = true;
            Button6.IsEnabled = true;
            Button7.IsEnabled = true;
            Button8.IsEnabled = true;
            Button9.IsEnabled = true;

            ButtonA.IsEnabled = false;
            ButtonB.IsEnabled = false;
            ButtonC.IsEnabled = false;
            ButtonD.IsEnabled = false;
            ButtonE.IsEnabled = false;
            ButtonF.IsEnabled = false;
        }
        private void OctButtonPressed(object sender, RoutedEventArgs e)
        {
            _mainVM.CommandHandler.ChangeBase(8);
            Properties.Settings.Default.Base = 8;
            Properties.Settings.Default.Save();

            Button0.IsEnabled = true;
            Button1.IsEnabled = true;
            Button2.IsEnabled = true;
            Button3.IsEnabled = true;
            Button4.IsEnabled = true;
            Button5.IsEnabled = true;
            Button6.IsEnabled = true;
            Button7.IsEnabled = true;
            Button8.IsEnabled = false;
            Button9.IsEnabled = false;

            ButtonA.IsEnabled = false;
            ButtonB.IsEnabled = false;
            ButtonC.IsEnabled = false;
            ButtonD.IsEnabled = false;
            ButtonE.IsEnabled = false;
            ButtonF.IsEnabled = false;
        }
        private void BinButtonPressed(object sender, RoutedEventArgs e)
        {
            _mainVM.CommandHandler.ChangeBase(2);
            Properties.Settings.Default.Base = 2;
            Properties.Settings.Default.Save();

            Button0.IsEnabled = true;
            Button1.IsEnabled = true;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Button5.IsEnabled = false;
            Button6.IsEnabled = false;
            Button7.IsEnabled = false;
            Button8.IsEnabled = false;
            Button9.IsEnabled = false;

            ButtonA.IsEnabled = false;
            ButtonB.IsEnabled = false;
            ButtonC.IsEnabled = false;
            ButtonD.IsEnabled = false;
            ButtonE.IsEnabled = false;
            ButtonF.IsEnabled = false;
        }



    }
}
