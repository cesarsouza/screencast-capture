// Screencast Capture Lite 
// http://www.crsouza.com
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

namespace ScreenCapture
{
    using System;
    using System.Windows.Forms;

    public static class BindingExtensions
    {
        public static void Bind<T, U>(this IBindableComponent component, Binding binding, Func<T, U> transform)
        {
            binding.Parse += (sender, e) => e.Value = transform((T)e.Value);
            binding.Format += (sender, e) => e.Value = transform((T)e.Value);
            component.DataBindings.Add(binding);
        }

        public static void Bind(this IBindableComponent component, Binding binding)
        {
            component.DataBindings.Add(binding);
        }

        public static void Bind<T, U>(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember, Func<T, U> transform)
        {
            Bind(component, new Binding(propertyName, dataSource, dataMember), transform);
        }

        public static void Bind(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember)
        {
            Bind(component, new Binding(propertyName, dataSource, dataMember));
        }
    }
}
