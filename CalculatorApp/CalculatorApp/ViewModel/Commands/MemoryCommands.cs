using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CalculatorApp.ViewModel.Commands
{
    public partial class CommandHandler
    {

        public class MemoryItem
        {
            public MemoryItem(int index, string value)
            {
                Index = index;
                Value = value;
            }
            public int Index { get; set; }
            public string Value { get; set; }
        }



        private ObservableCollection<MemoryItem> _memory;

        public ObservableCollection<MemoryItem> Memory
        {
            get => _memory;
            private set
            {
                _memory = value;
                OnPropertyChanged(nameof(Memory));
            }
        }


        private List<string> _memorizedValues = new List<string>();

        private List<string> MemorizedValues
        {
            get => _memorizedValues;
            set
            {
                _memorizedValues = value;

                ObservableCollection<MemoryItem> formattedMemory = new ObservableCollection<MemoryItem>();

                for(int i = 0; i < _memorizedValues.Count; ++i)
                {
                    if(Mode=="Programmer")
                    {
                        formattedMemory.Add(new MemoryItem(i, _memorizedValues[i]));
                    }
                    else formattedMemory.Add(new MemoryItem(i,Utilities.Utils.FormatCulture(_memorizedValues[i])));
                }

                Memory = formattedMemory;


            }
            
        }

        #region Memory Commands

        private ICommand _memSaveCommand;
        public ICommand MemSaveCommand
        {
            get
            {
                if (_memSaveCommand == null) _memSaveCommand = new RelayCommand(MemorySave);
                return _memSaveCommand;
            }
        }
        private void MemorySave(object parameter)
        {
            MemorizedValues.Insert(0, _currentValue);
            MemorizedValues = MemorizedValues;
        }


        
        private ICommand _memClearCommand;
        public ICommand MemClearCommand
        {
            get
            {
                if (_memClearCommand == null) _memClearCommand = new RelayCommand(MemoryClear);
                return _memClearCommand;
            }
        }
        private void MemoryClear(object parameter)
        {
            MemorizedValues.Clear();
            MemorizedValues = MemorizedValues;
        }




        private ICommand _memOperatorCommand;
        public ICommand MemOperatorCommand
        {
            get
            {
                if (_memOperatorCommand == null) _memOperatorCommand = new RelayCommand(MemoryOperatorPressed);
                return _memOperatorCommand;
            }
        }
        private void MemoryOperatorPressed(object parameter)
        {
            

            if (parameter == null) return;
            string op = parameter as string;
            if (op == null) return;


            double memoryValue, currentValue;

            if (MemorizedValues.Count == 0)
            {
                if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_currentValue) : _currentValue, out currentValue))
                {
                    currentValue = 0;
                }
                switch (op)
                {
                    case "+":
                        MemorizedValues.Add(Mode == "Programmer" ? FromDecToCurrent(currentValue.ToString()): currentValue.ToString());
                        break;
                    case "-":
                        MemorizedValues.Add(Mode == "Programmer" ? FromDecToCurrent((-currentValue).ToString()): (-currentValue).ToString());
                        break;
                }

                MemorizedValues = MemorizedValues;

                return;
            }



            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(MemorizedValues[0]) : MemorizedValues[0], out memoryValue))
            {
                memoryValue = 0;
            }

            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_currentValue) : _currentValue, out currentValue))
            {
                currentValue = 0;
            }

            switch (op)
            {
                case "+":
                    MemorizedValues[0] = Mode == "Programmer" ? FromDecToCurrent((memoryValue + currentValue).ToString()): (memoryValue + currentValue).ToString();
                    break;
                case "-":
                    MemorizedValues[0] = Mode == "Programmer" ? FromDecToCurrent((memoryValue - currentValue).ToString()): (memoryValue + currentValue).ToString();
                    break;
            }
            MemorizedValues = MemorizedValues;

        }



        private ICommand _memRecallCommand;
        public ICommand MemRecallCommand
        {
            get
            {
                if (_memRecallCommand == null) _memRecallCommand = new RelayCommand(MemoryRecall);
                return _memRecallCommand;
            }
        }
        private void MemoryRecall(object parameter)
        {
            if (MemorizedValues.Count > 0)
                _currentValue = MemorizedValues[0];
            _isResult = false;
        }

        #endregion

        #region Memory Item Commands

        private ICommand _memAddInItemCommand;
        public ICommand MemAddInItemCommand
        {
            get
            {
                if (_memAddInItemCommand == null) _memAddInItemCommand = new RelayCommand(MemoryAddInItem);
                return _memAddInItemCommand;
            }
        }
        private void MemoryAddInItem(object parameter)
        {
            if (parameter == null) return;
            int index = (int)parameter;

            if (index < 0 || MemorizedValues.Count <= index) return;


            double memoryValue, currentValue;

            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(MemorizedValues[index]) : MemorizedValues[index], out memoryValue))
            {
                memoryValue = 0;
            }

            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_currentValue) : _currentValue, out currentValue))
            {
                currentValue = 0;
            }

            MemorizedValues[index] = Mode == "Programmer" ? FromDecToCurrent((memoryValue + currentValue).ToString()): (memoryValue + currentValue).ToString();

            MemorizedValues = MemorizedValues;
        }





        private ICommand _memSubtractInItemCommand;
        public ICommand MemSubtractInItemCommand
        {
            get
            {
                if (_memSubtractInItemCommand == null) _memSubtractInItemCommand = new RelayCommand(MemorySubtractInItem);
                return _memSubtractInItemCommand;
            }
        }
        private void MemorySubtractInItem(object parameter)
        {
            if (parameter == null) return;
            int index = (int)parameter;

            if (index < 0 || MemorizedValues.Count <= index) return;


            double memoryValue, currentValue;

            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(MemorizedValues[index]) : MemorizedValues[index], out memoryValue))
            {
                memoryValue = 0;
            }

            if (!double.TryParse(Mode == "Programmer" ? FromCurrentToDec(_currentValue) : _currentValue, out currentValue))
            {
                currentValue = 0;
            }

            MemorizedValues[index] = Mode == "Programmer" ? FromDecToCurrent((memoryValue - currentValue).ToString()): (memoryValue - currentValue).ToString();

            MemorizedValues = MemorizedValues;
        }




        private ICommand _memRemoveItemCommand;
        public ICommand MemRemoveItemCommand
        {
            get
            {
                if (_memRemoveItemCommand == null) _memRemoveItemCommand = new RelayCommand(MemoryRemoveItem);
                return _memRemoveItemCommand;
            }
        }
        private void MemoryRemoveItem(object parameter)
        {
            if (parameter == null) return;
            int index = (int) parameter;

            if (index < 0 || MemorizedValues.Count <= index) return;

            MemorizedValues.RemoveAt(index);

            MemorizedValues = MemorizedValues;

        }


        public void MemoryItemClicked(object parameter)
        {
            MemoryItem item = (MemoryItem) parameter;
            if (item == null) return;
            if (item.Index < 0 || MemorizedValues.Count <= item.Index) return;

            _currentValue = MemorizedValues[item.Index];

            _isResult = false;
            _isJustOperatorPressed = false;

        }


        #endregion

    }
}
