using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UIKit;

namespace McDonalds_POC
{
	public class Application
	{



		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;

			//ServicePointManager
			//         .ServerCertificateValidationCallback +=
			//         (sender, cert, chain, sslPolicyErrors) => true;
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
		private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

	}
}
