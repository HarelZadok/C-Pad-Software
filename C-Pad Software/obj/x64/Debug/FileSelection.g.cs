#pragma checksum "..\..\..\FileSelection.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2E4ECF1FCACB254613EF866279C021132900DD96D07FCEBDE8115300CEA68359"
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
    /// FileSelection
    /// </summary>
    public partial class FileSelection : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\FileSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Checked;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\FileSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Left;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\FileSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Right;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\FileSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ButtonText;
        
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
            System.Uri resourceLocater = new System.Uri("/C-Pad Software;component/fileselection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\FileSelection.xaml"
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
            
            #line 12 "..\..\..\FileSelection.xaml"
            this.Checked.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.CheckChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Left = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\FileSelection.xaml"
            this.Left.Click += new System.Windows.RoutedEventHandler(this.OnMoveLeft);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Right = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\FileSelection.xaml"
            this.Right.Click += new System.Windows.RoutedEventHandler(this.OnMoveRight);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 20 "..\..\..\FileSelection.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ChooseFileButton);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

