using System;
using System.Diagnostics.Contracts;
using Foundation;
using UIKit;

namespace McDonalds_POC
{
	public class TableSource : UITableViewSource
	{
		NSDictionary TableItems;
		NSObject[] keyArray;
		string CellIdentifier = "TableCell";
		HomeVC owner;
		string[] countArray;
		public TableSource(NSDictionary items, HomeVC owner)
		{
			TableItems = items;
			keyArray = TableItems.Keys;
			this.owner = owner;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			var key = keyArray[indexPath.Row];
			var value = TableItems.ValueForKey((Foundation.NSString)key);
			if (cell == null){ 
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
			}
			cell.TextLabel.Text = key.ToString();
			if(cell.TextLabel.Text.Equals("Local Files"))
				cell.DetailTextLabel.Text = "Total Files:"+value.ToString();
			else
				cell.DetailTextLabel.Text = value.ToString();


			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return 2;
		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//base.RowSelected(tableView, indexPath);
			var key = keyArray[indexPath.Row];
			var value = TableItems.ValueForKey((Foundation.NSString)key);
			if (keyArray[indexPath.Row].ToString().Equals("Local Files"))
				owner.NavigationController.PushViewController(new LocalFilesVC(), true);
			else
				owner.NavigationController.PushViewController(new WebViewModalVC(new NSUrl(value.ToString())), true);
			//owner.PresentModalViewController(new WebViewModalVC(new NSUrl(value.ToString())),true);
			tableView.DeselectRow (indexPath, true);

		}
	}
}
