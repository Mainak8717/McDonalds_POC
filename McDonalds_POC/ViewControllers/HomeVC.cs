using System;
using Foundation;
using UIKit;

namespace McDonalds_POC
{
	public partial class HomeVC : UIViewController
	{
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

            this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.Title = "Home";

			var dict = new NSDictionary("See it", "https://spo.mcd.com/sites/seeit_development3/brand/cognizant-test-brand-05242017?FromMobile=Y","Local Files","");
			sharepointURLS_TblView.TableFooterView = new UIView();
			sharepointURLS_TblView.Source = new TableSource(dict,this);
		}
	}
}

