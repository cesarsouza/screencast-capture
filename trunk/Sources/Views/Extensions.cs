using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenCapture.Views
{
    public static class Extensions
    {
        public static void ForceCreateControl(this Control control)
        {
            var method = control.GetType().GetMethod("CreateControl",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic);
            var parameters = method.GetParameters();

            method.Invoke(control, new object[] { true });
        }
    }
}
