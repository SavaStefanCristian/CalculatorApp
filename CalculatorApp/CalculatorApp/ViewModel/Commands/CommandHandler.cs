using CalculatorApp.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CalculatorApp.ViewModel.Commands
{
    public partial class CommandHandler : BaseVM
    {
        private readonly MainVM _mainVM;

        public CommandHandler(MainVM mainVM)
        {
            _mainVM = mainVM;
            _currentValue = "0";
            PreviousContent = "";


        }


        private bool _isResult = false;
        private bool _isJustOperatorPressed = false;



        private string _currentContent;
        public string CurrentContent 
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                OnPropertyChanged(nameof(CurrentContent));
            }
        }

        private string _currentValueString;
        private string _currentValue
        {
            get => _currentValueString;
            set 
            {
                _currentValueString = value;
                if(Mode=="Standard" || Base == 10) CurrentContent = Utils.FormatCulture(_currentValue);
                if(Mode=="Programmer")
                {
                    string baseTenValue = "0";
                    switch(Base)
                    {
                        case 16:
                            baseTenValue = Utilities.Utils.FromHexToDec(_currentValueString);
                            CurrentContent = _currentValueString;
                            break;
                        case 10:
                            baseTenValue = _currentValueString;
                            break;
                        case 8:
                            baseTenValue = Utilities.Utils.FromBaseToDec(_currentValueString, 8);
                            CurrentContent = _currentValueString;
                            break;
                        case 2:
                            baseTenValue = Utilities.Utils.FromBaseToDec(_currentValueString, 2);
                            CurrentContent = _currentValueString;
                            break;
                    }

                    HexContent = Utilities.Utils.FromDecToHex(baseTenValue);
                    DecContent = Utils.FormatCulture(baseTenValue);
                    OctContent = Utilities.Utils.FromDecToBase(baseTenValue, 8);
                    BinContent = Utilities.Utils.FromDecToBase(baseTenValue, 2);

                }
            }
        }

        private string _previousValueString { get; set; }

        private string _previousContent;
        public string PreviousContent
        {
            get => _previousContent;
            set
            {
                _previousContent = value;
                OnPropertyChanged(nameof(PreviousContent));
            }

        }

        public string? LastOperator { get; set; } = null;





        #region Input

        private ICommand _digitCommand;
        public ICommand DigitCommand
        {
            get
            {
                if(_digitCommand == null) _digitCommand = new RelayCommand(DigitPressed);
                return _digitCommand;
            }
        }
        private void DigitPressed(object parameter)
        {
            if (parameter == null) return;

            

            _isJustOperatorPressed = false;

            if (_isResult)
            {
                _isResult = false;
                _currentValue = parameter.ToString();
                if (LastOperator == null) PreviousContent = "";
            }
            else if (_currentValue == "0")
            {
                _currentValue = parameter.ToString();
            }
            else if (_currentValue == "-0")
            {
                _currentValue = "-" + parameter.ToString();
            }
            else
            {
                if (CurrentContent.Length > (Mode=="Programmer"? 10 : 14)) return;
                _currentValue += parameter;
            }
        }

        private ICommand _dotCommand;
        public ICommand DotCommand
        {
            get
            {
                if (_dotCommand == null) _dotCommand = new RelayCommand(DotPressed);
                return _dotCommand;
            }
        }
        private void DotPressed(object parameter)
        {
            if (_isResult)
            {
                _currentValue = "0";
                PreviousContent = "";
                return;
            }

            if (!_currentValue.Contains('.'))
            {
                _currentValue += ".";
            }
        }

        #endregion

        #region Modify Input

        private ICommand _backspaceCommand;
        public ICommand BackspaceCommand
        {
            get
            {
                if (_backspaceCommand == null) _backspaceCommand = new RelayCommand(BackspacePressed);
                return _backspaceCommand;
            }
        }
        private void BackspacePressed(object parameter)
        {
            if(_isResult)
            {
                _currentValue = "0";
                PreviousContent = "";
                return;
            }
            if (_currentValue != "0")
            {
                if (_currentValue.Length == 1) _currentValue = "0";
                else _currentValue = _currentValue.Remove(_currentValue.Length - 1);
            }
            if(_currentValue == "-")
            {
                _currentValue = "0";
            }
        }

        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null) _clearCommand = new RelayCommand(ClearButtonPressed);
                return _clearCommand;
            }
        }

        private void ClearButtonPressed(object parameter)
        {
            if (parameter == null) return;
            string clearType = parameter as string;
            if (clearType == null) return;

            switch(clearType)
            {
                case "CE":
                    _currentValue = "0";
                    if(_isResult)
                    {
                        LastOperator = null;
                        PreviousContent = "";
                        _previousValueString = "";
                    }
                    break;

                case "C":
                    _currentValue = "0";
                    LastOperator = null;
                    PreviousContent = "";
                    _previousValueString = "";
                    break;
            }

        }

        #endregion


    }
}
