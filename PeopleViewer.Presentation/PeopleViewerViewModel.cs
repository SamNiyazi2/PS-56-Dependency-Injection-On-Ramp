using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PeopleViewer.Presentation
{
    public class PeopleViewerViewModel : INotifyPropertyChanged
    {

        // 08/27/2021 03:19 pm - SSN - [20210827-1500] - [003] - M02-03 - Demo: Adding the repository interface
        // protected ServiceRepository Repository;
        protected IPersonRepository Repository_v03;


        public bool DataSourceSelectionVisible { get; set; } = false;
        public bool UseCachingOptionVisible  { get; set; } = false;
        

        public bool chkDataSource_api_isChecked
        {
            get { return DataSourceSelected == "api"; }
            set { }
        }


        public bool chkDataSource_csv_isChecked
        {
            get { return DataSourceSelected == "csv"; }
            set { }
        }

        public bool chkDataSource_sql_isChecked
        {
            get { return DataSourceSelected == "sql"; }
            set { }
        }


        private IEnumerable<Person> _people;
        public IEnumerable<Person> People
        {
            get { return _people; }
            set
            {
                if (_people == value)
                    return;
                _people = value;
                RaisePropertyChanged("People");
            }
        }



        private string _dataSourceSelected;

        public string DataSourceSelected
        {
            get { return _dataSourceSelected; }
            set
            {
                _dataSourceSelected = value;

                RaisePropertyChanged(nameof(DataSourceSelected));
            }
        }

        private bool _UseCachingSelected;

        public bool UseCachingSelected
        {
            get { return _UseCachingSelected; }
            set
            {
                _UseCachingSelected = value;
                RaisePropertyChanged(nameof(UseCachingSelected));
            }
        }

        // 08/27/2021 03:26 pm - SSN - [20210827-1522] - [001] - M02-04 - Demo: Constructor injection
        public PeopleViewerViewModel(IPersonRepository repository)
        {
            //Repository = new ServiceRepository();
            Repository_v03 = repository;
           
        }

        #region Commands



        #region RefreshCommand Standard Stuff

        private RefreshCommand _refreshPeopleCommand = new RefreshCommand();
        public RefreshCommand RefreshPeopleCommand
        {
            get
            {
                ////////////////////////////////////////   if (_refreshPeopleCommand.ViewModel == null)
                _refreshPeopleCommand.ViewModel = this;
                return _refreshPeopleCommand;
            }

        }

        public class RefreshCommand : ICommand
        {
            public PeopleViewerViewModel ViewModel
            {
                get;
                set;
            }
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                ViewModel.People = ViewModel.Repository_v03.GetPeople();
            }
        }


        #endregion RefreshCommand Standard Stuff



        #region ClearCommand Standard Stuff

        private ClearCommand _clearPeopleCommand = new ClearCommand();
        public ClearCommand ClearPeopleCommand
        {
            get
            {
                if (_clearPeopleCommand.ViewModel == null)
                    _clearPeopleCommand.ViewModel = this;
                return _clearPeopleCommand;
            }
        }

        public class ClearCommand : ICommand
        {
            public PeopleViewerViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter)
            {
                return true;
            }


            public void Execute(object parameter)
            {
                ViewModel.People = new List<Person>();

            }
        }

        #endregion ClearCommand Standard Stuff



        #region SelectDataSource Standard Stuff

        private SelectDataSource _selectDataSourcePeople = new SelectDataSource();
        public SelectDataSource SelectDataSourcePeople
        {
            get
            {
                //////////////////////  if (_selectDataSourcePeople.ViewModel == null)
                _selectDataSourcePeople.ViewModel = this;
                return _selectDataSourcePeople;
            }
        }

        public class SelectDataSource : ICommand
        {
            public PeopleViewerViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;

            
            public bool CanExecute(object parameter)
            {

                return true;
            }


            public async void Execute(object parameter)
            {

                ViewModel.DataSourceSelected = parameter.ToString();

                ViewModel.People = new List<Person>();

            }
        }

        #endregion  SelectDataSource Standard Stuff






        #region UseCaching 

        private UseCaching _useCachingPeople = new UseCaching();
        public UseCaching UseCachingPeople
        {
            get
            {
                if (_useCachingPeople.ViewModel == null)
                    _useCachingPeople.ViewModel = this;
                return _useCachingPeople;
            }
            set
            {
                _useCachingPeople = value;

            }
        }

        public class UseCaching : ICommand
        {
            public PeopleViewerViewModel ViewModel { get; set; }
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object parameter)
            {
                return true;
            }


            public async void Execute(object parameter)
            {
                string temp = "XXX";

                //       ViewModel.UseCachingSelected =   parameter.ToString();
            }
        }

        #endregion  SelectDataSource Standard Stuff




        #endregion




        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
