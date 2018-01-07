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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BackEndSmartCity.ViewModel
{
    public class ComplexePageViewModel:CommonViewModel
    {
        private String _sportSélectionné,
            _complexeSelectionné, 
            _coordonnéeXInséré,
            _coordonnéeYInséré,
            _libelléInséré,
            _adresseInséré,
            _sitewebInséré,
            _coordonnéeXModifié,
            _coordonnéeYModifié,
            _libelléModifié, 
            _adresseModifié,
            _sitewebModifié,
            _erreur;
        private ObservableCollection<String> _complexeString, _sportsString;
        private IEnumerable<Complexe> _complexes;
        private IEnumerable<Sport> _sports;
        private ComplexeDataAccess _complexeDataAccess;
        private SportDataAccess _sportDataAccess;

        public ICommand Ajouter => new RelayCommand(() => AjouterComplexe());
        public ICommand Modifier => new RelayCommand(() => ModifierComplexe());
        public ICommand Supprimer => new RelayCommand(() => SupprimerComplexe());

        public ObservableCollection<String> Sports
        {
            get => _sportsString;
            set
            {
                _sportsString = value;
                RaisePropertyChanged("Sports");
            }
        }
        public String SportChoisi
        {
            get => _sportSélectionné;
            set
            {
                _sportSélectionné = value;
                RaisePropertyChanged("SportChoisi");
            }
        }

        public ObservableCollection<String> Complexes
        {
            get => _complexeString;
            set
            {
                _complexeString = value;
                RaisePropertyChanged("Complexes");
            }
        }

        public String ComplexeChoisie
        {
            get => _complexeSelectionné;
            set
            {
                _complexeSelectionné = value;
                RaisePropertyChanged("ComplexeChoisie");
            }

        }

        public String InsertionCoordonneeX
        {
            get => _coordonnéeXInséré;
            set
            {
                _coordonnéeXInséré = value;
                RaisePropertyChanged("InsertionCooordonneeX");
            }
        }
        public String InsertionCoordonneeY
        {
            get => _coordonnéeYInséré;
            set
            {
                _coordonnéeYInséré = value;
                RaisePropertyChanged("InsertionCooordonneeY");
            }
        }
        public String InsertionLibelle
        {
            get => _libelléInséré;
            set
            {
                _libelléInséré = value;
                RaisePropertyChanged("InsertionLibelle");
            }
        }
        public String InsertionAdresse
        {
            get => _adresseInséré;
            set
            {
                _adresseInséré = value;
                RaisePropertyChanged("InsertionAdresse");
            }
        }
        public String InsertionSiteweb
        {
            get => _sitewebInséré;
            set
            {
                _sitewebInséré = value;
                RaisePropertyChanged("InsertionSiteweb");
            }
        }
        public String ModificationCoordonneeX
        {
            get => _coordonnéeXModifié;
            set
            {
                _coordonnéeXModifié = value;
                RaisePropertyChanged("ModificationCoordonneeX");
            }
        }
        public String ModificationCoordonneeY
        {
            get => _coordonnéeYModifié;
            set
            {
                _coordonnéeYModifié = value;
                RaisePropertyChanged("ModificationCoordonneeY");
            }
        }
        public String ModificationLibelle
        {
            get => _libelléModifié;
            set
            {
                _libelléModifié = value;
                RaisePropertyChanged("ModificationLibelle");
            }
        }
        public String ModificationAdresse
        {
            get => _adresseModifié;
            set
            {
                _adresseModifié = value;
                RaisePropertyChanged("ModificationAdresse");
            }
        }
        public String ModificationSiteweb
        {
            get => _sitewebModifié;
            set
            {
                _sitewebModifié = value;
                RaisePropertyChanged("ModificationSiteweb");
            }
        }

        public ComplexePageViewModel(INavigationService _navigation):base(_navigation)
        {
            _sportDataAccess = new SportDataAccess();
            _complexeDataAccess = new ComplexeDataAccess();
            InitializeSportAsync();
            InitializeComplexeAsync();
        }

        private async Task InitializeSportAsync()
        {
            _sports = await _sportDataAccess.Get();
            Sports = new ObservableCollection<string>();
            foreach (var sport in _sports)
            {
                Sports.Add(sport.Libellé);
            }
            Refresh();
        }

        private async Task InitializeComplexeAsync()
        {
            _complexes = await _complexeDataAccess.Get();
            Complexes = new ObservableCollection<string>();
            foreach (var complexe in _complexes)
            {
                Complexes.Add(complexe.Libellé);
            }
            Refresh();
        }

        private void AjouterComplexe()
        {
            Complexe valeurChamp = new Complexe();
            valeurChamp.CoordonnéeX = Double.Parse(InsertionCoordonneeX);
            valeurChamp.CoordonnéeY = Double.Parse(InsertionCoordonneeY);
            valeurChamp.Libellé = InsertionLibelle;
            valeurChamp.Adresse = InsertionAdresse;
            valeurChamp.SiteWeb = InsertionSiteweb;

            EnvoieRequete(Action.AJOUTER, valeurChamp);
        }

        private void ModifierComplexe()
        {
            Complexe valeurChamp = new Complexe();
            valeurChamp.CoordonnéeX = Double.Parse(ModificationCoordonneeX);
            valeurChamp.CoordonnéeY = Double.Parse(ModificationCoordonneeY);
            valeurChamp.Libellé = ModificationLibelle;
            valeurChamp.Adresse = ModificationAdresse;
            valeurChamp.SiteWeb = ModificationSiteweb;

            EnvoieRequete(Action.MODIFIER, valeurChamp);
        }

        private void SupprimerComplexe()
        {
            _complexeDataAccess.Delete(ComplexeChoisie);
            Complexes.Remove(ComplexeChoisie);
            ComplexeChoisie = null;
            Refresh();
        }

        public String Erreur
        {
            get => _erreur;
            set
            {
                _erreur = value;
                RaisePropertyChanged("Erreur");
            }
        }

        private void EnvoieRequete(Action action, Complexe valeurChamp)
        {

            if(valeurChamp != null && !Complexes.Contains(valeurChamp.Libellé) && Regex.IsMatch(valeurChamp.Libellé, @"^[a-zA-Z0-9]"))
            {
                switch (action)
                {
                    case Action.AJOUTER:
                        _complexeDataAccess.Post(valeurChamp);
                        Complexes.Add(valeurChamp.Libellé);
                        InsertionCoordonneeX = null;
                        InsertionCoordonneeY = null;
                        InsertionLibelle = null;
                        InsertionAdresse = null;
                        InsertionSiteweb = null;
                        break;
                    case Action.MODIFIER:
                        _complexeDataAccess.Put(valeurChamp, ComplexeChoisie);
                        int i = 0;
                        while (!Complexes[i].Equals(ComplexeChoisie))
                        {
                            i++;
                        }
                        Complexes[i] = valeurChamp.Libellé;
                        ModificationCoordonneeX = null;
                        ModificationCoordonneeY = null;
                        ModificationLibelle = null;
                        ModificationAdresse = null;
                        ModificationSiteweb = null;
                        break;
                }
                Erreur = null;
                ComplexeChoisie = null;
                Refresh();
            }
            else
            {
                Erreur = "Valeur entrée incorrecte\n(complexe déjà présent ?)";
            }
        }

        private void Refresh()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if(rootFrame != null)
            {
                rootFrame.Navigate(typeof(ComplexePage));
            }
        }
    }
}
