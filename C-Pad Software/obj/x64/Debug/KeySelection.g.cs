﻿#pragma checksum "..\..\..\KeySelection.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FA485440C15976E52868515F2083D9E19D5CA01687763E794D8CBD3A95683EF6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using C_Pad_Software;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace C_Pad_Software {
    
    
    /// <summary>
    /// KeySelection
    /// </summary>
    public partial class KeySelection : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\KeySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Checked;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\KeySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Left;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\KeySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Right;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\KeySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Box;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\KeySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Click;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\KeySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Release;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/C-Pad Software;component/keyselection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\KeySelection.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Checked = ((System.Windows.Controls.CheckBox)(target));
            
            #line 12 "..\..\..\KeySelection.xaml"
            this.Checked.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.CheckChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Left = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\KeySelection.xaml"
            this.Left.Click += new System.Windows.RoutedEventHandler(this.OnMoveLeft);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Right = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\KeySelection.xaml"
            this.Right.Click += new System.Windows.RoutedEventHandler(this.OnMoveRight);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Box = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\KeySelection.xaml"
            this.Box.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OnKeyboardButtonSelected);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 155 "..\..\..\KeySelection.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnKeyRecord);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Click = ((System.Windows.Controls.CheckBox)(target));
            
            #line 163 "..\..\..\KeySelection.xaml"
            this.Click.Checked += new System.Windows.RoutedEventHandler(this.OnClickChanged);
            
            #line default
            #line hidden
            
            #line 164 "..\..\..\KeySelection.xaml"
            this.Click.Unchecked += new System.Windows.RoutedEventHandler(this.OnClickChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Release = ((System.Windows.Controls.CheckBox)(target));
            
            #line 174 "..\..\..\KeySelection.xaml"
            this.Release.Checked += new System.Windows.RoutedEventHandler(this.OnReleaseChanged);
            
            #line default
            #line hidden
            
            #line 175 "..\..\..\KeySelection.xaml"
            this.Release.Unchecked += new System.Windows.RoutedEventHandler(this.OnReleaseChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

