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

namespace ScreenCapture
{
    using System;
    using System.Windows.Forms;
    using System.Linq.Expressions;

    /// <summary>
    ///   Extension methods for binding.
    /// </summary>
    /// 
    /// <remarks>
    ///   The methods available in this class provide an easier way to configure windows forms
    ///   databindings using lambda functions. It is possible to direct configure the binding
    ///   (such as format and parsing options for two-way binding) in a single, one-line call
    ///   at the time of the binding.
    /// </remarks>
    /// 
    /// <example>
    /// <para>
    ///   The simplest usage scenario is:
    /// </para>
    /// <code>
    ///   myButton.Bind("Visible", viewModel, "SomeModelViewProperty");
    /// </code>
    /// 
    /// <para>
    ///   If you wish to have control over the binding, you can apply a transformation
    ///   (similar to what WPF converters do). Supposing you have a boolean property,
    ///   you can use:</para>
    /// <code>
    ///   myButton.Bind("Visible", viewModel, "SomeModelViewProperty", (bool value) => !value);
    /// </code>
    /// 
    /// <para>
    ///   In case you desire type-safety or are using obfuscation, you can use:
    /// </para>
    /// <code>
    ///   myButton.Bind(b => b.Enabled, viewModel, m => m.SomeViewModelProperty);
    /// </code>
    /// 
    /// <para>
    ///   And again you can fine control the behavior of the binding by using:
    /// </para>
    /// <code>
    ///   myButton.Bind(b => b.Enabled, viewModel, m => m.SomeViewModelProperty, value => !value);
    /// </code>
    /// </example>
    /// 
    public static class BindingExtensions
    {


        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <typeparam name="TSource">The type of the source property.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="binding">The binding object which defines the property being bound (such as a ViewModel property).</param>
        /// <param name="format">The format transformation which transforms the model's property to a control's value.</param>
        /// 
        public static Binding Bind<TSource, TDestination>(this IBindableComponent component, 
            Binding binding, Func<TSource, TDestination> format)
        {
            if (component == null) throw new ArgumentNullException("component");
            if (binding == null) throw new ArgumentNullException("binding");
            binding.Format += (sender, e) => e.Value = format((TSource)e.Value);
            binding.FormattingEnabled = true;
            component.DataBindings.Add(binding);
            return binding;
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <typeparam name="TSource">The type of the source property.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="binding">The binding object which defines the property being bound (such as a ViewModel property).</param>
        /// <param name="format">The format transformation which transforms the model's property to a control's value.</param>
        /// <param name="parse">The parse transformation which transforms the control's value into the model's property.</param>
        /// 
        public static Binding Bind<TSource, TDestination>(this IBindableComponent component,
            Binding binding, Func<TSource, TDestination> format, Func<TDestination, TSource> parse)
        {
            if (component == null) throw new ArgumentNullException("component");
            if (binding == null) throw new ArgumentNullException("component");
            binding.Parse += (sender, e) => e.Value = parse((TDestination)e.Value);
            binding.Format += (sender, e) => e.Value = format((TSource)e.Value);
            binding.FormattingEnabled = true;
            component.DataBindings.Add(binding);
            return binding;
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <typeparam name="TSource">The type of the source property.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="propertyName">The name of the control property to bind.</param>
        /// <param name="dataSource">An object that represents the data source.</param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="format">The format transformation which transforms the model's property to a control's value.</param>
        /// 
        public static Binding Bind<TSource, TDestination>(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember, Func<TSource, TDestination> format)
        {
            if (component == null) throw new ArgumentNullException("component");
            return Bind(component, new Binding(propertyName, dataSource, dataMember), format);
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <typeparam name="TSource">The type of the source property.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="propertyName">The name of the control property to bind.</param>
        /// <param name="dataSource">An object that represents the data source.</param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="format">The format transformation which transforms the model's property to a control's value.</param>
        /// <param name="parse">The parse transformation which transforms the control's value into the model's property.</param>
        /// 
        public static Binding Bind<TSource, TDestination>(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember,
            Func<TSource, TDestination> format, Func<TDestination, TSource> parse)
        {
            if (component == null) throw new ArgumentNullException("component");
            return Bind(component, new Binding(propertyName, dataSource, dataMember, true), format, parse);
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="binding">The binding object which defines the property being bound (such as a ViewModel property).</param>
        /// 
        public static Binding Bind(this IBindableComponent component, Binding binding)
        {
            if (component == null) throw new ArgumentNullException("component");
            component.DataBindings.Add(binding);
            return binding;
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="propertyName">The name of the control property to bind.</param>
        /// <param name="dataSource">An object that represents the data source.</param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// 
        public static Binding Bind(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember)
        {
            if (component == null) throw new ArgumentNullException("component");

            Binding binding = new Binding(propertyName, dataSource, dataMember);
            binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;

            return Bind(component, binding);
        }

        /// <summary>
        ///   Attaches a binding to a component.
        /// </summary>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="propertyName">The name of the control property to bind.</param>
        /// <param name="dataSource">An object that represents the data source.</param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// 
        public static Binding Bind<TControl, TModel, TSource>(this TControl component,
            Expression<Func<TControl, object>> propertyName, TModel dataSource,
            Expression<Func<TModel, TSource>> dataMember)
            where TControl : IBindableComponent
        {
            string nameString, dataString;
            getNames(propertyName, dataMember, out nameString, out dataString);

            if (nameString == null) throw new ArgumentException("Unexpected expression.", "propertyName");
            if (dataString == null) throw new ArgumentException("Unexpected expression.", "dataMember");

            return Bind(component, nameString, dataSource, dataString);
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="propertyName">The name of the control property to bind.</param>
        /// <param name="dataSource">An object that represents the data source.</param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="format">The format transformation which transforms the model's property to a control's value.</param>
        /// 
        public static Binding Bind<TControl, TModel, TSource, TDestination>(this TControl component,
           Expression<Func<TControl, TDestination>> propertyName, TModel dataSource,
           Expression<Func<TModel, TSource>> dataMember, Func<TSource, TDestination> format)
            where TControl : IBindableComponent
        {
            string nameString, dataString;
            getNames(propertyName, dataMember, out nameString, out dataString);

            if (nameString == null) throw new ArgumentException("Unexpected expression.", "propertyName");
            if (dataString == null) throw new ArgumentException("Unexpected expression.", "dataMember");

            return Bind(component, nameString, dataSource, dataString, format);
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="propertyName">The name of the control property to bind.</param>
        /// <param name="dataSource">An object that represents the data source.</param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="format">The format transformation which transforms the model's property to a control's value.</param>
        /// <param name="parse">The parse transformation which transforms the control's value into the model's property.</param>
        /// 
        public static Binding Bind<TControl, TModel, TSource, TDestination>(this TControl component,
           Expression<Func<TControl, TDestination>> propertyName, TModel dataSource,
           Expression<Func<TModel, TSource>> dataMember, Func<TSource, TDestination> format,
            Func<TDestination, TSource> parse)
            where TControl : IBindableComponent
        {
            string nameString, dataString;
            getNames(propertyName, dataMember, out nameString, out dataString);

            if (nameString == null) throw new ArgumentException("Unexpected expression.", "propertyName");
            if (dataString == null) throw new ArgumentException("Unexpected expression.", "dataMember");

            return Bind(component, nameString, dataSource, dataString, format, parse);
        }



        private static void getNames<TControl, TModel, TSource, TDestination>(
            Expression<Func<TControl, TDestination>> propertyNameExpression,
            Expression<Func<TModel, TSource>> dataMemberExpression,
            out string propertyName, out string dataMember)
        {
            propertyName = null;
            dataMember = null;

            MemberExpression exp1 = memberInfo(propertyNameExpression);

            if (exp1 == null) return;
            propertyName = exp1.Member.Name;

            MemberExpression exp2 = memberInfo(dataMemberExpression);

            if (exp2 == null) return;
            dataMember = exp2.Member.Name;
        }

        private static MemberExpression memberInfo(LambdaExpression exp)
        {
            MemberExpression memberExp = null;

            if (exp.Body.NodeType == ExpressionType.MemberAccess)
                memberExp = exp.Body as MemberExpression;

            else if (exp.Body.NodeType == ExpressionType.Convert)
                memberExp = ((UnaryExpression)exp.Body).Operand as MemberExpression;

            return memberExp;
        }

    }
}
