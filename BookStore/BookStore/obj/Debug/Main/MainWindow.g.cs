﻿#pragma checksum "..\..\..\Main\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2CC999B9DE9CF841D8E0A5A5654A0984"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BookStore;
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


namespace BookStore {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgItems;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTotal;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTotalNum;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveFromCart;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddInvoice;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditInvoice;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteInvoice;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblID;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblIDnum;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDate;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDatePick;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblItem;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbItems;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddToCart;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBItemPrice;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/BookStore;component/main/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Main\MainWindow.xaml"
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
            
            #line 30 "..\..\..\Main\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MI_Search_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 31 "..\..\..\Main\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MI_Update_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgItems = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.lblTotal = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblTotalNum = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btnRemoveFromCart = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\Main\MainWindow.xaml"
            this.btnRemoveFromCart.Click += new System.Windows.RoutedEventHandler(this.btnRemoveFromCart_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnAddInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\Main\MainWindow.xaml"
            this.btnAddInvoice.Click += new System.Windows.RoutedEventHandler(this.btnAddInvoice_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnEditInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\Main\MainWindow.xaml"
            this.btnEditInvoice.Click += new System.Windows.RoutedEventHandler(this.btnEditInvoice_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnDeleteInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\Main\MainWindow.xaml"
            this.btnDeleteInvoice.Click += new System.Windows.RoutedEventHandler(this.btnDeleteInvoice_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblID = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.lblIDnum = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.lblDate = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.dpDatePick = ((System.Windows.Controls.DatePicker)(target));
            
            #line 76 "..\..\..\Main\MainWindow.xaml"
            this.dpDatePick.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpDatePick_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.lblItem = ((System.Windows.Controls.Label)(target));
            return;
            case 15:
            this.cbItems = ((System.Windows.Controls.ComboBox)(target));
            
            #line 83 "..\..\..\Main\MainWindow.xaml"
            this.cbItems.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbItems_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.btnAddToCart = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\Main\MainWindow.xaml"
            this.btnAddToCart.Click += new System.Windows.RoutedEventHandler(this.btnAddToCart_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.txtBItemPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 18:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\..\Main\MainWindow.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\Main\MainWindow.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

