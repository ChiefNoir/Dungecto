using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Linq;
using Dungecto.Common.Utils;

namespace Dungecto.Model
{
    /// <summary> Map </summary>
    [Serializable]
    public class Map : INotifyPropertyChanged
    {
        /// <summary> See <see cref="Columns"/> property </summary>
        private int _columns;

        /// <summary> See <see cref="Rows"/> property </summary>
        private int _rows;

        /// <summary> See <see cref="SectorHeight"/> property </summary>
        private int _sectorHeight;

        /// <summary> See <see cref="SectorWidth"/> property </summary>
        private int _sectorWidth;

        /// <summary> See <see cref="Background"/> property </summary>
        [NonSerialized]
        private Color _background = Colors.LightBlue;

        /// <summary> See <see cref="Tiles"/> property </summary>
        private ObservableCollection<Tile> _tiles;

        /// <summary>Coordinates of the left-top corner of the each grid square in format: position, [column number, row number] </summary>
        readonly Dictionary<Point, int[]> _gridCoordinates = new Dictionary<Point, int[]>();

        /// <summary> Create map </summary>
        public Map()
        {
            _tiles = new ObservableCollection<Tile>();
            _sectorWidth = 50;
            _sectorHeight = 50;
            _rows = 5;
            _columns = 10;
        }

        /// <summary> Property changed event</summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary> Get/set map's columns, in sectors (Width) </summary>
        [XmlAttribute("Columns")]
        public int Columns
        {
            get { return _columns; }
            set
            {
                if (_columns == value) { return; }

                _columns = value;
                RaisePropertyChanged("Columns");
                RaisePropertyChanged("Width");

                RemoveOutOfRangeFillers();
            }
        }

        /// <summary> Get/set map's background color</summary>
        [XmlElement("Background")]
        public Color Background
        {
            get { return _background; }
            set { _background = value; RaisePropertyChanged("Background"); }
        }

        /// <summary> Get/set map's rows, in sectors (Height) </summary>
        [XmlAttribute("Rows")]
        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows == value) { return; }

                _rows = value;
                RaisePropertyChanged("Rows");
                RaisePropertyChanged("Height");

                RemoveOutOfRangeFillers();
            }
        }

        /// <summary> Get/set height of map's sector </summary>
        [XmlAttribute("SectorHeight")]
        public int SectorHeight
        {
            get { return _sectorHeight; }
            set
            {
                if (_sectorHeight == value) { return; }

                _sectorHeight = value;
                RaisePropertyChanged("SectorHeight");
                RaisePropertyChanged("Height");

                InitGridCoordinates();
                foreach (var item in Tiles.Where(x => x.IsFiller))
                {
                    item.Height = value;

                    var ss = _gridCoordinates.FirstOrDefault(x => x.Value[1] == item.YIndex);
                    item.Y = (int)ss.Key.Y;
                }

                RemoveOutOfRangeFillers();
            }
        }

        /// <summary> Get/set width of map's sector </summary>
        [XmlAttribute("SectorWidth")]
        public int SectorWidth
        {
            get { return _sectorWidth; }
            set
            {
                if (_sectorWidth == value) { return; }

                _sectorWidth = value;
                RaisePropertyChanged("SectorWidth");
                RaisePropertyChanged("Width");

                InitGridCoordinates();
                foreach (var item in Tiles.Where(x=>x.IsFiller))
                {
                    item.Width = value;

                    var ss = _gridCoordinates.FirstOrDefault(x => x.Value[0] == item.XIndex);
                    item.X = (int)ss.Key.X;
                }

                RemoveOutOfRangeFillers();
            }
        }

        /// <summary> Get total map width (in pixels)</summary>
        public int Width
        {
            get
            {
                return Columns * SectorWidth;
            }
        }

        /// <summary> Get total map height (in pixels) </summary>
        public int Height
        {
            get
            {
                return Rows * SectorHeight;
            }
        }

        /// <summary> Get/set map's tiles </summary>
        [XmlElement("Tiles")]
        public ObservableCollection<Tile> Tiles
        { 
            get { return _tiles; } 
            private set 
            { 
                _tiles = value; 
                RaisePropertyChanged("Tiles"); 
            } 
        }

        /// <summary> On property changed </summary>
        /// <param name="propertyName">Property name</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>Remove fillers placed out of the map borders</summary>
        private void RemoveOutOfRangeFillers()
        {
            Tiles.Remove(ti => (ti.X >= Width) || (ti.Y >= Height));
        }

        /// <summary>Get filler color</summary>
        /// <param name="point">Approximate filler position</param>
        /// <returns>Filler color or null if there is no filler at the position</returns>
        public string GetFillerColor(Point point)
        {
            var poi = FindFillerPlace(point);
            if (poi == null) { return null;}

            var filler = Tiles.FirstOrDefault(x => x.IsFiller && x.X == poi.Value.X && x.Y == poi.Value.Y);

            if (filler != null) { return filler.Color; }

            return null;
        }

        /// <summary>Add filler to the map</summary>
        /// <param name="point">Approximate filler position (binded to the grid)</param>
        /// <param name="color">Filler color</param>
        public void AddFiller(Point point, string color)
        {
            InitGridCoordinates();

            var poi = FindFillerPlace(point);
            if (poi == null) { return; }


            var filler = Tiles.FirstOrDefault(x => x.IsFiller && x.X == poi.Value.X && x.Y == poi.Value.Y);
            if (filler != null)
            {
                filler.Color = color;
            }
            else
            {
                Tiles.Add
                    (new Tile 
                    { 
                        Color = color, 
                        IsFiller = true, 
                        Height = SectorHeight, 
                        Width = SectorWidth, 
                        XIndex = _gridCoordinates[poi.Value][0],
                        YIndex = _gridCoordinates[poi.Value][1],
                        Geometry = "M0,0 H5 V5 H0 Z",
                        X = Convert.ToInt32(poi.Value.X),
                        Y = Convert.ToInt32(poi.Value.Y),
                        Z = -100
                    }
                    );
            }

        }

        /// <summary>Init grid points</summary>
        /// <remarks>Coordinates of the left-top corner of the each grid square</remarks>
        private void InitGridCoordinates()
        {
            _gridCoordinates.Clear();
            for (var i = 0; i < Columns; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    _gridCoordinates.Add(new Point(i * (SectorWidth), j * (SectorHeight)), new[]{i, j});
                }
            }
        }

        /// <summary>Find precise (bind to the grid) position for filler</summary>
        /// <param name="point">Approximate filler position</param>
        /// <returns>Filler position (bind to the grid)</returns>
        private Point? FindFillerPlace(Point point)
        {            
            var positions = _gridCoordinates.Keys.Where(poi => poi.X <= point.X && poi.Y <= point.Y);

            if (positions.Any())
            {
                return positions.Last();
            }

            return null;
        }

        /// <summary>Remove filler from the map</summary>
        /// <param name="point">Approximate filler position</param>
        public void RemoveFiller(Point point)
        {
            if (!Tiles.Any(x => x.IsFiller)) { return; }
            
            var poi = FindFillerPlace(point);
            if (poi == null) { return;}

            Tiles.Remove(Tiles.FirstOrDefault(x => x.IsFiller && x.X == poi.Value.X && x.Y == poi.Value.Y));
        }
    }
}
