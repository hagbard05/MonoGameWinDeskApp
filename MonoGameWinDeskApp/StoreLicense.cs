using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Services.Store;

namespace MyFirstGame
{
	internal static class StoreLicense
	{
		public static async Task<bool> IsTrialAsync()
		{
			try
			{
				_ = Package.Current.Id;
			}
			catch (InvalidOperationException)
			{
				Trace.TraceInformation("No MSIX package identity is present; starting outside Store trial mode.");
				return false;
			}

			StoreAppLicense license = await StoreContext.GetDefault().GetAppLicenseAsync();
			return license.IsTrial;
		}
	}
}
