using PeopleViewer.Presentation;
using System;
using System.ComponentModel;
using System.Windows;

// 08/27/2021 04:39 pm - SSN - [20210827-1601] - [002] - M02-06 - Demo: Adding the bootstrapper
// This form was moved from PeopleViewer to this library
// The following references were added:
// PresentationCore
// PresentationFramework
// System.Xaml
// WindowsBase

namespace PeopleViewer
{
    public partial class PeopleViewerWindow : Window
    {

        PeopleViewerViewModel vm;

        // 08/27/2021 03:27 pm - SSN - [20210827-1522] - [002] - M02-04 - Demo: Constructor injection
        // public PeopleViewerWindow()



        public PeopleViewerWindow(PeopleViewerViewModel peopleViewerViewModel)
        {
            InitializeComponent();

            // [20210827-1522] - [002] 
            // DataContext = new PeopleViewerViewModel(repository);

            this.DataContextChanged += PeopleViewerWindow_DataContextChanged;

            DataContext = peopleViewerViewModel;

        }


        private void PeopleViewerWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string dataContextChanged = "test";

            Type type = e.NewValue.GetType();

            if (type.Name == "PeopleViewerViewModel")
            {
                vm = ((PeopleViewerViewModel)e.NewValue);
                string test = vm.DataSourceSelected;

                vm.PropertyChanged += PeopleViewerViewModel_PropertyChanged;


            }

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void PeopleViewerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e.PropertyName));


        }



    }
}
