using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Dungecto.Model
{
    /// <summary> Map tile description </summary>
    [Serializable]
    public class Tile : INotifyPropertyChanged, ICloneable
    {
        private string _color;
        private string _geometry;

        private int _height;
        private int _width;
        private int _x;
        private int _y;
        private int _z;


        /// <summary> Property changed event</summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary> Get/set color </summary>
        [XmlAttribute("Color")]
        public string Color
        {
            get { return _color; }
            set
            {
                if (_color == value) { return; }

                _color = value;
                RaisePropertyChanged("Color");
            }
        }

        /// <summary> Get/set geometry path markup </summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/ms752293%28v=vs.110%29.aspx</remarks>
        [XmlElement("Geometry")]
        public string Geometry
        {
            get { return _geometry; }
            set
            {
                if (_geometry == value) { return; }

                _geometry = value;
                RaisePropertyChanged("Geometry");
            }
        }

        /// <summary> Get/set height (OY-size) </summary>
        [XmlAttribute("Height")]
        public int Height
        {
            get { return _height; }
            set
            {
                if (_height == value) { return; }

                _height = value;
                RaisePropertyChanged("Height");
            }
        }

        /// <summary> Get/set width (OX-size) </summary>
        [XmlAttribute("Width")]
        public int Width
        {
            get { return _width; }
            set
            {
                if (_width == value) { return; }

                _width = value;
                RaisePropertyChanged("Width");
            }
        }

        /// <summary> Get/set X position </summary>
        [XmlAttribute("X")]
        public int X
        {
            get { return _x; }
            set
            {
                if (_x == value) { return;}

                _x = value;
                RaisePropertyChanged("X");
            }
        }

        public int xIndex { get; set; }
        public int yIndex { get; set; }

        /// <summary> Get/set Y position </summary>
        [XmlAttribute("Y")]
        public int Y
        {
            get { return _y; }
            set
            {
                if (_y == value) { return; }

                _y = value;
                RaisePropertyChanged("Y");
            }
        }

        /// <summary> Get/set Z position </summary>
        [XmlAttribute("Z")]
        public int Z
        {
            get { return _z; }
            set
            {
                _z = value;
                RaisePropertyChanged("Z");
            }
        }

        /// <summary> Is tile a static square? </summary>
        [XmlAttribute("IsFiller")]
        public bool IsFiller { get; set; }

        /// <summary>Clone tile description</summary>
        /// <returns>Clone of this</returns>
        public object Clone()
        {
            return new Tile
                       {
                           _color = Color,
                           _geometry = Geometry,
                           _height = Height,
                           _width = Width,
                           _x = X,
                           _y = Y,
                           IsFiller = IsFiller
                       };
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
    }
}