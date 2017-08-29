// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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