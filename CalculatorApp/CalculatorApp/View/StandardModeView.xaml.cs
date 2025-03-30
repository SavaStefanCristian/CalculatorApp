using CalculatorApp.ViewModel;
using CalculatorApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
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
    public partial class StandardModeView : UserControl
    {
        private readonly MainVM _mainVM;
        public StandardModeVM StandardModeVM
        {
            get;
            private set;
        }
        public StandardModeView(MainVM mainVM)
        {
            InitializeComponent();
            StandardModeVM = (StandardModeVM)DataContext;
            StandardModeVM.CommandHandler = mainVM.CommandHandler;
            _mainVM = mainVM;
            MemoryList.Visibility = Visibility.Collapsed;
        }

        private void HandleKeyDown(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("0");
                    break;
                case Key.D1:
                case Key.NumPad1:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    StandardModeVM.CommandHandler.DigitCommand.Execute("9");
                    break;
                case Key.Add:
                case Key.OemPlus: 
                    StandardModeVM.CommandHandler.OperationCommand.Execute("+");
                    break;
                case Key.Subtract:
                case Key.OemMinus: 
                    StandardModeVM.CommandHandler.OperationCommand.Execute("-");
                    break;
                case Key.Multiply:
                    StandardModeVM.CommandHandler.OperationCommand.Execute("×");
                    break;
                case Key.Divide:
                case Key.Oem2: 
                    StandardModeVM.CommandHandler.OperationCommand.Execute("/");
                    break;
                case Key.Decimal:
                case Key.OemPeriod:
                    StandardModeVM.CommandHandler.DotCommand.Execute("");
                    break;
                case Key.Enter:
                    StandardModeVM.CommandHandler.EqualsCommand.Execute("=");
                    break;
                case Key.Back:
                    StandardModeVM.CommandHandler.BackspaceCommand.Execute("");
                    break;
                case Key.Escape:
                    StandardModeVM.CommandHandler.ClearCommand.Execute("C");
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
    }
}
