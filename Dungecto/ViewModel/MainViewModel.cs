using Dungecto.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Dungecto.Common;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Dungecto.ViewModel
{

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public Map Map { get; private set; }

        public ObservableCollection<Tile> Toolbox { get; private set; }

        private Tile _selectedTile;

        public Tile SelectedTile
        {
            get { return _selectedTile; }
            set 
            { 
                _selectedTile = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {   
            Toolbox = new ObservableCollection<Tile>();

            Map = new Map();

            Toolbox = Serializer.FromXml<ObservableCollection<Tile>>(@"Data\Toolbox.xml");

        }


        private ICommand _removeTileCommand;
        public ICommand RemoveTileCommand
        {
            get { return _removeTileCommand ?? (_removeTileCommand = new RelayCommand(RemoveTile)); }
        }
       
        private void RemoveTile()
        {
            if (SelectedTile == null) { return; }
            
            Map.Tiles.Remove(SelectedTile);
            SelectedTile = null;
        }


        private ICommand _createNewMapCommand;
        public ICommand CreateNewMapCommand
        {
            get { return _createNewMapCommand ?? (_createNewMapCommand = new RelayCommand(CreateMap)); }
        }

        private void CreateMap()
        {
            SelectedTile = null;
            Map.Tiles.Clear();

            Map.Columns = 10;
            Map.Rows = 10;

            Map.SectorHeight = 50;
            Map.SectorWidth = 50;
        }

        private ICommand _loadMapCommand;
        public ICommand LoadMapCommand
        {
            get { return _loadMapCommand ?? (_loadMapCommand = new RelayCommand(LoadMap)); }
        }

        private void LoadMap()
        {
            SelectedTile = null;

            var file = Dialogs.ShowOpenDialog("", ".xml");

            if (!string.IsNullOrEmpty(file))
            {
                Map = Serializer.FromXml<Map>(file);
                RaisePropertyChanged("Map");
            }

        }

        private ICommand _saveMapCommand;
        public ICommand SaveMapCommand
        {
            get { return _saveMapCommand ?? (_saveMapCommand = new RelayCommand(SaveMap)); }
        }

        private void SaveMap()
        {
            var file = Dialogs.ShowSaveDialog("", ".xml");

            if (!string.IsNullOrEmpty(file))
            {
                Serializer.ToXml<Map>(Map, file);
            }
        }



        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}
