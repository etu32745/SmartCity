using BackEndSmartCity.DataAccess;
using BackEndSmartCity.Model;
using BackEndSmartCity.Service;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BackEndSmartCity.ViewModel
{
    public class UserManagerPageViewModel:CommonViewModel
    {
        private String _userSelectionné;
        private ObservableCollection<String> _usersString;
        private UserDataAccess _userDataAccess;
        private IEnumerable<User> _users;

        public ICommand Supprimer => new RelayCommand(() => SupprimerUser());
        public ICommand Afficher => new RelayCommand(() => AfficherRapport());

        public ObservableCollection<String> Users
        {
            get => _usersString;
            set
            {
                _usersString = value;
                RaisePropertyChanged("Users");
            }
        }
        public String UserChoisi
        {
            get => _userSelectionné;
            set
            {
                _userSelectionné = value;
                RaisePropertyChanged("UserChoisi");
            }
        }
        public UserManagerPageViewModel(INavigationService _navigation):base(_navigation)
        {
            _userDataAccess = new UserDataAccess();
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            _users = await _userDataAccess.Get();
            Users = new ObservableCollection<string>();
            foreach(var user in _users)
            {
                Users.Add(user.Username);
            }
            Refresh();
        }

        private void SupprimerUser()
        {
            _userDataAccess.Delete(UserChoisi);
            Users.Remove(UserChoisi);
            UserChoisi = null;
        }
        
        private void AfficherRapport()
        {
            //implémenter l'attribut rapport dans les user dans l'api...
        }

        private void Refresh()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                rootFrame.Navigate(typeof(UserManagerPage));
            }
        }
    }
}
