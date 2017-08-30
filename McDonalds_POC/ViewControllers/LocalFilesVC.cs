using System;
using System.IO;
using Foundation;
using UIKit;

namespace McDonalds_POC
{
	public partial class LocalFilesVC : UIViewController
	{
		public LocalFilesVC() : base("LocalFilesVC", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.Title = "Local Files";

			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			NSFileManager fileManager = NSFileManager.DefaultManager;
			NSError error;
			//NSDictionary* fileAttributes = [fileManager fileAttributesAtPath: filePath traverseLink:YES];
			string[] listOfFiles = fileManager.GetDirectoryContent(documents, out error);
			localFiles_TblView.TableFooterView = new UIView();
			localFiles_TblView.Source = new localFilesTableSource(listOfFiles, this);
		}
	}
}

