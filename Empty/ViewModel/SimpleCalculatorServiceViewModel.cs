using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Empty
{
	public class SimpleCalculatorServiceViewModel : INotifyPropertyChanged
	{
		//Поля
		private string inputString = "";
		private string displayText = "0";

		//Свойства
		public static string s_Operation {get;set;}
		public double Value{get;set;}
		public bool Operation_pressed{get;set;}
		public bool Point_pressed{get;set;}
		public bool Button_pressed{get;set;}
		public string InputString
		{
			set
			{
				if (inputString != value){
					inputString = value;
					OnPropertyChanged("InputString");
				}
			}
			get { return inputString; }
		}

		public string DisplayText
		{
			set
			{
				if (displayText != value){
					displayText = value;
					OnPropertyChanged("DisplayText");
				}
			}
			get { return displayText; }
		}

		public ICommand CharAddCommand { protected set; get; }
		public ICommand OperationAddCommand { protected set; get; }
		public ICommand AllCleaningCommand { protected set; get; }
		public ICommand LastValueCleaningCommand { protected set; get; }

		public event PropertyChangedEventHandler PropertyChanged;

		//Конструктор
		public SimpleCalculatorServiceViewModel()
		{			
			this.CharAddCommand = new Command<string> (AddChar);
			this.OperationAddCommand = new Command<string> (OperationAdd);
			this.AllCleaningCommand = new Command(AllCleaning);
			this.LastValueCleaningCommand = new Command(LastValueCleaning);
		}

		//AllCleaning - метод сбрасывает все поля в значения по умолчанию
		public void AllCleaning ()
		{			
				InputString = "";
				DisplayText = "0";
				Value = 0;
				s_Operation = "";
				Point_pressed = false;
				Operation_pressed = false;
				Button_pressed = false;			
		}

		//LastValueCleaning - метод сбрасывает последнее набранное значение
		public void LastValueCleaning ()
		{		
				InputString = "";
				DisplayText = "0";
				Point_pressed = false;
		}

		//AddChar - метод добавляет в окно вывода цыфры которые нажимает пользователь
		public void AddChar (string key)
		{
			Button_pressed = true;

			//Очищение InputString/DisplayText
			bool cleanOutScreen = (DisplayText == "0"|| Operation_pressed || key == "На ноль делить нельзя!");
			if (cleanOutScreen)
				InputString = "";

		    //Проверка на первую нажатую "."
			if (key == "." && DisplayText == "0" || Operation_pressed && key == "."){
				InputString = "0.";
				Point_pressed = true;
			}

			//CorectOutput - слидит за корректностью ввода данных
			CorectInput (key);				
			Operation_pressed = false;
		}

		//OperationAdd - работа над арифметическими операциями
		public void OperationAdd(string operation)
		{
			Operation_pressed = true;
			Point_pressed = false;

			if (InputString != "") {
				//Арифметические операции
				ArithmeticOperations (operation);
			}
		}

		//В зависимости от входящей операции производятся арифметические действия
		public void ArithmeticOperations(string operation)
		{
			if (Value == 0)
				Value = Convert.ToDouble (InputString);

			//Проверка на нажатие кнопки с цифрой и реализация арифметических операций
			if (Button_pressed == true){
				switch (s_Operation){

				case "+":
					Button_pressed = false;
					Value = new SimpleCalculatorService ().Summ (Value, Convert.ToDouble (InputString));
					//Value = DependencyService.Get<ICalculatorService> ().Summ (Value, Convert.ToDouble (InputString));
					InputString = Convert.ToString (Value);
					break;
				case "-":
					Button_pressed = false;
					Value = DependencyService.Get<ICalculatorService> ().Diff (Value, Convert.ToDouble (InputString));
					InputString = Convert.ToString (Value);
					break;
				case "*":
					Button_pressed = false;
					Value = DependencyService.Get<ICalculatorService> ().Multi (Value, Convert.ToDouble (InputString));
					InputString = Convert.ToString (Value);
					break;
				case "/":
					Button_pressed = false;
					if (InputString == "0")
						InputString = "На ноль делить нельзя!";
					else {
						Value = DependencyService.Get<ICalculatorService> ().Div (Value, Convert.ToDouble (InputString));
						InputString = Convert.ToString (Value);
					}
					break;
				default:
					Button_pressed = false;
					break;
				}
			}

			if (operation == "="){
				if (InputString != "На ноль делить нельзя!")
					InputString = Convert.ToString (Value);
				else {
					Value = 0;
					s_Operation = "";
				}
			}
			else
				s_Operation = operation;	
			
			DisplayText = InputString;
		}

		//CorectInput - следит за корректность введенных данных
		public void CorectInput (string key)
		{
			if (Point_pressed == false && key == "."){				
				this.InputString += key;
				Point_pressed = true;
			} 
			else if (Point_pressed == true && key != ".")
				this.InputString += key;
			else if (key != ".")
				this.InputString += key;

			DisplayText = InputString;
		}

		//Отслеживает изменения в свойствах
		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
		}
	}
}