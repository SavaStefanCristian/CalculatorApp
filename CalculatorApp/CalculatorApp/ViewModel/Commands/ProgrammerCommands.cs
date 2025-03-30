using CalculatorApp.Properties;
using CalculatorApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorApp.ViewModel.Commands
{
    public partial class CommandHandler
    {
        private string _mode;
        public string Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                ClearButtonPressed("C");
                MemoryClear("");
            }
        }

        public int Base { get; set; }

        private string _hexContent;
        public string HexContent
        {
            get => _hexContent;
            set
            {
                _hexContent = value;
                OnPropertyChanged(nameof(HexContent));
            }
        }

        private string _decContent;
        public string DecContent
        {
            get => _decContent;
            set
            {
                _decContent = value;
                OnPropertyChanged(nameof(DecContent));
            }
        }

        private string _octContent;
        public string OctContent
        {
            get => _octContent;
            set
            {
                _octContent = value;
                OnPropertyChanged(nameof(OctContent));
            }
        }

        private string _binContent;
        public string BinContent
        {
            get => _binContent;
            set
            {
                _binContent = value;
                OnPropertyChanged(nameof(BinContent));
            }
        }









        public void ChangeBase(int numBase)
        {
            if (numBase != 2 && numBase != 8 && numBase != 10 && numBase != 16) return;

            string baseTenValue = FromCurrentToDec(_currentValueString);
            
            string baseTenValuePrev = FromCurrentToDec(_previousValueString);

            List<string> newMemory = MemorizedValues.Select(FromCurrentToDec).ToList();




            Base = numBase;

            switch (numBase)
            {
                case 16:
                    _previousValueString = Utilities.Utils.FromDecToHex(baseTenValuePrev);
                    _currentValue = Utilities.Utils.FromDecToHex(baseTenValue);
                    MemorizedValues = newMemory.Select(Utilities.Utils.FromDecToHex).ToList();
                    break;
                case 10:
                    _previousValueString = baseTenValuePrev;
                    _currentValue = baseTenValue;
                    MemorizedValues = newMemory;
                    break;
                case 8:
                    _previousValueString = Utilities.Utils.FromDecToBase(baseTenValuePrev, 8);
                    _currentValue = Utilities.Utils.FromDecToBase(baseTenValue, 8);
                    MemorizedValues = newMemory.Select((element) => Utilities.Utils.FromDecToBase(element,8)).ToList();
                    break;
                case 2:
                    _previousValueString = Utilities.Utils.FromDecToBase(baseTenValuePrev, 2);
                    _currentValue = Utilities.Utils.FromDecToBase(baseTenValue, 2);
                    MemorizedValues = newMemory.Select((element) => Utilities.Utils.FromDecToBase(element, 2)).ToList();
                    break;
            }

            _previousContent = "";

            RefreshContent();

        }



        private string FromDecToCurrent(string value)
        {
            string result = "0";
            switch (Base)
            {
                case 16:
                    result = Utilities.Utils.FromDecToHex(value);
                    break;
                case 10:
                    result = value;
                    break;
                case 8:
                    result = Utilities.Utils.FromDecToBase(value, 8);
                    break;
                case 2:
                    result = Utilities.Utils.FromDecToBase(value, 2);
                    break;
            }
            return result;
        }

        private string FromCurrentToDec(string value)
        {
            string result = "0";
            switch (Base)
            {
                case 16:
                    result = Utilities.Utils.FromHexToDec(value);
                    break;
                case 10:
                    result = value;
                    break;
                case 8:
                    result = Utilities.Utils.FromBaseToDec(value, 8);
                    break;
                case 2:
                    result = Utilities.Utils.FromBaseToDec(value, 2);
                    break;
            }
            return result;
        }




    }
}
