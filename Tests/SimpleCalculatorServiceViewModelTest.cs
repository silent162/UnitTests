﻿using NUnit.Framework;
using Empty;

namespace CalculatorMvvM.Tests
{
	[TestFixture ()]
	public class SimpleCalculatorServiceViewModelTest
	{		
		[Test()]
		public void AllCleaningTest()
		{
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			sp.DisplayText = "77";
			sp.InputString = "0177";
			sp.Value = 1000;
			SimpleCalculatorServiceViewModel.s_Operation = "+-*";
			sp.Point_pressed = true;
			sp.Operation_pressed = true;
			sp.Button_pressed = true;

			sp.AllCleaning ();

			Assert.AreEqual ("0", sp.DisplayText);
			Assert.AreEqual (string.Empty, sp.InputString);
			Assert.AreEqual (0, sp.Value);
			Assert.AreEqual (string.Empty, SimpleCalculatorServiceViewModel.s_Operation);
			Assert.IsFalse (sp.Point_pressed);
			Assert.IsFalse (sp.Operation_pressed);
			Assert.IsFalse (sp.Button_pressed);	
		}

		[Test()]
		public void LastValueCleaningTest ()
		{
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			sp.DisplayText = "77";
			sp.InputString = "77";
			sp.Point_pressed = true;

			sp.LastValueCleaning ();

			Assert.AreEqual ("0", sp.DisplayText);
			Assert.AreEqual (string.Empty, sp.InputString);
			Assert.IsFalse (sp.Point_pressed);
		}

		[Test()]
		public void AddCharTest ()
		{
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			sp.AllCleaning ();
			string str = "6";
			sp.AddChar (str);
			str = ".";
			sp.AddChar (str);
			str = ".";
			sp.AddChar (str);
			str = "8";
			sp.AddChar (str);

			Assert.AreEqual ("6.8", sp.DisplayText);
		}

		[Test()]
		public void OperationAddTest ()
		{
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			string str = "+";
			sp.AllCleaning ();
			sp.AddChar ("9");

			sp.OperationAdd (str);

			Assert.AreEqual ("+", SimpleCalculatorServiceViewModel.s_Operation);
		}

		[Test()]
		public void ArithmeticOperationsTest ()
		{
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			sp.AllCleaning ();
			sp.Value = 10;
			string str = "+";
			sp.ArithmeticOperations (str);
			sp.Button_pressed = true;
			sp.InputString = "5";
			sp.ArithmeticOperations ("+");
			sp.Button_pressed = true;
			sp.InputString = "5";
			sp.ArithmeticOperations ("=");	

			Assert.AreEqual ("20", sp.DisplayText);
		}

		[Test()]
		public void CorectInputTest ()
		{
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			sp.AllCleaning ();
			string str = "8";
			sp.CorectInput (str);
			str = ".";
			sp.CorectInput (str);
			str = ".";
			sp.CorectInput (str);
			str = "5";
			sp.CorectInput (str);

			Assert.AreEqual ("8.5", sp.DisplayText);
		}

		[Test()]
		public void OnPropertyChangedTest()
		{			
			SimpleCalculatorServiceViewModel sp = new SimpleCalculatorServiceViewModel();
			sp.DisplayText = "5";
			string s = "";
			sp.PropertyChanged += delegate {
				s = "00";
			};

			sp.OnPropertyChanged ("DisplayText");
			sp.DisplayText += s;

			Assert.AreEqual ("500", sp.DisplayText);
				
		}
	}
}

