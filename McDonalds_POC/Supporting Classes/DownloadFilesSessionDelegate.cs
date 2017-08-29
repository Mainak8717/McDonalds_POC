using System;
using System.IO;
using System.Linq;
using Foundation;
using Xamarin.Auth;
using UIKit;

namespace McDonalds_POC
{
	public class DownloadFilesSessionDelegate : NSUrlSessionDownloadDelegate
	{
		public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			string urlString = System.Web.HttpUtility.UrlDecode(downloadTask.CurrentRequest.Url.ToString());
			string fileType = urlString.Substring(urlString.Length - 3);
			int idx = urlString.LastIndexOf('/');
			string fileName = urlString.Substring(idx + 1);
			AppDelegate.filesDownloaded.Add(NSObject.FromObject(fileName));
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var destinationPath = Path.Combine(documents, fileName);

			if (File.Exists(location.Path))
			{
				NSFileManager fileManager = NSFileManager.DefaultManager;
				NSError error;

				// Remove the same name file in destination path. 
				fileManager.Remove(destinationPath, out error);

				// Copy the file from the tmp directory to your destination path. The tmp file will be removed when this delegate finishes.
				bool success = fileManager.Copy(location.Path, destinationPath, out error);

				if (!success)
				{
					Console.WriteLine("Error during the copy: {0}", error.LocalizedDescription);
				}
			}
			int count = 0;
			for (nuint i = 0; i < AppDelegate.filesDownloading.Count; i++)
			{
				for (nuint j = 0; j < AppDelegate.filesDownloaded.Count; j++)
				{
					if (AppDelegate.filesDownloading.GetItem<NSObject>(i).Equals(AppDelegate.filesDownloading.GetItem<NSObject>(j)))
					{
						count = count + 1;
					}
				}

			}
			if (count == (int)AppDelegate.filesDownloading.Count)
			{
				InvokeOnMainThread(delegate
				{
					var notification = new UILocalNotification();
					notification.FireDate = NSDate.FromTimeIntervalSinceNow(1);
					notification.AlertAction = "McDonalds:File Download Complete";
					notification.AlertBody = "McDonalds:File Download Complete";
					notification.ApplicationIconBadgeNumber = 1;
					notification.SoundName = UILocalNotification.DefaultSoundName;
					UIApplication.SharedApplication.ScheduleLocalNotification(notification);
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				});
  		  }
		}
		public override void DidReceiveChallenge(NSUrlSession session, NSUrlSessionTask task, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
		{
			//base.DidReceiveChallenge(session, task, challenge, completionHandler);
			NSUrlCredential userCredential = NSUrlCredential.FromUserPasswordPersistance(UserName, Password, NSUrlCredentialPersistence.None);
			//challenge.Sender.UseCredential(userCredential, challenge);
			completionHandler(NSUrlSessionAuthChallengeDisposition.UseCredential, userCredential);
		}
		[Export("URLSession:downloadTask:didWriteData:totalBytesWritten:totalBytesExpectedToWrite:")]
		public override void DidWriteData(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
		{
			float progress = (totalBytesWritten / (float)totalBytesExpectedToWrite) * 100;
			Console.WriteLine(string.Format("progress: {0}%", progress));
		}

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
