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

            this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.Title = "Home";

			var dict = new NSDictionary("See it", "https://spo.mcd.com/sites/seeit_development3/brand/cognizant-test-brand-05242017","Local Files","");
			sharepointURLS_TblView.TableFooterView = new UIView();
			sharepointURLS_TblView.Source = new TableSource(dict,this);
		}

	}
}

