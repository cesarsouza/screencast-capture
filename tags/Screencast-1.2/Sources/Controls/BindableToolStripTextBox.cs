// Screencast Capture, free screen recorder
// http://screencast-capture.googlecode.com
//
// Copyright © César Souza, 2012-2013
// cesarsouza at gmail.com
//
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation; either version 2 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program; if not, write to the Free Software
//    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
// 

namespace ScreenCapture.Controls
{
    using System.Windows.Forms;
    using System.Drawing;
    using System;

    /// <summary>
    ///   Tool Strip Button with support for data binding.
    /// </summary>
    /// 
    public class BindableToolStripTextBox : ToolStripTextBox, IBindableComponent
    {
        private ControlBindingsCollection dataBindings;
        private BindingContext bindingContext;
        private bool spring;

        /// <summary>
        ///   Gets or sets whether this control should fill
        ///   the available space in the <see cref="ToolStrip"/>.
        /// </summary>
        /// 
        public bool Spring
        {
            get { return spring; }
            set { spring = value; }
        }

        /// <summary>
        ///   Gets the collection of data-binding objects for this
        ///   <see cref="T:System.Windows.Forms.IBindableComponent"/>.
        /// </summary>
        /// 
        /// <returns>The <see cref="T:System.Windows.Forms.ControlBindingsCollection"/> 
        /// for this <see cref="T:System.Windows.Forms.IBindableComponent"/>. </returns>
        /// 
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (dataBindings == null)
                    dataBindings = new ControlBindingsCollection(this);
                return dataBindings;
            }
        }

        /// <summary>
        ///   Gets or sets the collection of currency managers for the
        ///   <see cref="T:System.Windows.Forms.IBindableComponent"/>.
        /// </summary>
        /// 
        /// <returns>The collection of <see cref="T:System.Windows.Forms.BindingManagerBase"/> 
        /// objects for this <see cref="T:System.Windows.Forms.IBindableComponent"/>.</returns>
        /// 
        public BindingContext BindingContext
        {
            get
            {
                if (bindingContext == null)
                    bindingContext = new BindingContext();
                return bindingContext;
            }
            set { bindingContext = value; }
        }

        /// <summary>
        ///   Gets the preferred size for the control.
        /// </summary>
        /// 
        public override Size GetPreferredSize(Size constrainingSize)
        {
            if (!spring)
                return base.GetPreferredSize(constrainingSize);

            // Use the default size if the text box is on the overflow menu 
            // or is on a vertical ToolStrip. 
            if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
            {
                return DefaultSize;
            }

            // Declare a variable to store the total available width as  
            // it is calculated, starting with the display width of the  
            // owning ToolStrip.
            int width = Owner.DisplayRectangle.Width;

            // Subtract the width of the overflow button if it is displayed.  
            if (Owner.OverflowButton.Visible)
            {
                width = width - Owner.OverflowButton.Width -
                    Owner.OverflowButton.Margin.Horizontal;
            }

            // Declare a variable to maintain a count of ToolStripSpringTextBox  
            // items currently displayed in the owning ToolStrip. 
            int springBoxCount = 0;

            foreach (ToolStripItem item in Owner.Items)
            {
                // Ignore items on the overflow menu. 
                if (item.IsOnOverflow) continue;

                if (item is BindableToolStripTextBox)
                {
                    // For ToolStripSpringTextBox items, increment the count and  
                    // subtract the margin width from the total available width.
                    springBoxCount++;
                    width -= item.Margin.Horizontal;
                }
                else
                {
                    // For all other items, subtract the full width from the total 
                    // available width.
                    width = width - item.Width - item.Margin.Horizontal;
                }
            }

            // If there are multiple ToolStripSpringTextBox items in the owning 
            // ToolStrip, divide the total available width between them.  
            if (springBoxCount > 1) width /= springBoxCount;

            // If the available width is less than the default width, use the 
            // default width, forcing one or more items onto the overflow menu. 
            if (width < DefaultSize.Width) width = DefaultSize.Width;

            // Retrieve the preferred size from the base class, but change the 
            // width to the calculated width. 
            Size size = base.GetPreferredSize(constrainingSize);
            size.Width = width;
            return size;
        }

        /// <summary>
        ///   Releases the unmanaged resources used by the 
        ///   <see cref="T:System.Windows.Forms.ToolStripMenuItem"/>
        ///   and optionally releases the managed resources.
        /// </summary>
        /// 
        /// <param name="disposing">true to release both managed and unmanaged
        /// resources; false to release only unmanaged resources.</param>
        /// 
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
