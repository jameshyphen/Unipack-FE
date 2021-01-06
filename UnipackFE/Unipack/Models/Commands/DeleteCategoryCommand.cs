using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unipack.ViewModels;

namespace Unipack.Models.Commands
{
    class DeleteCategoryCommand : ICommand
    {
        CategoryViewModel _viewModel;
        
        public DeleteCategoryCommand(CategoryViewModel cVM)
        {
            _viewModel = cVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(CanExecute(parameter))
                _viewModel.DeleteCategory(parameter as string);
        }
    }
}
