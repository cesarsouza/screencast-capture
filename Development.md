# Requirements #

In order to develop Screencast Capture Lite, you will need at least .NET 4 and Visual Studio 2010. It may also be possible to build using SharpDevelop.

# Introduction #

Screencast Capture uses the Model-View-ViewModel (MVVM) pattern to manage application complexity in Windows Forms. While MVVM originated as a design pattern for developing WPF and Silverlight aplications, the main concepts (ui/logic separation, databinding) can also be applied to other technologies, such as Windows Forms.

The following image shows the class diagram for the Views and ViewModels of the application (click to enlarge).

<a href='https://screencast-capture.googlecode.com/svn/wiki/images/diagrams/Application.png'>
<img src='https://screencast-capture.googlecode.com/svn/wiki/images/diagrams/Application.png' /></a>


## Native Win32 APIs ##

Screencast Capture makes extensive use of native Win32 APIs through Interop. The following image shows the class diagram for the native wrappers used by application (click to enlarge).

<a href='https://screencast-capture.googlecode.com/svn/wiki/images/diagrams/NativeInterop.png'>
<img src='https://screencast-capture.googlecode.com/svn/wiki/images/diagrams/NativeInterop.png' /></a>