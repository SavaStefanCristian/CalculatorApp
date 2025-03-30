using CalculatorApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.ViewModel
{
    public class ProgrammerModeVM : BaseVM
    {
        private CommandHandler _commandHandler;
        public CommandHandler CommandHandler
        {
            get { return _commandHandler; }
            set
            {
                _commandHandler = value;
                OnPropertyChanged(nameof(CommandHandler));
            }
        }
    }
}
