﻿using System;
using System.IO;
using Foundation;

namespace McDonalds_POC
{
	public class DownloadFilesSessionDelegate : NSUrlSessionDownloadDelegate
	{
		public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			string urlString = System.Web.HttpUtility.UrlDecode(downloadTask.CurrentRequest.Url.ToString())  ;
			string fileType = urlString.Substring(urlString.Length - 3);
			int idx = urlString.LastIndexOf('/');
			string fileName = urlString.Substring(idx + 1);

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
		}
		public override void DidWriteData(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
		{
			//base.DidWriteData(session, downloadTask, bytesWritten, totalBytesWritten, totalBytesExpectedToWrite);
		}
	}
}