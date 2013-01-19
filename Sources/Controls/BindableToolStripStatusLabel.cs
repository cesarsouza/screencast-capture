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

    /// <summary>
    ///   Tool Strip Status Label with support for data binding.
    /// </summary>
    /// 
    public class BindableToolStripStatusLabel : ToolStripStatusLabel, IBindableComponent
    {
        private ControlBindingsCollection dataBindings;

        private BindingContext bindingContext;

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
        ///   Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripMenuItem"/> and optionally releases the managed resources.
        /// </summary>
        /// 
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        /// 
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
