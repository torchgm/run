// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Run
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSComboBox RunBox { get; set; }

		[Action ("ClickedBrowse:")]
		partial void ClickedBrowse (Foundation.NSObject sender);

		[Action ("ClickedCancel:")]
		partial void ClickedCancel (Foundation.NSObject sender);

		[Action ("ClickedOK:")]
		partial void ClickedOK (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (RunBox != null) {
				RunBox.Dispose ();
				RunBox = null;
			}
		}
	}
}
