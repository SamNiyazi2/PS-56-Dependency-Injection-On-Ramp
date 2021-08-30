using PeopleViewer.Presentation;
using PersonRepository.CachingDecorator;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System.Windows;

// 08/30/2021 12:46 pm - SSN - [20210830-1246] - [001] -  M05-04 - Demo: Using Ninject 
using Ninject;
using Ninject.Parameters;

namespace PeopleViewer
{
    public partial class App : Application
    {

        IKernel Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }


        private void ConfigureContainer()
        {

            Container = new StandardKernel();


            Container.Bind<IPersonRepository>().To<ServiceRepository>()
            // Container.Bind<IPersonRepository>().To<CSVRepository>()
            // Container.Bind<IPersonRepository>().To<SQLRepository>()

                .InSingletonScope()
                // .InThreadScope();
                // .InTransientScope();

                // Needed when applying CSVRepository.  Does not affect the other two.
                .WithConstructorArgument("fullCSVFileName", "CSVFileName");

        }



        private void ComposeObjects()
        {
            Application.Current.MainWindow = Container.Get<PeopleViewerWindow>();
            Application.Current.MainWindow.Title = "DI with Ninject - People Viewer";
        }
    }
}
