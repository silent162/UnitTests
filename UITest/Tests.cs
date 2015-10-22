using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace UITest
{
	[TestFixture]
	public class Tests
	{
		AndroidApp app;
		private string path = "C:/Users/Worker5/Desktop/Empty1/Empty1/Empty/Droid/bin/Debug/com.companyname.empty.apk";

		[SetUp]
		public void BeforeEachTest ()
		{
			app = ConfigureApp.Android.ApkFile (path).StartApp ();
	    }

		[Test]
		public void AppLaunches ()
		{
			app.Screenshot ("First screen.");
		}
	}
}

