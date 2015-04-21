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
        private Color _background = Colors.LightBlue;

        /// <summary> See <see cref="Tiles"/> property </summary>
        private ObservableCollection<Tile> _tiles;

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

                ResizeFillers();
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

                ResizeFillers();
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

                var oldV = _sectorHeight;
                _sectorHeight = value;
                RaisePropertyChanged("SectorHeight");
                RaisePropertyChanged("Height");

                InitHas();
                foreach (var item in Tiles.Where(x => x.IsFiller))
                {
                    item.Height = value;

                    var ss = _points.FirstOrDefault(x => x.Value[1] == item.yIndex);
                    item.Y = (int)ss.Key.Y;
                }

                ResizeFillers();
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

                var oldV = _sectorWidth;

                _sectorWidth = value;
                RaisePropertyChanged("SectorWidth");
                RaisePropertyChanged("Width");

                InitHas();
                foreach (var item in Tiles.Where(x=>x.IsFiller))
                {
                    item.Width = value;

                    var ss = _points.FirstOrDefault(x => x.Value[0] == item.xIndex);
                    item.X = (int)ss.Key.X;
                }

                ResizeFillers();
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

        public void ResizeFillers()
        {
            var ft = Tiles.Where(ti => (ti.X>= Width) || (ti.Y>= Height));

            Tiles.Remove(ti => (ti.X >= Width) || (ti.Y >= Height));

        }

        public string GetFillerColor(Point point)
        {
            var poi = FindFillerPlace(point);

            var filler = Tiles.FirstOrDefault(x => x.IsFiller && x.X == poi.X && x.Y == poi.Y);
            if (filler != null)
            {
                return filler.Color;
            }

            return null;
        }

        public void AddFiller(Point point, string color)
        {
            InitHas();
            var poi = FindFillerPlace(point);

            var filler = Tiles.FirstOrDefault(x => x.IsFiller && x.X == poi.X && x.Y == poi.Y);
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
                        xIndex = _points[poi][0],
                        yIndex = _points[poi][1],
                        Geometry = "M0,0 H5 V5 H0 Z", 
                        X = Convert.ToInt32(poi.X), Y = Convert.ToInt32(poi.Y), Z = -100 }
                    );
            }

        }

        public void InitHas()
        {
            _points.Clear();
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    _points.Add(new Point(i * (SectorWidth), j * (SectorHeight)), new[]{i, j});
                }
            }
        }

        Dictionary<Point, int[]> _points = new Dictionary<Point, int[]>();

        private Point FindFillerPlace(Point point)
        {            
            var ss = _points.Keys.Where(poi => poi.X <= point.X && poi.Y <= point.Y);
            if (ss.Any())
            {
                return ss.Last();
            }
            return new Point(-100, -100);//TODO: -100;-100?! Make it "null" or smthg like this.
        }

        public void RemoveFiller(Point point)
        {
            if (!Tiles.Any(x => x.IsFiller)) { return; }
            
            var poi = FindFillerPlace(point);
            Tiles.Remove(Tiles.FirstOrDefault(x => x.IsFiller && x.X == poi.X && x.Y == poi.Y));
        }
    }
}
