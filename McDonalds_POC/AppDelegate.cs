using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Auth;

namespace McDonalds_POC
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}
		public static string AppName { get { return "McDonalds_POC"; } }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			UINavigationController navC;
			//var account = AccountStore.Create().FindAccountsForService(AppDelegate.AppName).FirstOrDefault();
			var userName = NSUserDefaults.StandardUserDefaults.StringForKey("Username");

			if (string.IsNullOrEmpty(userName))
				navC = new UINavigationController(new LoginVC());
			else
				navC = new UINavigationController(new HomeVC());
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.Yellow });
			Window = new UIWindow(UIScreen.MainScreen.Bounds);
			Window.RootViewController = navC;
			Window.MakeKeyAndVisible();

			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}

