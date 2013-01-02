using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace ScreenCapture.Controls
{
    /// <summary>
    ///   Specifies a component that creates an icon in the notification area.
    ///   Unlike <see cref="BindableNotifyIcon"/>, this class supports binding
    ///   and can be inherited.
    /// </summary>
    /// 
    public class BindableNotifyIcon : IComponent, IBindableComponent, IDisposable
    {
        private NotifyIcon notifyIcon;
        private ControlBindingsCollection dataBindings;

        private BindingContext bindingContext;


        /// <summary>
        ///   Initializes a new instance of the <see cref="BindableNotifyIcon"/> class.
        /// </summary>
        /// 
        public BindableNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="BindableNotifyIcon"/>
        ///   class with the specified container.
        /// </summary>
        /// 
        /// <param name="container">An <see cref="IContainer"/> that represents
        /// the container for the <see cref="BindableNotifyIcon"/> control.</param>
        /// 
        public BindableNotifyIcon(IContainer container)
        {
            notifyIcon = new NotifyIcon(container);
        }


        #region NotifyIcon members
        /// <summary>
        ///   Gets or sets a value indicating whether the icon
        ///   is visible in the notification area of the taskbar.
        /// </summary>
        /// 
        public bool Visible
        {
            get { return notifyIcon.Visible; }
            set { notifyIcon.Visible = value; }
        }

        /// <summary>
        ///   Occurs when the balloon tip is clicked.
        /// </summary>
        /// 
        public event EventHandler BalloonTipClicked
        {
            add { notifyIcon.BalloonTipClicked += value; }
            remove { notifyIcon.BalloonTipClicked -= value; }
        }

        /// <summary>
        ///   Occurs when the balloon tip is closed by the user.
        /// </summary>
        /// 
        public event EventHandler BalloonTipClosed
        {
            add { notifyIcon.BalloonTipClosed += value; }
            remove { notifyIcon.BalloonTipClosed -= value; }
        }

        /// <summary>
        ///   Gets or sets the icon to display on the balloon 
        ///   tip associated with the <see cref="BindableNotifyIcon"/>.
        /// </summary>
        /// 
        public ToolTipIcon BalloonTipIcon
        {
            get { return notifyIcon.BalloonTipIcon; }
            set { notifyIcon.BalloonTipIcon = value; }
        }

        /// <summary>
        ///   Occurs when the component is disposed by 
        ///   a call to the <see cref="Dispose()"/> method.
        /// </summary>
        /// 
        public event EventHandler Disposed
        {
            add { notifyIcon.Disposed += value; }
            remove { notifyIcon.Disposed -= value; }
        }

        /// <summary>
        ///   Occurs when the balloon tip is displayed on the screen.
        /// </summary>
        /// 
        public event EventHandler BalloonTipShown
        {
            add { notifyIcon.BalloonTipShown += value; }
            remove { notifyIcon.BalloonTipShown -= value; }
        }

        /// <summary>
        ///   Gets or sets the text to display on the balloon
        ///   tip associated with the <see cref="BindableNotifyIcon"/>.
        /// </summary>
        /// 
        public string BalloonTipText
        {
            get { return notifyIcon.BalloonTipText; }
            set { notifyIcon.BalloonTipText = value; }
        }

        /// <summary>
        ///   Gets or sets the title of the balloon tip displayed on the <see cref="BindableNotifyIcon"/>.
        /// </summary>
        public string BalloonTipTitle
        {
            get { return notifyIcon.BalloonTipTitle; }
            set { notifyIcon.BalloonTipTitle = value; }
        }

        /// <summary>
        ///   Occurs when the user clicks the
        ///   icon in the notification area.
        /// </summary>
        /// 
        public event EventHandler Click
        {
            add { notifyIcon.Click += value; }
            remove { notifyIcon.Click -= value; }
        }

        /// <summary>
        ///   Gets or sets the current icon.
        /// </summary>
        /// 
        public Icon Icon
        {
            get { return notifyIcon.Icon; }
            set { notifyIcon.Icon = value; }
        }

        /// <summary>
        ///   Gets or sets the ToolTip text displayed when the 
        ///   mouse pointer rests on a notification area icon.
        /// </summary>
        /// 
        public string Text
        {
            get { return notifyIcon.Text; }
            set { notifyIcon.Text = value; }
        }

        /// <summary>
        ///   Gets or sets the shortcut menu for the icon.
        /// </summary>
        /// 
        public ContextMenu ContextMenu
        {
            get { return notifyIcon.ContextMenu; }
            set { notifyIcon.ContextMenu = value; }
        }

        /// <summary>
        ///   Gets or sets the shortcut menu associated 
        ///   with the <see cref="BindableNotifyIcon"/>.
        /// </summary>
        /// 
        public ContextMenuStrip ContextMenuStrip
        {
            get { return notifyIcon.ContextMenuStrip; }
            set { notifyIcon.ContextMenuStrip = value; }
        }

        /// <summary>
        ///   Occurs when the user double-clicks the icon
        ///   in the notification area of the taskbar.
        /// </summary>
        /// 
        public event EventHandler DoubleClick
        {
            add { notifyIcon.DoubleClick += value; }
            remove { notifyIcon.DoubleClick -= value; }
        }

        /// <summary>
        ///   Occurs when the user clicks a <see cref="BindableNotifyIcon"/> with the mouse.
        /// </summary>
        /// 
        public event MouseEventHandler MouseClick
        {
            add { notifyIcon.MouseClick += value; }
            remove { notifyIcon.MouseClick -= value; }
        }

        /// <summary>
        ///   Occurs when the user double-clicks a <see cref="BindableNotifyIcon"/> with the mouse.
        /// </summary>
        /// 
        public event MouseEventHandler MouseDoubleClick
        {
            add { notifyIcon.MouseDoubleClick += value; }
            remove { notifyIcon.MouseDoubleClick -= value; }
        }

        public event MouseEventHandler MouseDown
        {
            add { notifyIcon.MouseDown += value; }
            remove { notifyIcon.MouseDown -= value; }
        }

        public event MouseEventHandler MouseMove
        {
            add { notifyIcon.MouseMove += value; }
            remove { notifyIcon.MouseMove -= value; }
        }

        public event MouseEventHandler MouseUp
        {
            add { notifyIcon.MouseUp += value; }
            remove { notifyIcon.MouseUp -= value; }
        }

        public ISite Site
        {
            get { return notifyIcon.Site; }
            set { notifyIcon.Site = value; }
        }

        public object Tag
        {
            get { return notifyIcon.Tag; }
            set { notifyIcon.Tag = value; }
        }

        public void ShowBalloonTip(int timeout)
        {
            notifyIcon.ShowBalloonTip(timeout);
        }

        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }
        #endregion


        #region IBindableComponent members

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
        #endregion




        #region IDisposable implementation

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, 
        ///   releasing, or resetting unmanaged resources.
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged resources and performs other cleanup operations 
        ///   before the <see cref="BindableNotifyIcon"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~BindableNotifyIcon()
        {
            Dispose(false);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// 
        /// <param name="disposing"><c>true</c> to release both managed
        /// and unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        ///
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (notifyIcon != null)
                {
                    notifyIcon.Dispose();
                    notifyIcon = null;
                }
            }
        }
        #endregion


    }
}
