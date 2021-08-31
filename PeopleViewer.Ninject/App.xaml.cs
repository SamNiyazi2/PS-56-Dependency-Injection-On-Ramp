using PeopleViewer.Presentation;
using PersonRepository.CachingDecorator;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System.Windows;

// 08/30/2021 12:46 pm - SSN - [20210830-1246] - [001] -  M05-04 - Demo: Using Ninject 

using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Planning.Bindings;

namespace PeopleViewer
{
    public partial class App : Application
    {

        IKernel Container;

        PeopleViewerWindow mw = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            ConfigureContainer("api", false);
            ComposeObjects("api", false);
        }


        private void ConfigureContainer(string dataSourceSelected, bool useCaching)
        {


            if (Container != null)
            {
                while (!Container.IsDisposed)
                {
                    Container.Dispose();
                }
            }

            Container = new StandardKernel();




            // .InSingletonScope()
            // .InThreadScope();
            // .InTransientScope();

            switch (dataSourceSelected)
            {
                case "api":
                    Container.Bind<IPersonRepository>().To<ServiceRepository>()
                        .InSingletonScope();
                    break;

                case "csv":
                    Container.Bind<IPersonRepository>().To<CSVRepository>()
                        .InSingletonScope()
                        .WithConstructorArgument("fullCSVFileName", "CSVFileName");
                    break;

                case "sql":
                    Container.Bind<IPersonRepository>().To<SQLRepository>()
                        .InSingletonScope();
                    break;

                default:
                    throw new Exception($"No case for dataSourceSelected [{dataSourceSelected}] (20210831-1001)");

            }

        }



        private void ComposeObjects(string dataSourceSelected, bool useCaching)
        {

            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Hide();
            }

            mw = Container.Get<PeopleViewerWindow>();

            PeopleViewerViewModel vm = ((PeopleViewerViewModel)mw.DataContext);


            vm.DataSourceSelected = dataSourceSelected;
            vm.DataSourceSelectionVisible = true; 
            vm.UseCachingOptionVisible = false;

            mw.PropertyChanged += Mw_PropertyChanged;

            Application.Current.MainWindow = mw;

            Application.Current.MainWindow.Title = "DI with Ninject - People Viewer";

            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.UpdateLayout();



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

                        ConfigureContainer(dataSourceSelected, useCaching);
                        ComposeObjects(dataSourceSelected, useCaching);
                    }
                }
            }

        }




    }
}
