using System;
using System.Linq;
using Foundation;
using UIKit;
using WebKit;
using Xamarin.Auth;

namespace McDonalds_POC
{
	public partial class WebViewModalVC : UIViewController, IWKUIDelegate, IWKNavigationDelegate
	{
		WKWebView webView;
		private NSUrl url;
		private LoadingOverlay loadPop;


		public WebViewModalVC(NSUrl url) : base("WebViewModalVC", null)
		{
			this.url = url;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
			webView.UIDelegate = this;
			webView.NavigationDelegate = this;

			View.AddSubview(webView);
			webView.LoadRequest(new NSUrlRequest(url));
		}

		/// <summary>
		/// WKWebview Delegate Methods
		/// </summary>
		[Export("webView:didReceiveAuthenticationChallenge:completionHandler:")]
		public void DidReceiveAuthenticationChallenge(WKWebView webView, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
		{
			NSUrlCredential userCredential = NSUrlCredential.FromUserPasswordPersistance(UserName, Password, NSUrlCredentialPersistence.None);
			//challenge.Sender.UseCredential(userCredential, challenge);
			completionHandler(NSUrlSessionAuthChallengeDisposition.UseCredential, userCredential);
		}

		[Export("webView:didFinishNavigation:")]
		public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
		{
			StopActivityIndicator();
			DownloadFiles();
		}


		[Export("webView:didStartProvisionalNavigation:")]
		public void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
		{
			StartActivityIndicatorWithText("Loading...");
		}

		/// <summary>
		/// Java Script Methods
		/// </summary>

		[Foundation.Export("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
		public void RunJavaScriptAlertPanel(WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
		{

		}

		// Called when a Javascript confirm() alert is called in the WKWebView
		[Export("webView:runJavaScriptConfirmPanelWithMessage:initiatedByFrame:completionHandler:")]
		public void RunJavaScriptConfirmPanel(WKWebView webView, string message, WKFrameInfo frame, Action<bool> completionHandler)
		{

		}

		// Called when a Javascript prompt() alert is called in the WKWebView
		[Foundation.Export("webView:runJavaScriptTextInputPanelWithPrompt:defaultText:initiatedByFrame:completionHandler:")]
		public void RunJavaScriptTextInputPanel(WebKit.WKWebView webView, string prompt, string defaultText, WebKit.WKFrameInfo frame, System.Action<string> completionHandler)
		{

		}

		public void DownloadFiles()
		{
//			NSUrl urls = NSUrl.FromString("https://spo.mcd.com/sites/seeit_development3/brand/PublishingImages/burgers%20test.jpg");
			NSUrl urls = NSUrl.FromString("https://spo.mcd.com/sites/seeit_development3/brand/PublishingImages/test1.mp4");
			// Configure your download session.
			var config = NSUrlSessionConfiguration.DefaultSessionConfiguration;
			NSUrlSession session = NSUrlSession.FromConfiguration(config, new DownloadFilesSessionDelegate(), new NSOperationQueue());
			var downloadTask = session.CreateDownloadTask(NSUrlRequest.FromUrl(urls));

			// Start the session.
			downloadTask.Resume();
		}




		/// <summary>
		/// Activity Indicator Methods
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
		/// Get UserName and Password from KeyChain
		/// </summary>
		public string UserName
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(AppDelegate.AppName).FirstOrDefault();
				return (account != null) ? account.Properties["Username"] : null;
			}
		}
		public string Password
		{
			get
			{
				var account = AccountStore.Create().FindAccountsForService(AppDelegate.AppName).FirstOrDefault();
				return (account != null) ? account.Properties["Password"] : null;
			}
		}
	}
}

