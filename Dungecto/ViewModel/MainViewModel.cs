﻿using Dungecto.Common;
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

        /// <summary> See <see cref="SaveMapCommand"/> property </summary>
        private ICommand _saveMapAsCommand;

        /// <summary> See <see cref="DeselectCommand"/> property </summary>
        private ICommand _deselectCommand;

        /// <summary> See <see cref="CopyCommand"/> property </summary>
        private ICommand _copyCommand;

        /// <summary> See <see cref="CutCommand"/> property </summary>
        private ICommand _cutCommand;

        /// <summary> See <see cref="PasteCommand"/> property </summary>
        private ICommand _pasteCommand;

        /// <summary> See <see cref="SelectedTile"/> property </summary>
        private Tile _selectedTile;

        /// <summary> See <see cref="Map"/> property </summary>
        private Map _map;

        /// <summary> Clipboard for Tile </summary>
        private Tile _clipboardTile = null;

        /// <summary> Last file path (last opened/last saved)</summary>
        private string _lastFilePath = null;

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

        /// <summary> Remove tile command. Removing <see cref="SelectedTile"/> from <see cref="Map"/></summary>
        public ICommand RemoveTileCommand
        {
            get { return _removeTileCommand ?? (_removeTileCommand = new RelayCommand(RemoveTile)); }
        }

        /// <summary> Save <see cref="Map"/> to file </summary>
        public ICommand SaveMapCommand
        {
            get { return _saveMapCommand ?? (_saveMapCommand = new RelayCommand(Save)); }
        }

        /// <summary> Save <see cref="Map"/> to file </summary>
        public ICommand SaveMapAsCommand
        {
            get { return _saveMapAsCommand ?? (_saveMapAsCommand = new RelayCommand(SaveAs)); }
        }

        /// <summary> Make <see cref="SelectedTile"/> null </summary>
        public ICommand DeselectCommand
        {
            get { return _deselectCommand ?? (_deselectCommand = new RelayCommand(Deselect)); }
        }

        /// <summary> Copy tile to local "clipboard" </summary>
        public ICommand CopyCommand
        {
            get { return _copyCommand ?? (_copyCommand = new RelayCommand(Copy)); }
        }

        /// <summary> Cut tile to local "clipboard" </summary>
        public ICommand CutCommand
        {
            get { return _cutCommand ?? (_cutCommand = new RelayCommand(Cut)); }
        }

        /// <summary> Paste tile from local "clipboard" to map </summary>
        public ICommand PasteCommand
        {
            get { return _pasteCommand ?? (_pasteCommand = new RelayCommand(Paste)); }
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
            _lastFilePath = null;
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
                _map = Serializer.FromXml<Map>(file);
                RaisePropertyChanged("Map");
                _lastFilePath = file;
            }
        }

        /// <summary> Remove <see cref="SelectedTile"/> </summary>
        private void RemoveTile()
        {
            if (SelectedTile == null) { return; }

            Map.Tiles.Remove(SelectedTile);
            SelectedTile = null;
        }

        /// <summary> Save <see cref="Map"/> to <see cref="_lastFilePath"/> or call <see cref="SaveAs"/> </summary>
        private void Save()
        {
            if(_lastFilePath !=null)
            {
                Serializer.ToXml<Map>(Map, _lastFilePath);
            }
            else
            {
                SaveAs();
            }
        }

        /// <summary>Call Win32 savefile dialog and save <see cref="Map"/> to file </summary>
        private void SaveAs()
        {
            //TODO: Not MVVM. fix it
            var filePath = Dialogs.ShowSaveDialog("", ".xml");

            if (!string.IsNullOrEmpty(filePath))
            {
                Serializer.ToXml<Map>(Map, filePath);
                _lastFilePath = filePath;
            }
        }

        /// <summary> Insert <see cref="_clipboardTile"/> to <see cref="Map"/> </summary>
        private void Paste()
        {
            if (_clipboardTile != null)
            {
                _clipboardTile.X = 40;
                _clipboardTile.Y = 40;
                Map.Tiles.Add(_clipboardTile);
                _clipboardTile = null;
            }
        }

        /// <summary> Clone <see cref="SelectedTile"/> to local clipboard (<see cref="_clipboardTile"/>) </summary>
        private void Copy()
        {
            if (SelectedTile != null)
            {
                _clipboardTile = SelectedTile.Clone() as Tile;
            }
        }

        /// <summary> Cut <see cref="SelectedTile"/> to local clipboard (<see cref="_clipboardTile"/>) and remove it from <see cref="Map"/> </summary>
        private void Cut()
        {
            if (SelectedTile != null)
            {
                _clipboardTile = SelectedTile;
                Map.Tiles.Remove(SelectedTile);
                SelectedTile = null;
            }
        }

        /// <summary> Make <see cref="SelectedTile"/> null </summary>
        private void Deselect()
        {
            SelectedTile = null;
        }
    }
}