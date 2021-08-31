using PeopleViewer.Presentation;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using PersonRepository.CachingDecorator;
using System.Windows;
using System;

namespace PeopleViewer
{
    public partial class App : Application
    {

        // 08/27/2021 03:40 pm - SSN - [20210827-1522] - [004] - M02-04 - Demo: Constructor injection
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            PeopleViewerWindow mw = new PeopleViewerWindow(null);
            Application.Current.MainWindow = mw;

            mw.PropertyChanged += Mw_PropertyChanged;
            ComposeViewModel();

            Application.Current.MainWindow.Title = "Loose Coupling - People Viewer";
            Application.Current.MainWindow.Show();




        }


        string last_dataSourceSelected;

        private void ComposeViewModel(string dataSourceSelected = "api", bool useCaching = false)
        {

            dataSourceSelected = dataSourceSelected ?? "api";

            if (last_dataSourceSelected != dataSourceSelected) { useCaching = false; }
            last_dataSourceSelected = dataSourceSelected;

            IPersonRepository personRepository;
            IPersonRepository wrappedRepository;

            // 08/30/2021 06:47 am - SSN - [20210827-1701] - [007] - M03-03 - Demo: Additional repositories
            // 08/30/2021 08:00 am - SSN - [20210830-0800] - [001] - M03-05 - Demo: Using the caching repository


            switch (dataSourceSelected)
            {
                case "api":

                    personRepository = new ServiceRepository();
                    break;

                case "csv":

                    personRepository = new CSVRepository("CSVFileName");
                    break;

                case "sql":

                    personRepository = new SQLRepository();
                    break;

                default:
                    throw new Exception($"Invalid dataSourceSelected selected [{dataSourceSelected}]  (20210831-1647)");

            }


            PeopleViewerViewModel vm3;

            if (useCaching)
            {
                wrappedRepository = new CachingRepository(personRepository);
                vm3 = new PeopleViewerViewModel(wrappedRepository);
            }
            else
            {
                vm3 = new PeopleViewerViewModel(personRepository);
            }
             

            vm3.DataSourceSelected = dataSourceSelected;
            vm3.DataSourceSelectionVisible = true;
            vm3.UseCachingOptionVisible = true;


            Application.Current.MainWindow.DataContext = vm3;
        }


        private void Mw_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {


            if (e.PropertyName == "DataSourceSelected" || e.PropertyName == "UseCachingSelected")
            {
                Type type = sender.GetType();

                if (type.Name == "PeopleViewerWindow")
                {
                    PeopleViewerWindow _mw = sender as PeopleViewerWindow;

                    PeopleViewerViewModel _vm = _mw.DataContext as PeopleViewerViewModel;

                    if (_vm != null)
                    {
                        string dataSourceSelected = _vm.DataSourceSelected;
                        bool useCaching = _vm.UseCachingSelected;

                        ComposeViewModel(dataSourceSelected, useCaching);
                    }
                }
            }

        }


    }
}
