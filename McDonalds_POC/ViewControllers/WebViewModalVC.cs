using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using WebKit;
using Xamarin.Auth;

namespace McDonalds_POC
{
	public partial class WebViewModalVC : UIViewController, IWKUIDelegate, IWKNavigationDelegate, IWKScriptMessageHandler
	{
		WKWebView webView;
		private NSUrl url;
		private LoadingOverlay loadPop;

		const string JavaScriptFunctionForVideo = "function getVideoURL(data){window.webkit.messageHandlers.callBack.postMessage(data);}";
		const string JavaScriptFunctionForImage = "function getImageURL(data){window.webkit.messageHandlers.callBack.postMessage(data);}";
		const string JavaScriptFunctionForPDF = "function getPDFURL(data){window.webkit.messageHandlers.callBack.postMessage(data);}";
		const string JavaScriptFunctionForDownloadAll = "function getAllURL(data){window.webkit.messageHandlers.callBack.postMessage(data);}";
		const string JavaScriptFunction = "function getURL(data){window.webkit.messageHandlers.callBack.postMessage(data);}";


		public WebViewModalVC(NSUrl url) : base("WebViewModalVC", null)
		{
			this.url = url;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var userController = new WKUserContentController();

			var scriptImage = new WKUserScript(new NSString(JavaScriptFunctionForImage), WKUserScriptInjectionTime.AtDocumentEnd, false);
			var scriptVideo = new WKUserScript(new NSString(JavaScriptFunctionForVideo), WKUserScriptInjectionTime.AtDocumentEnd, false);
			var scriptPDF = new WKUserScript(new NSString(JavaScriptFunctionForPDF), WKUserScriptInjectionTime.AtDocumentEnd, false);
			var scriptAllFiles = new WKUserScript(new NSString(JavaScriptFunctionForDownloadAll), WKUserScriptInjectionTime.AtDocumentEnd, false);
			var scriptGetURL = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);

			userController.AddUserScript(scriptGetURL);
			userController.AddScriptMessageHandler(this, "callBack");

			webView = new WKWebView(new CoreGraphics.CGRect(0, 0, 375, 667), new WKWebViewConfiguration()
			{
				UserContentController = userController
			});
			webView.UIDelegate = this;
			webView.NavigationDelegate = this;
			View.AddSubview(webView);
			webView.LoadRequest(new NSUrlRequest(url));
			StartActivityIndicatorWithText("Loading...",new CoreGraphics.CGRect(0, 0, 375, 667));

		}
		/// <summary>
		/// WKWebview Delegate Methods
		/// </summary>
		[Export("webView:didReceiveAuthenticationChallenge:completionHandler:")]
		public void DidReceiveAuthenticationChallenge(WKWebView webView, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
		{
			NSUrlCredential userCredential = NSUrlCredential.FromUserPasswordPersistance(UserName, Password, NSUrlCredentialPersistence.None);
			completionHandler(NSUrlSessionAuthChallengeDisposition.UseCredential, userCredential);
		}

		[Export("webView:didFinishNavigation:")]
		public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
		{
			StopActivityIndicator();

		}

		public void DownloadFiles(NSUrl url)
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			var config = NSUrlSessionConfiguration.DefaultSessionConfiguration;
			NSUrlSession session = NSUrlSession.FromConfiguration(config, new DownloadFilesSessionDelegate(), new NSOperationQueue());
			var downloadTask = session.CreateDownloadTask(NSUrlRequest.FromUrl(url));

			downloadTask.Resume();
		}




		/// <summary>
		/// Activity Indicator Methods
		/// </summary>
		private void StartActivityIndicatorWithText(string message, CoreGraphics.CGRect frame)
		{
			loadPop = new LoadingOverlay(frame, message);
			View.Add(loadPop);
			View.BringSubviewToFront(loadPop);
		}
		private void StopActivityIndicator()
		{
			loadPop.Hide();
		}

		public async void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
		{
			StartActivityIndicatorWithText("Download in progress..", new CoreGraphics.CGRect(82.5, 225, 200, 200));
			await Task.Delay(2000);
			StopActivityIndicator();
			var msg = message.Body.ToString();
			string[] urlArray = msg.Split(',');
			if (AppDelegate.filesDownloading != null){
				AppDelegate.filesDownloading.RemoveAllObjects();
				AppDelegate.filesDownloaded.RemoveAllObjects();
			}
			AppDelegate.filesDownloading = new NSMutableArray();
			AppDelegate.filesDownloaded = new NSMutableArray();

			foreach (string urlfromArray in urlArray)
			{
				string urlString = System.Web.HttpUtility.UrlDecode(urlfromArray.ToString());
				string fileType = urlString.Substring(urlString.Length - 3);
				int idx = urlString.LastIndexOf('/');
				var fileName = urlString.Substring(idx + 1);
				AppDelegate.filesDownloading.Add(NSObject.FromObject(fileName));
				NSUrl downloadURL = NSUrl.FromString(urlfromArray);
				DownloadFiles(downloadURL);
			}
		}

		/// <summary>
		/// Get UserName and Password from KeyChain
		/// </summary>
		public string UserName
		{
			get
			{
				var userName = NSUserDefaults.StandardUserDefaults.StringForKey("Username");
				return (userName != null) ? userName : null;
			}
		}
		public string Password
		{
			get
			{
				var password = NSUserDefaults.StandardUserDefaults.StringForKey("Password");
				return (password != null) ? password : null;
			}
		}
	}
}

