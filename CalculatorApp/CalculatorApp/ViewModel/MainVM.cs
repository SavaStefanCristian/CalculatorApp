using CalculatorApp.View;
using CalculatorApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorApp.ViewModel
{
    public class MainVM : BaseVM
    {
        public CommandHandler CommandHandler { get; set; }

        private UserControl _standardModeView;
        private UserControl _programmerModeView;


        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private string _currentViewName;
        public string CurrentViewName
        {
            get => _currentViewName;
            private set
            {
                _currentViewName = value;
                OnPropertyChanged(nameof(CurrentViewName));
            }
        }

        public void SetStandardMode()
        {
            CurrentView = _standardModeView;
            CurrentViewName = "Standard";
            CommandHandler.Mode = "Standard";
            CommandHandler.RefreshContent();
            CurrentView.Focus();
        }
        public void SetProgrammerMode()
        {
            CurrentView = _programmerModeView;
            CurrentViewName = "Programmer";
            CommandHandler.Mode = "Programmer";
            CommandHandler.RefreshContent();
            CurrentView.Focus();
        }

        public MainVM()
        {
            CommandHandler = new CommandHandler(this);
            _standardModeView = new StandardModeView(this);
            _programmerModeView = new ProgrammerModeView(this);
        }

    }
}
