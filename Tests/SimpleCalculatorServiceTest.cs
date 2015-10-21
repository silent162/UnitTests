using NUnit.Framework;
using Empty;

namespace CalculatorMvvM.Tests
{
	[TestFixture ()]
	public class SimpleCalculatorServiceTests
	{
		SimpleCalculatorService sp;

		[Test ()]
		public void Summ()
		{
			sp = new SimpleCalculatorService ();
			double result = sp.Summ(4,4);
			Assert.AreEqual (8, result);
		}

		[Test ()]
		public void Diff()
		{			
			sp = new SimpleCalculatorService ();
			double result = sp.Diff(9,4);
			Assert.AreEqual (5, result);
		}

		[Test ()]
		public void Multi()
		{			
			sp = new SimpleCalculatorService ();
			double result = sp.Multi(2,5);
			Assert.AreEqual (10, result);
		}

		[Test ()]
		public void Div()
		{			
			sp = new SimpleCalculatorService ();
			double result = sp.Div(9,3);
			Assert.AreEqual (3, result);

			result = sp.Div(9,0);
			Assert.AreEqual (0, result);
		}
	}
}

