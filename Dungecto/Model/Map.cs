using System;
using System.ComponentModel;

namespace Dungecto.Model
{
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    /// <summary> Map </summary>
    [Serializable]
    public class Map : INotifyPropertyChanged
    {
        /// <summary> <see cref="Columns"/> property </summary>
        private int _columns;

        /// <summary> <see cref="Rows"/> property </summary>
        private int _rows;

        /// <summary> <see cref="SectorHeight"/> property </summary>
        private int _sectorHeight;

        /// <summary> <see cref="SectorWidth"/> property </summary>
        private int _sectorWidth;

        /// <summary> Create map </summary>
        public Map()
        {
            Tiles = new ObservableCollection<Tile>();
            SectorWidth = 50;
            SectorHeight = 50;
            Rows = 5;
            Columns = 10;
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
            }
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
            }
        }

        /// <summary> Get/set width of map's sector </summary>
        [XmlAttribute("SectorWidth")]
        public int SectorWidth
        {
            get { return _sectorWidth;}
            set
            {
                if (_sectorWidth == value) { return; }

                _sectorWidth = value;
                RaisePropertyChanged("SectorWidth");
            }
        }
        
        /// <summary> Get/set map's tiles </summary>
        [XmlElement("Tiles")]
        public ObservableCollection<Tile> Tiles { get; set; }

        /// <summary> On property changed </summary>
        /// <param name="propertyName">Property name</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
