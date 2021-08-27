using PeopleViewer.Presentation;
using PersonRepository.Interface;
using PersonRepository.Service;
using System.Windows;

namespace PeopleViewer
{
    public partial class App : Application
    {

        // 08/27/2021 03:40 pm - SSN - [20210827-1522] - [004] - M02-04 - Demo: Constructor injection
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            PeopleViewerViewModel vm = ComposeViewModel();
            Application.Current.MainWindow = new PeopleViewerWindow(vm);
            Application.Current.MainWindow.Title = "Loose Coupling - People Viewer";
            Application.Current.MainWindow.Show();

        }

        private static PeopleViewerViewModel ComposeViewModel()
        {
            IPersonRepository personRepository = new ServiceRepository();
            PeopleViewerViewModel vm = new PeopleViewerViewModel(personRepository);
            return vm;
        }
    }
}
