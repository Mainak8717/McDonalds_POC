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
	[Register ("HomeVC")]
	partial class HomeVC
	{
		[Outlet]
		UIKit.UITableView sharepointURLS_TblView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (sharepointURLS_TblView != null) {
				sharepointURLS_TblView.Dispose ();
				sharepointURLS_TblView = null;
			}
		}
	}
}
