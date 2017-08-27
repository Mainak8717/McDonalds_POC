using System;
using UIKit;
using Foundation;
using UIKit;
using CoreGraphics;

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
			this.owner = owner;
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
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			NSFileManager fileManager = NSFileManager.DefaultManager;
			NSError error;
			string[] listOfFiles = fileManager.GetDirectoryContent(documents, out error);
			string jpgFilename = System.IO.Path.Combine(documents, listOfFiles[0]);
					

			UIImageView imageView = new UIImageView(new CGRect(0, 0, 150, 150));
			imageView.Image = UIImage.FromFile(jpgFilename);
			imageView.UserInteractionEnabled = true;
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			// Create a view controller to act as the popover
			UIViewController popover = new UIViewController();
			popover.View = imageView;
			popover.ModalPresentationStyle = UIModalPresentationStyle.Popover;

			// Grab Image
			var image = UIImage.FromFile("closeImg.png");

			// Add a close button
			var closeButton = new ImageButton(new CGRect(popover.View.Frame.Size.Width, 20, image.Size.Width, image.Size.Height));
			closeButton.UserInteractionEnabled = true;
			closeButton.Image = image;
			imageView.AddSubview(closeButton);

			// Wireup the close button
			closeButton.Touched += (button) =>
			{
				popover.DismissViewController(true, null);
			};

			// Present the popover
			owner.PresentViewController(popover, true, null);
			tableView.DeselectRow(indexPath, true);
		}
	}

	public class ImageButton : UIImageView
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="UIKitEnhancements.ImageButton"/> class.
		/// </summary>
		public ImageButton() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UIKitEnhancements.ImageButton"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ImageButton(NSCoder coder) : base(coder)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UIKitEnhancements.ImageButton"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ImageButton(NSObjectFlag flag) : base(flag)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UIKitEnhancements.ImageButton"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ImageButton(CGRect bounds) : base(bounds)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UIKitEnhancements.ImageButton"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ImageButton(IntPtr ptr) : base(ptr)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UIKitEnhancements.ImageButton"/> class.
		/// </summary>
		/// <param name="image">Image.</param>
		public ImageButton(UIImage image) : base(image)
		{
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Toucheses the began.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			//Inform caller of event
			RaiseTouched();

			//Pass call to base object
			base.TouchesBegan(touches, evt);
		}

		#endregion

		#region Events
		/// <summary>
		/// Button touched delegate.
		/// </summary>
		public delegate void ButtonTouchedDelegate(ImageButton button);
		public event ButtonTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched()
		{
			if (this.Touched != null)
				this.Touched(this);
		}
		#endregion
	}

}
