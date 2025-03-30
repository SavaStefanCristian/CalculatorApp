using CalculatorApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorApp.ViewModel.Commands
{
    public partial class CommandHandler
    {
        public void RefreshContent()
        {
            CurrentContent = Mode=="Programmer"?_currentValue:Utils.FormatCulture(_currentValue);
            MemorizedValues = MemorizedValues;
        }

        private ICommand _fileCommand;
        public ICommand FileCommand
        {
            get
            {
                if (_fileCommand == null) _fileCommand = new RelayCommand(FileCommandPressed);
                return _fileCommand;
            }
        }
        private void FileCommandPressed(object parameter)
        {

            if (parameter == null) return;
            string actionType = parameter as string;
            if (actionType == null) return;

            switch (actionType)
            {
                case "Cut":
                    System.Windows.Clipboard.SetText(CurrentContent);
                    ClearButtonPressed("CE");
                    break;
                case "Copy":
                    System.Windows.Clipboard.SetText(CurrentContent);
                    break;
                case "Paste":
                    if (Mode == "Programmer") break;
                    double value;
                    if (double.TryParse(System.Windows.Clipboard.GetText(), out value))
                    {
                        _currentValue = value.ToString();
                        _isResult = false;
                        _isJustOperatorPressed = false;
                    }
                    break;
            }

        }
    }
}
