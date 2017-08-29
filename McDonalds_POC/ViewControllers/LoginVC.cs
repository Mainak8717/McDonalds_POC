using System;
using System.Drawing;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Auth;

namespace McDonalds_POC
{
	public partial class LoginVC : UIViewController
	{
		private UIView activeTextFieldView;
		private nfloat amountToScroll = 0.0f;
		private nfloat alreadyScrolledAmount = 0.0f;
		private nfloat bottomOfTheActiveTextField = 0.0f;
		private nfloat offsetBetweenKeybordAndTextField = 10.0f;
		private bool isMoveRequired = false;
		private LoadingOverlay loadPop;


		public LoginVC() : base("LoginVC", null)
		{
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.NavigationController.NavigationBar.BarTintColor = UIColor.Red;
			this.Title = "Login";

			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver
								(UIKeyboard.DidShowNotification, KeyBoardDidShow);

			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver
								(UIKeyboard.WillHideNotification, KeyBoardWillHide);

			username_TxtField.Text = "https://spo.mcd.com/sites/seeit_development3/brand/cognizant-test-brand-05242017";
			username_TxtField.ShouldReturn = delegate
			{
				username_TxtField.ResignFirstResponder();
				return true;
			};
			password_TxtField.ShouldReturn = delegate
			{
				password_TxtField.ResignFirstResponder();
				return true;
			};

		}
		/// <summary>
		/// Submits the button action.
		/// </summary>
		partial void Submit_Btn_Action(NSObject sender)
		{
			StartActivityIndicatorWithText("Please Wait...");
			try
			{
				if (!string.IsNullOrWhiteSpace(username_TxtField.Text) && !string.IsNullOrWhiteSpace(password_TxtField.Text))
				{
					//Account account = new Account();
					//account.Properties.Add("Username", username_TxtField.Text);
					//account.Properties.Add("Password", password_TxtField.Text);
					//AccountStore.Create().Save(account, AppDelegate.AppName);

					NSUserDefaults.StandardUserDefaults.SetString(username_TxtField.Text.ToString(), "Username");
					NSUserDefaults.StandardUserDefaults.SetString(password_TxtField.Text.ToString(), "Password");
					UINavigationController navC = new UINavigationController(new HomeVC());

					UIApplication.SharedApplication.KeyWindow.RootViewController = navC;
					UIApplication.SharedApplication.KeyWindow.MakeKeyAndVisible();

				}
			}
			catch (Exception ex)
			{
				var okAlertController = UIAlertController.Create("Error", ex.Message, UIAlertControllerStyle.Alert);
				okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
				PresentViewController(okAlertController, true, null);
				StopActivityIndicator();
			}
			finally
			{
				StopActivityIndicator();
			}
		}
		/// <summary>
		/// Starts the activity indicator with text.
		/// </summary>
		private void StartActivityIndicatorWithText(string message)
		{
			var bounds = UIScreen.MainScreen.Bounds;
			loadPop = new LoadingOverlay(bounds, message);
			View.Add(loadPop);
		}
		private void StopActivityIndicator()
		{
			loadPop.Hide();
		}
		/// <summary>
		/// Keies the board did show.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void KeyBoardDidShow(NSNotification notification)
		{
			// get the keyboard size
			CoreGraphics.CGRect notificationBounds = UIKeyboard.BoundsFromNotification(notification);

			// Find what opened the keyboard
			foreach (UIView view in this.View.Subviews)
			{
				if (view.IsFirstResponder)
					activeTextFieldView = view;
			}

			// Bottom of the controller = initial position + height + offset
			bottomOfTheActiveTextField = (activeTextFieldView.Frame.Y + activeTextFieldView.Frame.Height + offsetBetweenKeybordAndTextField);

			// Calculate how far we need to scroll
			amountToScroll = (notificationBounds.Height - (View.Frame.Size.Height - bottomOfTheActiveTextField));

			// Perform the scrolling
			if (amountToScroll > 0)
			{
				bottomOfTheActiveTextField -= alreadyScrolledAmount;
				amountToScroll = (notificationBounds.Height - (View.Frame.Size.Height - bottomOfTheActiveTextField));
				alreadyScrolledAmount += amountToScroll;
				isMoveRequired = true;
				ScrollTheView(isMoveRequired);
			}
			else
			{
				isMoveRequired = false;
			}

		}
		private void KeyBoardWillHide(NSNotification notification)
		{
			bool wasViewMoved = !isMoveRequired;
			if (isMoveRequired) { ScrollTheView(wasViewMoved); }
		}
		private void ScrollTheView(bool move)
		{

			// scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			CoreGraphics.CGRect frame = View.Frame;

			if (move)
			{
				frame.Y -= amountToScroll;
			}
			else
			{
				frame.Y += alreadyScrolledAmount;
				amountToScroll = 0;
				alreadyScrolledAmount = 0;
			}

			View.Frame = frame;
			UIView.CommitAnimations();
		}
	}
}

