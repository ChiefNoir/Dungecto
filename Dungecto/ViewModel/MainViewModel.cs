using Dungecto.Common;
using Dungecto.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Dungecto.ViewModel
{
    /// <summary> This class contains properties that the main View can data bind to </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary> See <see cref="CreateNewMapCommand"/> property </summary>
        private ICommand _createNewMapCommand;

        /// <summary> See <see cref="LoadMapCommand"/> property </summary>
        private ICommand _loadMapCommand;

        /// <summary> See <see cref="RemoveTileCommand"/> property </summary>
        private ICommand _removeTileCommand;

        /// <summary> See <see cref="SaveMapCommand"/> property </summary>
        private ICommand _saveMapCommand;

        /// <summary> See <see cref="SelectedTile"/> property </summary>
        private Tile _selectedTile;

        /// <summary> See <see cref="Map"/> property </summary>
        private Map _map;

        /// <summary> See <see cref="Toolbox"/> property </summary>
        private ObservableCollection<Tile> _toolbox;

        /// <summary> Create new map command </summary>        
        public ICommand CreateNewMapCommand
        {
            get { return _createNewMapCommand ?? (_createNewMapCommand = new RelayCommand(CreateMap)); }
        }

        /// <summary> Load <see cref="Map"/> from file </summary>
        public ICommand LoadMapCommand
        {
            get { return _loadMapCommand ?? (_loadMapCommand = new RelayCommand(LoadMap)); }
        }

        /// <summary> Get map </summary>
        public Map Map
        {
            get { return _map; }
            private set
            {
                _map = value;
                RaisePropertyChanged("Map");
            }
        }

        /// <summary> Get toolbox, contains map tiles </summary>
        public ObservableCollection<Tile> Toolbox 
        { 
            get { return _toolbox; } 
            private set 
            { 
                _toolbox = value; 
                RaisePropertyChanged("Toolbox"); 
            } 
        }

        /// <summary> Remove tile command. Removing <see cref="SelectedTile"/> from <see cref="Map"/></summary>
        public ICommand RemoveTileCommand
        {
            get { return _removeTileCommand ?? (_removeTileCommand = new RelayCommand(RemoveTile)); }
        }

        /// <summary> Save <see cref="Map"/> to file </summary>
        public ICommand SaveMapCommand
        {
            get { return _saveMapCommand ?? (_saveMapCommand = new RelayCommand(SaveMap)); }
        }

        /// <summary> Get/set selected tile from the <see cref="Map"/> </summary>
        public Tile SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                _selectedTile = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> Initializes a new instance of the MainViewModel class </summary>
        public MainViewModel()
        {
            _map = new Map();

            _toolbox = Serializer.FromXml<ObservableCollection<Tile>>(@"Data\Toolbox.xml");
        }

        /// <remarks> Clearing current <see cref="Map"/> and initializing it with default values </remarks>
        private void CreateMap()
        {
            SelectedTile = null;
            Map.Tiles.Clear();

            Map.Columns = 10;
            Map.Rows = 10;

            Map.SectorHeight = 50;
            Map.SectorWidth = 50;
        }

        /// <summary> Load <see cref="Map"/> from file </summary>
        private void LoadMap()
        {
            SelectedTile = null;

            //TODO: Not MVVM. fix it
            var file = Dialogs.ShowOpenDialog("", ".xml");

            if (!string.IsNullOrEmpty(file))
            {
                Map = Serializer.FromXml<Map>(file);
                RaisePropertyChanged("Map");
            }
        }

        /// <summary> Remove <see cref="SelectedTile"/> </summary>
        private void RemoveTile()
        {
            if (SelectedTile == null) { return; }

            Map.Tiles.Remove(SelectedTile);
            SelectedTile = null;
        }
        
        /// <summary> Save <see cref="Map"/> to file </summary>
        private void SaveMap()
        {
            //TODO: Not MVVM. fix it
            var file = Dialogs.ShowSaveDialog("", ".xml");

            if (!string.IsNullOrEmpty(file))
            {
                Serializer.ToXml<Map>(Map, file);
            }
        }

    }
}