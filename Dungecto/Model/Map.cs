using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dungecto.Model
{
    /// <summary> Map desciption </summary>
    public class Map
    {
        /// <summary> Get/set numbers of columns </summary>
        [XmlAttribute("Columns")]
        public int Columns { get; set; }

        /// <summary> Get/set numbers of rows </summary>
        [XmlAttribute("Rows")]
        public int Rows { get; set; }

        /// <summary> Get/set sector height </summary>
        [XmlAttribute("SectorHeight")]
        public int SectorHeight { get; set; }

        /// <summary> Get/set sector width </summary>
        [XmlAttribute("SectorWidth")]
        public int SectorWidth { get; set; }

        /// <summary> Get/set map tiles </summary>
        [XmlElement("Tiles")]
        public List<Tile> Tiles { get; set; }

        /// <summary>Create map description</summary>
        public Map()
        {
            Tiles = new List<Tile>();
        }
    }
}
