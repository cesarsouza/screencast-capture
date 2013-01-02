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
        public static void Bind<TSource, TDestination>(this IBindableComponent component, Binding binding, Func<TSource, TDestination> format)
        {
            if (component == null) throw new ArgumentNullException("component");
            if (binding == null) throw new ArgumentNullException("binding");
            binding.Format += (sender, e) => e.Value = format((TSource)e.Value);
            component.DataBindings.Add(binding);
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
        public static void Bind<TSource, TDestination>(this IBindableComponent component, Binding binding, Func<TSource, TDestination> format, Func<TDestination, TSource> parse)
        {
            if (component == null) throw new ArgumentNullException("component");
            if (binding == null) throw new ArgumentNullException("component");
            binding.Parse += (sender, e) => e.Value = parse((TDestination)e.Value);
            binding.Format += (sender, e) => e.Value = format((TSource)e.Value);
            component.DataBindings.Add(binding);
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
        public static void Bind<TSource, TDestination>(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember, Func<TSource, TDestination> format)
        {
            if (component == null) throw new ArgumentNullException("component");
            Bind(component, new Binding(propertyName, dataSource, dataMember), format);
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
        public static void Bind<TSource, TDestination>(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember,
            Func<TSource, TDestination> format, Func<TDestination, TSource> parse)
        {
            if (component == null) throw new ArgumentNullException("component");
            Bind(component, new Binding(propertyName, dataSource, dataMember), format, parse);
        }

        /// <summary>
        ///   Attaches a binding to a component, using the given format transformation.
        /// </summary>
        /// 
        /// <param name="component">The component to which the binding applies (such as a control in the form).</param>
        /// <param name="binding">The binding object which defines the property being bound (such as a ViewModel property).</param>
        /// 
        public static void Bind(this IBindableComponent component, Binding binding)
        {
            if (component == null) throw new ArgumentNullException("component");
            component.DataBindings.Add(binding);
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
        public static void Bind(this IBindableComponent component,
            string propertyName, object dataSource, string dataMember)
        {
            if (component == null) throw new ArgumentNullException("component");
            Bind(component, new Binding(propertyName, dataSource, dataMember));
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
        public static void Bind<TControl, TSource>(this IBindableComponent component,
            Expression<Func<TControl, object>> propertyName, TSource dataSource,
            Expression<Func<TSource, object>> dataMember)
        {
            string propertyNameStr, dataMemberStr;
            getNames(propertyName, dataMember, out propertyNameStr, out dataMemberStr);

            Bind(component, propertyNameStr, dataSource, dataMemberStr);
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
        public static void Bind<TControl, TModel, TSource, TDestination>(this IBindableComponent component,
           Expression<Func<TControl, TDestination>> propertyName, TSource dataSource,
           Expression<Func<TModel, TSource>> dataMember, Func<TSource, TDestination> format)
        {
            string propertyNameStr, dataMemberStr;
            getNames(propertyName, dataMember, out propertyNameStr, out dataMemberStr);

            Bind(component, propertyNameStr, dataSource, dataMemberStr, format);
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
        public static void Bind<TControl, TModel, TSource, TDestination>(this IBindableComponent component,
          Expression<Func<TControl, TDestination>> propertyName, TSource dataSource,
          Expression<Func<TModel, TSource>> dataMember, Func<TSource, TDestination> format,
            Func<TDestination, TSource> parse)
        {
            string propertyNameStr, dataMemberStr;
            getNames(propertyName, dataMember, out propertyNameStr, out dataMemberStr);

            Bind(component, propertyNameStr, dataSource, dataMemberStr, format, parse);
        }



        private static void getNames<TControl, TModel, TSource, TDestination>(Expression<Func<TControl, TDestination>> propertyName, Expression<Func<TModel, TSource>> dataMember, out string propertyNameStr, out string dataMemberStr)
        {
            MemberExpression exp1 = memberInfo(propertyName);

            if (exp1 == null)
                throw new ArgumentException("Lambda expression for PropertyName is not correct.");

            propertyNameStr = exp1.Member.Name;

            MemberExpression exp2 = memberInfo(dataMember);

            if (exp2 == null)
                throw new ArgumentException("Lambda expression for DataMember is not correct.");

            dataMemberStr = exp2.Member.Name;
        }

        private static MemberExpression memberInfo(Expression exp)
        {
            LambdaExpression lambdaExp = exp as LambdaExpression;

            if (lambdaExp == null)
                throw new ArgumentNullException("Lambda expression syntax is not correct");

            MemberExpression memberExp = null;

            if (lambdaExp.Body.NodeType == ExpressionType.MemberAccess)
                memberExp = lambdaExp.Body as MemberExpression;

            else if (lambdaExp.Body.NodeType == ExpressionType.Convert)
                memberExp = ((UnaryExpression)lambdaExp.Body).Operand as MemberExpression;

            return memberExp;
        }

    }
}
