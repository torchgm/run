using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using AppKit;
using Foundation;

namespace Run
{
    [Register ("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate
	{
		public AppDelegate ()
		{
		}

		public static byte[] streamToByteArray(Stream input)
		{
			MemoryStream ms = new MemoryStream();
			input.CopyTo(ms);
			return ms.ToArray();
		}

		public override void DidFinishLaunching (NSNotification notification)
		{
			string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			if (!File.Exists($"{userFolder}/Library/LaunchAgents/run.plist"))
			{
			var plist = Assembly.GetExecutingAssembly().GetManifestResourceStream("startupPlist");

			File.WriteAllBytes($"{userFolder}/Library/LaunchAgents/run.plist", streamToByteArray(plist));
			}


			// Create a Status Bar Menu
			NSStatusBar statusBar = NSStatusBar.SystemStatusBar;

			var item = statusBar.CreateStatusItem(NSStatusItemLength.Variable);
			item.Title = ">_";
			item.HighlightMode = true;
			item.Menu = new NSMenu(">_");

			var showRun = new NSMenuItem("Run");
			showRun.Activated += (sender, e) => {
                try
                {
					NSApplication.SharedApplication.KeyWindow.Close();
				}
				catch (NullReferenceException)
                {

                }


				// Get new window
				var storyboard = NSStoryboard.FromName("Main", null);
				var controller = storyboard.InstantiateControllerWithIdentifier("RunWindowController") as NSWindowController;

				// Display
				controller.ShowWindow(this);

			};
			item.Menu.AddItem(showRun);

			var quitRun = new NSMenuItem("Quit");
			quitRun.Activated += (sender, e) => {
				Process.GetCurrentProcess().CloseMainWindow();
			};
			item.Menu.AddItem(quitRun);
		}

		public override void WillTerminate (NSNotification notification)
		{
			// Insert code here to tear down your application
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
		{
			return false;
		}
	}
}

