﻿#pragma checksum "..\..\..\Windows\AdditionalModules.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3ABF46C9A690826A8F71EA39E20EFF42"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TibiaHeleper.Windows;


namespace TibiaHeleper.Windows {
    
    
    /// <summary>
    /// AdditionalModules
    /// </summary>
    public partial class AdditionalModules : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AHSpell;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AutoHasteRow;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AHSpellLabel;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AHManaLabel;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AHMana;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Windows\AdditionalModules.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox AHEnable;
        
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
            System.Uri resourceLocater = new System.Uri("/TibiaHeleper;component/windows/additionalmodules.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AdditionalModules.xaml"
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
            
            #line 9 "..\..\..\Windows\AdditionalModules.xaml"
            ((TibiaHeleper.Windows.AdditionalModules)(target)).Loaded += new System.Windows.RoutedEventHandler(this.AssignData);
            
            #line default
            #line hidden
            return;
            case 2:
            this.button = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.AHSpell = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.AutoHasteRow = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.AHSpellLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.AHManaLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.AHMana = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.AHEnable = ((System.Windows.Controls.CheckBox)(target));
            
            #line 17 "..\..\..\Windows\AdditionalModules.xaml"
            this.AHEnable.Checked += new System.Windows.RoutedEventHandler(this.EnableAutoHaste);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\Windows\AdditionalModules.xaml"
            this.AHEnable.Unchecked += new System.Windows.RoutedEventHandler(this.DisableAutoHate);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

