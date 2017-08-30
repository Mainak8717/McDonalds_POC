using System;
using Foundation;
using UIKit;

namespace McDonalds_POC
{
	public partial class HomeVC : UIViewController
	{
		int fileDownloadedCount;
		NSDictionary myDictionary;
		public HomeVC() : base("HomeVC", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            this.NavigationItem.SetRightBarButtonItem(

	new UIBarButtonItem(UIImage.FromFile("exitImg.png")
    , UIBarButtonItemStyle.Plain
    , (sender,args) => {

				NSUserDefaults.StandardUserDefaults.RemoveObject("Username");
				NSUserDefaults.StandardUserDefaults.RemoveObject("Password");

				UINavigationController navC = new UINavigationController(new LoginVC());
				UIApplication.SharedApplication.KeyWindow.RootViewController = navC;
				UIApplication.SharedApplication.KeyWindow.MakeKeyAndVisible();
    })
, true);
			NSNotificationCenter.DefaultCenter.AddObserver ((NSString)"DownloadCount", getDownloadCount);
            this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.Title = "Home";

			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			NSFileManager fileManager = NSFileManager.DefaultManager;
			NSError error;
			string[] countArray = fileManager.GetDirectoryContent(documents, out error);

			myDictionary= new NSDictionary("Seeit", "https://spo.mcd.com/sites/seeit_development3/brand/cognizant-test-brand-05242017?FromMobile=Y","Local Files",countArray.Length);
			sharepointURLS_TblView.TableFooterView = new UIView();
			sharepointURLS_TblView.Source = new TableSource(myDictionary,this);
		}

		void getDownloadCount(NSNotification obj)
		{
            InvokeOnMainThread(delegate
				{
					var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					NSFileManager fileManager = NSFileManager.DefaultManager;
					NSError error;
					string[] countArray = fileManager.GetDirectoryContent(documents, out error);
					myDictionary= new NSDictionary("Seeit", "https://spo.mcd.com/sites/seeit_development3/brand/cognizant-test-brand-05242017?FromMobile=Y","Local Files", countArray.Length);
					sharepointURLS_TblView.Source = new TableSource(myDictionary,this);
					sharepointURLS_TblView.ReloadData();
				});
		}
	}
}

