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

    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Forms;
    using ScreenCapture.Native;

    /// <summary>
    ///   Specifies a component which handles global hotkeys.
    /// </summary>
    /// 
    public class HotKey : Component
    {

        private Keys key;
        private bool enabled;
        private HotKeyNativeWindow window;

        /// <summary>
        ///   Occurs when the hotkey is pressed.
        /// </summary>
        /// 
        public event HandledEventHandler Pressed;

        /// <summary>
        ///   Gets or sets a value indicating whether the <see cref="HotKey"/>
        ///   is listening to user interaction.
        /// </summary>
        /// 
        [DefaultValue(false)]
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///   Gets or sets the triggering key for
        ///   this global <see cref="HotKey"/>.
        /// </summary>
        /// 
        public Keys Key
        {
            get { return key; }
            set
            {
                if (key != value)
                {
                    key = value;
                    OnKeyChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="HotKey"/> component.
        /// </summary>
        /// 
        public HotKey()
        {
            window = new HotKeyNativeWindow(this);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="HotKey"/> component.
        /// </summary>
        /// 
        public HotKey(IContainer container)
            : this()
        {
            if (container == null)
                throw new ArgumentNullException("container");
            container.Add(this);
        }

        /// <summary>
        ///   Raises the <see cref="E:KeyChanged"/> event.
        /// </summary>
        /// 
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// 
        protected virtual void OnKeyChanged(EventArgs e)
        {
            if (DesignMode) return;

            if (Enabled)
            {
                if (window.IsRegistered)
                    window.Unregister();
                window.Register(key);
            }
        }

        /// <summary>
        ///   Raises the <see cref="E:EnabledChanged"/> event.
        /// </summary>
        /// 
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// 
        protected virtual void OnEnabledChanged(EventArgs e)
        {
            if (DesignMode) return;

            if (Enabled)
            {
                if (window.IsRegistered)
                    window.Unregister();
                window.Register(key);
            }
            else
            {
                window.Unregister();
            }
        }

        /// <summary>
        ///   Raises the <see cref="E:Pressed"/> event.
        /// </summary>
        /// 
        /// <param name="handledEventArgs">The <see cref="System.ComponentModel.HandledEventArgs"/> instance containing the event data.</param>
        /// 
        protected virtual void OnPressed(HandledEventArgs handledEventArgs)
        {
            if (Pressed != null)
                Pressed(this, handledEventArgs);
        }



        private class HotKeyNativeWindow : NativeWindow, IMessageFilter
        {
            private HotKey owner;
            private int hotKeyId;

            private static int counter = 1;

            public bool IsRegistered { get; private set; }


            internal HotKeyNativeWindow(HotKey owner)
            {
                this.owner = owner;

            }

            private bool createHandle()
            {
                if (Handle == IntPtr.Zero)
                {
                    CreateParams createParams = new CreateParams();
                    createParams.Style = 0;
                    createParams.ExStyle = 0;
                    createParams.ClassStyle = 0;
                    createParams.Caption = base.GetType().Name;

                    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                        createParams.Parent = NativeMethods.HWND_MESSAGE;

                    CreateHandle(createParams);
                }

                return Handle != IntPtr.Zero;
            }


            public void Register(Keys key)
            {
                if (key == Keys.None) return;
                if (IsRegistered) return;

                if (createHandle())
                {
                    Application.AddMessageFilter(this);
                    hotKeyId = Interlocked.Increment(ref counter);

                    if (!SafeNativeMethods.TryRegisterHotKey(this, key, hotKeyId))
                    {
                        Application.RemoveMessageFilter(this); return;
                    }

                    IsRegistered = true;
                }
            }

            public void Unregister()
            {
                if (!IsRegistered) return;

                SafeNativeMethods.UnregisterHotKey(this, hotKeyId);
                Application.RemoveMessageFilter(this);

                IsRegistered = false;
            }

            public override void ReleaseHandle()
            {
                Unregister();
                base.ReleaseHandle();
            }

            public override void DestroyHandle()
            {
                Unregister();
                base.DestroyHandle();
            }

            public bool PreFilterMessage(ref Message message)
            {
                if (message.Msg != NativeMethods.WM_HOTKEY)
                    return false;

                HandledEventArgs args = new HandledEventArgs();
                if (message.WParam.ToInt32() == hotKeyId)
                    owner.OnPressed(args);

                return args.Handled;
            }
        }


    }
}
