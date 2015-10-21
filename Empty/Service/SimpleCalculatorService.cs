using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Empty;

[assembly: Xamarin.Forms.Dependency (typeof (SimpleCalculatorService))]

namespace Empty
{

	public class SimpleCalculatorService : ICalculatorService
	{
		public double Summ(double x, double y)
		{
			double result = x + y;
			return result;
		}

		public double Diff(double x, double y)
		{			
			double result = x - y;
			return result;
		}

		public double Multi(double x, double y)
		{
			double result = x * y;
			return result;
		}

		public double Div(double x, double y)
		{
			if (y == 0)
				return 0;
			else {
				double result = x / y;
				return result;
			}
		}


	}
}



