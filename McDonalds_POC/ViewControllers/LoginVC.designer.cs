// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace McDonalds_POC
{
	[Register ("LoginVC")]
	partial class LoginVC
	{
		[Outlet]
		UIKit.UILabel password_Lbl { get; set; }

		[Outlet]
		UIKit.UITextField password_TxtField { get; set; }

		[Outlet]
		UIKit.UIButton submit_Btn { get; set; }

		[Outlet]
		UIKit.UILabel username_Lbl { get; set; }

		[Outlet]
		UIKit.UITextField username_TxtField { get; set; }

		[Action ("Submit_Btn_Action:")]
		partial void Submit_Btn_Action (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (username_Lbl != null) {
				username_Lbl.Dispose ();
				username_Lbl = null;
			}

			if (password_Lbl != null) {
				password_Lbl.Dispose ();
				password_Lbl = null;
			}

			if (username_TxtField != null) {
				username_TxtField.Dispose ();
				username_TxtField = null;
			}

			if (password_TxtField != null) {
				password_TxtField.Dispose ();
				password_TxtField = null;
			}

			if (submit_Btn != null) {
				submit_Btn.Dispose ();
				submit_Btn = null;
			}
		}
	}
}
