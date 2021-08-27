using PeopleViewer.Presentation;
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
        // 08/27/2021 03:27 pm - SSN - [20210827-1522] - [002] - M02-04 - Demo: Constructor injection
        // public PeopleViewerWindow()
        public PeopleViewerWindow(PeopleViewerViewModel peopleViewerViewModel)
        {
            InitializeComponent();

            // [20210827-1522] - [002] 
            // DataContext = new PeopleViewerViewModel(repository);
            DataContext = peopleViewerViewModel;

        }
    }
}
