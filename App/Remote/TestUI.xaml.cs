using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Remote
{
	public partial class TestUI : ContentPage
	{
		String guidFan, guidLightLeft, guidLightRight;

		public TestUI ()
		{
			SynchronousSocketConnector.InitializeSocket ("raspberrypi", 11337);
			guidFan = SmartManager.Instance.CreateSmartObjcet ("11111", "01", "Fan", SmartObjectType.Fan);
			guidLightLeft = SmartManager.Instance.CreateSmartObjcet ("11111", "03", "Light Left");
			guidLightRight = SmartManager.Instance.CreateSmartObjcet ("11111", "04", "Light right");

			Title = "Remote";

			BindingContext = SmartManager.Instance;
			InitializeComponent ();
		}


		public async void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return; // has been set to null, do not 'process' tapped event

			SmartObject smartObject = ((SmartObject)((ListView)sender).SelectedItem);
			await SmartManager.Instance.ToogleAsync (smartObject.SmartID);

			((ListView)sender).SelectedItem = null; // de-select the row
		}

	}
}

