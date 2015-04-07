//
// ToolStripDropDownTests.cs
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2007 Jonathan Pobst
//
// Authors:
//	Jonathan Pobst (monkey@jpobst.com)
//
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using System.Windows.Forms;

namespace MonoTests.System.Windows.Forms
{
	[TestFixture]
	public class ToolStripDropDownTests : TestHelper
	{
		[Test]
		public void Constructor ()
		{
			ToolStripDropDown tsdd = new ToolStripDropDown ();

			Assert.AreEqual (false, tsdd.AllowTransparency, "A1");
			Assert.AreEqual (true, tsdd.AutoClose, "A2");
			Assert.AreEqual (false, tsdd.CanOverflow, "A3");
			Assert.AreEqual (ToolStripDropDownDirection.Right, tsdd.DefaultDropDownDirection, "A4");
			Assert.AreEqual (true, tsdd.DropShadowEnabled, "A5");
			Assert.AreEqual (false, tsdd.IsAutoGenerated, "A6");
			Assert.AreEqual (1, tsdd.Opacity, "A7");
			Assert.AreEqual (Orientation.Horizontal, tsdd.Orientation, "A7-2");
			Assert.AreEqual (null, tsdd.OwnerItem, "A8");
			Assert.AreEqual (null, tsdd.Region, "A9");
			Assert.AreEqual (RightToLeft.No, tsdd.RightToLeft, "A10");
			Assert.AreEqual (ToolStripTextDirection.Horizontal, tsdd.TextDirection, "A11");
			Assert.AreEqual (true, tsdd.TopLevel, "A12");
			Assert.AreEqual (false, tsdd.Visible, "A13");
			
			Assert.AreEqual ("System.Windows.Forms.ToolStripDropDown+ToolStripDropDownAccessibleObject", tsdd.AccessibilityObject.GetType ().ToString (), "A14");
		}

		[Test]
		public void Layout ()
		{
			ToolStripDropDown drop_down = new ToolStripDropDown ();
			drop_down.Items.Add (new ToolStripVariableSizeItem ());
			drop_down.PerformLayout ();

			// We want to be sure the DropDown is using the size provided
			// by GetPreferredSize, not DefaultSize, and since the extra padding/margin
			// can change by some few pixels, we do a light check
			Assert.AreEqual (true, drop_down.Size.Width >= 100, "A1");
			Assert.AreEqual (true, drop_down.Size.Height >= 100, "A2");
		}

		private class ToolStripVariableSizeItem : ToolStripItem {

			public override Size GetPreferredSize (Size constrainingSize) 
			{
				return new Size (100, 100);
			}

			protected override Size DefaultSize {
				get {
					return new Size (33, 33);
				}
			}
		}

		[Test]
		public void ProtectedProperties ()
		{
			ExposeProtectedProperties epp = new ExposeProtectedProperties ();

			Assert.AreEqual (WindowStyles.WS_TILED | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_POPUP, (WindowStyles)epp.CreateParams.Style, "C1");
			// This makes no sense
			// Assert.AreEqual (WindowExStyles.WS_EX_CONTROLPARENT, (WindowExStyles)epp.CreateParams.ExStyle, "C2");
			Assert.AreEqual (DockStyle.None, epp.DefaultDock, "C3");
			Assert.AreEqual (new Padding (1, 2, 1, 2), epp.DefaultPadding, "C4");
			Assert.AreEqual (true, epp.DefaultShowItemToolTips, "C5");
			// Dependent on monitor resolution
			// Assert.AreEqual (new Size (1678, 1016), epp.MaxItemSize, "C6");
			Assert.AreEqual (true, epp.TopMost, "C7");
		}

