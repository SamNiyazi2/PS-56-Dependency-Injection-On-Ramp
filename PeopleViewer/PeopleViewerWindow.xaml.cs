using PeopleViewer.Presentation;
using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using PersonRepository.Service;
using System.Windows;

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
