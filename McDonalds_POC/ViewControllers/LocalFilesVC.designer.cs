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
	[Register ("LocalFilesVC")]
	partial class LocalFilesVC
	{
		[Outlet]
		UIKit.UITableView localFiles_TblView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (localFiles_TblView != null) {
				localFiles_TblView.Dispose ();
				localFiles_TblView = null;
			}
		}
	}
}