		[Test]
		public void MethodCreateLayoutSettings ()
		{
			ExposeProtectedProperties ts = new ExposeProtectedProperties ();

			Assert.AreEqual ("System.Windows.Forms.FlowLayoutSettings", ts.PublicCreateLayoutSettings (ToolStripLayoutStyle.Flow).ToString (), "A1");
			Assert.AreEqual (null, ts.PublicCreateLayoutSettings (ToolStripLayoutStyle.HorizontalStackWithOverflow), "A2");
			Assert.AreEqual (null, ts.PublicCreateLayoutSettings (ToolStripLayoutStyle.StackWithOverflow), "A3");
			//Assert.AreEqual ("System.Windows.Forms.TableLayoutSettings", ts.PublicCreateLayoutSettings (ToolStripLayoutStyle.Table).ToString (), "A4");
			Assert.AreEqual (null, ts.PublicCreateLayoutSettings (ToolStripLayoutStyle.VerticalStackWithOverflow), "A5");
		}

		//[Test]
		//public void Accessibility ()
		//{
		//        ToolStripSeparator tsi = new ToolStripSeparator ();
		//        AccessibleObject ao = tsi.AccessibilityObject;

		//        Assert.AreEqual ("ToolStripItemAccessibleObject: Owner = " + tsi.ToString (), ao.ToString (), "L");
		//        Assert.AreEqual (Rectangle.Empty, ao.Bounds, "L1");
		//        Assert.AreEqual ("Press", ao.DefaultAction, "L2");
		//        Assert.AreEqual (null, ao.Description, "L3");
		//        Assert.AreEqual (null, ao.Help, "L4");
		//        Assert.AreEqual (string.Empty, ao.KeyboardShortcut, "L5");
		//        Assert.AreEqual (string.Empty, ao.Name, "L6");
		//        Assert.AreEqual (null, ao.Parent, "L7");
		//        Assert.AreEqual (AccessibleRole.Separator, ao.Role, "L8");
		//        Assert.AreEqual (AccessibleStates.None, ao.State, "L9");
		//        Assert.AreEqual (string.Empty, ao.Value, "L10");

		//        tsi.Name = "Label1";
		//        tsi.Text = "Test Label";
		//        tsi.AccessibleDescription = "Label Desc";

		//        Assert.AreEqual (Rectangle.Empty, ao.Bounds, "L11");
		//        Assert.AreEqual ("Press", ao.DefaultAction, "L12");
		//        Assert.AreEqual ("Label Desc", ao.Description, "L13");
		//        Assert.AreEqual (null, ao.Help, "L14");
		//        Assert.AreEqual (string.Empty, ao.KeyboardShortcut, "L15");
		//        Assert.AreEqual ("Test Label", ao.Name, "L16");
		//        Assert.AreEqual (null, ao.Parent, "L17");
		//        Assert.AreEqual (AccessibleRole.Separator, ao.Role, "L18");
		//        Assert.AreEqual (AccessibleStates.None, ao.State, "L19");
		//        Assert.AreEqual (string.Empty, ao.Value, "L20");

		//        tsi.AccessibleName = "Access Label";
		//        Assert.AreEqual ("Access Label", ao.Name, "L21");

		//        tsi.Text = "Test Label";
		//        Assert.AreEqual ("Access Label", ao.Name, "L22");

		//        tsi.AccessibleDefaultActionDescription = "AAA";
		//        Assert.AreEqual ("AAA", tsi.AccessibleDefaultActionDescription, "L23");
		//}

		private class ExposeProtectedProperties : ToolStripDropDown
		{
			public new CreateParams CreateParams { get { return base.CreateParams; } }
			public new DockStyle DefaultDock { get { return base.DefaultDock; } }
			public new Padding DefaultPadding { get { return base.DefaultPadding; } }
			public new bool DefaultShowItemToolTips { get { return base.DefaultShowItemToolTips; } }
			public new Size MaxItemSize { get { return base.MaxItemSize; } }
			public new bool TopMost { get { return base.TopMost; } }
			public LayoutSettings PublicCreateLayoutSettings (ToolStripLayoutStyle style) { return base.CreateLayoutSettings (style); }
		}
	}
}
