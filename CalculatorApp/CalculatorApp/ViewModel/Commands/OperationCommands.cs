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
        private ICommand _equalsCommand;
        public ICommand EqualsCommand
        {
            get
            {
                if (_equalsCommand == null) _equalsCommand = new RelayCommand(EqualsPressed);
                return _equalsCommand;
            }
        }
        private void EqualsPressed(object parameter)
        {
            if (parameter == null) return;
            string op = parameter as string;
            if (op == null || op != "=") return;

            if (_isJustOperatorPressed)
            {
                _currentValue = _previousValueString;
            }

            _processEquals();

            LastOperator = null;
            _isResult = true;
            _isJustOperatorPressed = false;
        }

        private void _processEquals()
        {
            if (LastOperator == null)
            {
                PreviousContent = CurrentContent + "=";
                _previousValueString = _currentValue;
                _currentValueString = "0";
                _previousContent = "";
                return;
            }

            PreviousContent += CurrentContent + "=";
            _previousContent = "";

            double previousValue, currentValue;


            if (!double.TryParse(Mode=="Programmer" ? FromCurrentToDec(_previousValueString):_previousValueString, out previousValue))
            {
                previousValue = 0;
            }

            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_currentValue):_currentValue, out currentValue))
            {
                currentValue = 0;
            }


            switch (LastOperator)
            {
                case "+":
                    _currentValue = Mode == "Programmer" ? FromDecToCurrent((previousValue + currentValue).ToString()): (previousValue + currentValue).ToString();
                    break;
                case "-":
                    _currentValue = Mode == "Programmer" ? FromDecToCurrent((previousValue - currentValue).ToString()):(previousValue - currentValue).ToString();
                    break;
                case "/":
                    if (currentValue == 0)
                    {
                        CurrentContent = "DIVISION BY 0";
                        _currentContent = "0";
                        _currentValueString = "0";
                    }
                    else _currentValue = Mode == "Programmer" ? FromDecToCurrent((previousValue / currentValue).ToString()): (previousValue / currentValue).ToString();
                    break;
                case "×":
                    _currentValue = Mode == "Programmer" ? FromDecToCurrent((previousValue * currentValue).ToString()): (previousValue * currentValue).ToString();
                    break;
            }

            _previousValueString = _currentValue;
        }

        private ICommand _operationCommand;
        public ICommand OperationCommand
        {
            get
            {
                if (_operationCommand == null) _operationCommand = new RelayCommand(OperationPressed);
                return _operationCommand;
            }
        }
        private void OperationPressed(object parameter)
        {
            if (parameter == null) return;
            string op = parameter as string;
            if (op == null) return;

            if (!("+-/×".Contains(op))) return;

            if (LastOperator != null)
            {
                if (_isJustOperatorPressed)
                {
                    LastOperator = op;
                    PreviousContent = PreviousContent.Remove(PreviousContent.Length - 1) + op;
                    return;
                }
                _processEquals();
                LastOperator = op;


            }
            else
            {
                LastOperator = op;
                if (!_isResult) 
                    _previousValueString = _currentValue;
                _isJustOperatorPressed = true;
            }


            PreviousContent += CurrentContent + op;
            _currentValueString = "0";




        }


        private ICommand _negateCommand;
        public ICommand NegateCommand
        {
            get
            {
                if (_negateCommand == null) _negateCommand = new RelayCommand(NegatePressed);
                return _negateCommand;
            }
        }
        private void NegatePressed(object parameter)
        {
            if (_currentValue.StartsWith("-"))
            {
                _currentValue = _currentValue.Substring(1);
            }
            else
            {
                _currentValue = "-" + _currentValue;
            }
            _isResult = false;
        }
        private ICommand _percentCommand;
        public ICommand PercentCommand
        {
            get
            {
                if (_percentCommand == null) _percentCommand = new RelayCommand(PercentPressed);
                return _percentCommand;
            }
        }
        private void PercentPressed(object parameter)
        {
            if (_isResult)
            {
                _currentValue = "0";
                PreviousContent = "";
                return;
            }

            if (LastOperator != null && LastOperator != "" && _previousValueString != null && _previousValueString != "")
            {
                double previousValue, currentValue;


                if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_previousValueString) : _previousValueString, out previousValue))
                {
                    previousValue = 0;
                }

                if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_currentValue) : _currentValue, out currentValue))
                {
                    currentValue = 0;
                }
                _currentValue = Mode == "Programmer" ? FromDecToCurrent(((previousValue * currentValue) / 100).ToString()): ((previousValue * currentValue) / 100).ToString();
            }
            else
            {
                _currentValue = "0";
                PreviousContent = "";
                return;
            }
        }

        private ICommand _unaryOperatorCommand;
        public ICommand UnaryOperatorCommand
        {
            get
            {
                if (_unaryOperatorCommand == null) _unaryOperatorCommand = new RelayCommand(UnaryPressed);
                return _unaryOperatorCommand;
            }
        }
        private void UnaryPressed(object parameter)
        {
            if (parameter == null) return;
            string op = parameter as string;
            if (op == null) return;

            double currentValue;

            if (!double.TryParse(_currentValue, out currentValue))
            {
                currentValue = 0;
            }

            switch (op)
            {
                case "1/x":
                    _currentValue = (1 / currentValue).ToString();
                    _isResult = false;
                    break;
                case "x2":
                    _currentValue = (currentValue * currentValue).ToString();
                    _isResult = false;
                    break;
                case "sqrt":
                    if (currentValue >= 0)
                    {
                        _currentValue = (Math.Sqrt(currentValue)).ToString();
                        _isResult = false;
                    }
                    break;
            }
        }
    }
}
