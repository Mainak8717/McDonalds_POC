using System;
using UIKit;
using Foundation;
using UIKit;

namespace McDonalds_POC
{
	public class localFilesTableSource : UITableViewSource
	{
		protected string[] tableItems;
		string CellIdentifier = "TableCell";
		LocalFilesVC owner;

		public localFilesTableSource(string[] items, LocalFilesVC owner)
		{
			tableItems = items;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
			}
			cell.TextLabel.Text = tableItems[indexPath.Row];

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Length;
		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//base.RowSelected(tableView, indexPath);
			//owner.NavigationController.PushViewController(new WebViewModalVC(new NSUrl(value.ToString())), true);
			//owner.PresentModalViewController(new WebViewModalVC(new NSUrl(value.ToString())),true);
			tableView.DeselectRow(indexPath, true);
		}
	}
}
