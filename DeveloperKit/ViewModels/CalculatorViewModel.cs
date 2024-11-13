using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Hikari.Mvvm.Command;

namespace DeveloperKit.ViewModels
{
    public class CalculatorViewModel
    {
        //private readonly Cauculator _calculatorModel;
        private DelegateCommand<string> _addCommand;


        public CalculatorViewModel()
        {
           // _calculatorModel = new Cauculator();
        }

        //#region Public Properties
        //public string FirstOperand
        //{
        //    get => _calculatorModel.FirstOperand;
        //    set => _calculatorModel.FirstOperand = value;
        //}
        //public string SecondOperand
        //{
        //    get => _calculatorModel.SecondOperand;
        //    set => _calculatorModel.SecondOperand = value;
        //}
        //public string Operation
        //{
        //    get => _calculatorModel.Operation;
        //    set => _calculatorModel.Operation = value;
        //}
        //public string Result
        //{
        //    get => _calculatorModel.Result;
        //    set => _calculatorModel.Result = value;
        //}
        //#endregion

        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand<string>(Add, CanAdd));

        public void Add(string x)
        {
            //FirstOperand = x;
            //SecondOperand = x;
            //Result = (double.Parse(FirstOperand) + double.Parse(SecondOperand)).ToString(CultureInfo.InvariantCulture);
            //Operation = "+";
            //MessageBox.Show(FirstOperand + Operation + SecondOperand + "=" + Result);
        }

        private static bool CanAdd(string num)
        {
            return true;
        }
    }
}