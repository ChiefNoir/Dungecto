using System;
using System.Xml.Serialization;

namespace Dungecto.Model
{
    /// <summary> Map tile description </summary>
    [Serializable]
    public class TileDescription
    {
        /// <summary> Path markup </summary>
        /// <remarks>https://msdn.microsoft.com/en-us/library/ms752293%28v=vs.110%29.aspx</remarks>
        [XmlElement("Geometry")]
        public string GeometryPath { get; set; }

        /// <summary> Tile height (OY-size) </summary>
        [XmlAttribute("Height")]
        public double Height { get; set; }

        /// <summary> Color </summary>
        [XmlAttribute("HexColor")]
        public string HexColor { get; set; }

        /// <summary> Tile width (OX-size) </summary>
        [XmlAttribute("Width")]
        public double Width { get; set; }
    }
}
