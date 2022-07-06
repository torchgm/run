using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using AppKit;
using Foundation;

namespace Run
{
	public partial class ViewController : NSViewController
	{
		List<string> recents = new List<string>();
		string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		public ViewController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			// Check if file already exists. If yes, delete it.

			if (!Directory.Exists($"{userFolder}/Library/Application Support/is.torch.Run"))
			{
				Directory.CreateDirectory($"{userFolder}/Library/Application Support/is.torch.Run");
			}
            if (!File.Exists($"{userFolder}/Library/Application Support/is.torch.Run/recents"))
            {
				File.CreateText($"{userFolder}/Library/Application Support/is.torch.Run/recents").Close();
			}

			RunBox.UsesDataSource = true;
			
			recents = new List<string>(File.ReadAllLines($"{userFolder}/Library/Application Support/is.torch.Run/recents"));


			RunBox.DataSource = new BloomTypesDataSource(recents);



		}

		public override void ViewDidAppear()
		{
			base.ViewDidAppear();
			NSApplication app = NSApplication.SharedApplication;
			app.ActivateIgnoringOtherApps(true);
			View.Window.MakeKeyAndOrderFront(this);
		}

		public override NSObject RepresentedObject {
			get {
				return base.RepresentedObject;
			}
			set {
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}

		}

        partial void ClickedOK(NSObject sender)
        {
            if (RunBox.StringValue != "")
            {
                if (!recents.Contains(RunBox.StringValue))
                {
					recents.Insert(0, RunBox.StringValue);
					recents = recents.Take(20).ToList();
					File.WriteAllLines($"{userFolder}/Library/Application Support/is.torch.Run/recents", recents);
					RunBox.DataSource = new BloomTypesDataSource(recents);
				}

			}

            try
            {
                if (RunBox.StringValue.Contains('/'))
                {
					Process.Start(RunBox.StringValue);
					View.Window.Close();
				}
                else
                {
					throw new Exception();
                }
			}
			catch (Exception)
            {
                if (RunBox.StringValue == "")
                {
					var alert = new NSAlert()
					{
						AlertStyle = NSAlertStyle.Critical,
						InformativeText = $"macOS cannot find something if you don't tell it what you want. Actually enter something into the box, and then try again.",
						MessageText = "",
					};
					alert.RunModal();
                }
                else
                {
                    try
                    {
						Process.Start($"/System/Applications/{RunBox.StringValue}");
						View.Window.Close();
					}
                    catch (Exception)
                    {
                        try
                        {
							Process.Start($"/System/Applications/Utilities/{RunBox.StringValue}");
							View.Window.Close();
						}
                        catch (Exception)
                        {
							try
							{
								Process.Start($"/Applications/{RunBox.StringValue}");
								View.Window.Close();
							}
							catch (Exception)
							{
								try
								{
									List<string> cmd = RunBox.StringValue.Split(' ').ToList();
                                    if ((!cmd[0].Contains('/') && (File.Exists($"/usr/bin/{cmd[0]}") || File.Exists($"/bin/{cmd[0]}"))) || (cmd[0].Contains('/') && !cmd[0].Split('/').Last().Contains('.')))
                                    {
										CreateRunFile(RunBox.StringValue);
										Process.Start("/tmp/run.command");
										View.Window.Close();
									}
                                    else
                                    {
										throw new Exception();
                                    }

								}
								catch (Exception)
								{
									var alert = new NSAlert()
									{
										AlertStyle = NSAlertStyle.Critical,
										InformativeText = $"macOS cannot find '{RunBox.StringValue}'. Make sure you typed the name correctly, and then try again.",
										MessageText = "",
									};
									alert.RunModal();
								}
							}
						}
                    }
				}
			}
		}

		partial void ClickedCancel(NSObject sender)
		{
			View.Window.Close();
		}

		partial void ClickedBrowse(NSObject sender)
        {
			var panel = NSOpenPanel.OpenPanel;
			panel.FloatingPanel = true;
			panel.CanChooseDirectories = true;
			panel.CanChooseFiles = true;
			panel.AllowsMultipleSelection = false;
			int i = (int)panel.RunModal();
			if (i == 1 && panel.Urls != null)
			{
				foreach (NSUrl url in panel.Urls)
				{
					RunBox.StringValue = url.ToString();
				}
			}

		}

		public static void CreateRunFile(string command)
        {
			string fileName = "/tmp/run.command";

			try
			{
				// Check if file already exists. If yes, delete it.     
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}

				// Create a new file     
				using (StreamWriter sw = File.CreateText(fileName))
				{
					sw.Write(command);
				}

				// Write file contents on console.     
				using (StreamReader sr = File.OpenText(fileName))
				{
					string s = "";
					while ((s = sr.ReadLine()) != null)
					{
						Console.WriteLine(s);
					}
				}
				Process.Start("chmod", $"a+rwx {fileName}");
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex.ToString());
			}
		}

		public class BloomTypesDataSource : NSComboBoxDataSource
		{
			readonly List<string> source;

			public BloomTypesDataSource(List<string> source)
			{
				this.source = source;
			}

			public override string CompletedString(NSComboBox comboBox, string uncompletedString)
			{
				return source.Find(n => n.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase));
			}

			public override nint IndexOfItem(NSComboBox comboBox, string value)
			{
				return source.FindIndex(n => n.Equals(value, StringComparison.InvariantCultureIgnoreCase));
			}

			public override nint ItemCount(NSComboBox comboBox)
			{
				return source.Count;
			}

			public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
			{
				return FromObject(source[(int)index]);
			}
		}
	}
	
}
