using Microsoft.Practices.Unity;
using PeopleViewer.Presentation;
using PersonRepository.CachingDecorator;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System;
using System.Windows;


namespace PeopleViewer
{
    public partial class App : Application
    {
        // 08/30/2021 11:43 am - SSN - [20210830-1143] - [001] - M05-03 - Demo: Using Unity
        // Add NuGet package Unity 4.0.1 (Guess)
        IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }



        ParameterOverride param = null;



        private void ConfigureContainer()
        {

            Container = new UnityContainer();
             

            
            // Applies when registering type CSVRepository
            param = new ParameterOverride("fullCSVFileName", "CSVFileName");


             // Container.RegisterType<IPersonRepository, ServiceRepository>
             Container.RegisterType<IPersonRepository, CSVRepository>
            // Container.RegisterType<IPersonRepository, SQLRepository>

                (
                new ContainerControlledLifetimeManager()
                );

        }


        private void ComposeObjects()
        {

            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>(param);

            Application.Current.MainWindow.Title = "DI with Unity - People Viewer";
        }


    }
}
