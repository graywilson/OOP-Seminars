using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OOP_Seminars.Models
{
    [XmlRoot("ForestArea")]
    public class ForestAreaModel
    {
        [XmlArray("VertexCoordinates")]
        [XmlArrayItem("Vertex")]
        public List<VertexModel> VertexCoordinates { get; set; }

        [XmlArray("Trees")]
        [XmlArrayItem("Tree")]
        public List<TreeModel> Trees { get; set; }
    }

    public class VertexModel
    {
        [XmlElement("Latitude")]
        public double Latitude { get; set; }

        [XmlElement("Longitude")]
        public double Longitude { get; set; }
    }

    public class TreeModel
    {
        [XmlElement("Latitude")]
        public double Latitude { get; set; }

        [XmlElement("Longitude")]
        public double Longitude { get; set; }

        [XmlElement("Diameter")]
        public string Diameter { get; set; }

        [XmlElement("Species")]
        public string Species { get; set; }
    }
}
