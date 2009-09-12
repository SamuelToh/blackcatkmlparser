using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BlackCat
{
    public class GeoModel : IGeoModel
    {
        private const string KML_NAMESPACE_ADDR = "http://www.opengis.net/kml/2.2";
        public const string BLUE_CODE = "#0000FF";
        public const string GREEN_CODE = "#00FF00";
        public const string RED_CODE = "FF0000";
        public const string YELLOW_CODE = "FFFF00";

        private List<Style> styles = new List<Style>();
        private List<Region> regions = new List<Region>();

        public class Region
        {
            //Constructor
            public Region(string regionName,
                    string coordinates)
            {
                this.regionName = regionName;
                this.coordinates = coordinates;
            }

            //Properties 
            public string regionName
            {
                get { return this.regionName; }
                set { this.regionName = value; }
            }
            public string coordinates
            {
                get { return this.coordinates; }
                set { this.coordinates = value; }
            }

            public Style regionStyle
            {
                get { return this.regionStyle; }
                set { this.regionStyle = value; }
            }
        }

        public void BuildGeoModel(string kmlURL, ProgressBar bar)
        {
            bool endOfFile = false;

            XmlTextReader reader = getReader(kmlURL);

            while (reader.Read() && !endOfFile)
            {
                //Get the curr node's tag name
                string tagName = reader.Name
                                              .ToLower();


                switch (tagName)
                {
                    case "placemark":
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                                buildRegion(reader);

                            break;
                        }
                    case "style":
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                                buildStyle(reader);

                            break;
                        }
                    case "kml":
                        {
                            if (reader.NodeType == XmlNodeType.EndElement)
                                endOfFile = true;

                            break;
                        }
                }//End Switch

            }//End While
        }

        public void BuildGeoModel(string midURL, string mifURL, ProgressBar bar)
        {
            //Will add in
        }

        private void buildRegion(XmlTextReader reader)
        {
            string regionName = "",
                             coord = "";
            reader.Read();

            while (reader.Name.ToLower() != "placemark" &&
                       reader.NodeType != XmlNodeType.EndElement)
            {

                //Search for <name> [Item name] </name>
                if (reader.Name.
                            ToLower() == "name")

                    regionName = reader.ReadContentAsString();


                //Search for <polygon> 
                else if (reader.Name.
                                 ToLower() == "polygon")

                    coord += extractCoord(reader);
                //break;


                reader.Skip();

            }


            this.regions.Add
                (new Region(regionName, coord));

        }

        private string extractCoord(XmlTextReader reader)
        {

            while (reader.Read())
            {

                if (reader.Name.ToLower() == "polygon"
                        && reader.NodeType == XmlNodeType.EndElement)

                    break;


                if (reader.Name.
                           ToLower() == "coordinates")

                    return reader.ReadString();

            }

            return "";

        }

        private void buildStyle(XmlTextReader reader)
        {
            string styleId = "",
                             colorCode = "";

            styleId = reader.GetAttribute("id");


            while (reader.Read())


                if (reader.Name.ToLower() == "style"
                          && reader.NodeType == XmlNodeType.EndElement)

                    break;

                else if (reader.Name.
                            ToLower() == "polystyle")
                {
                    colorCode = extractColor(reader);
                    break; //we found what we want so we break out of loop
                }

            this.styles.Add(new Style(colorCode, styleId));

        }

        private string extractColor(XmlTextReader reader)
        {
            string defaultCode = "#000000"; //default color 

            while (reader.Read())
            {
                //Incase we couldnt find color tag we break off the loop
                if (reader.Name.ToLower() == "polystyle"
                            && reader.NodeType == XmlNodeType.EndElement)

                    break;


                if (reader.Name.
                           ToLower() == "color")

                    return reader.ReadString();

            }

            return defaultCode;
        }

        #region Output KML Codes

        public void outputKML(string outputPath, ProgressBar bar)
        {
            XmlTextWriter writer = this.getWriter
                                        (outputPath);

            writeKMLHeader(writer);

            writeKMLStyles(writer);

            writeKMLRegion(writer);

            writeKMLFooter(writer);
        }

        private void writeKMLHeader(XmlTextWriter writer)
        {
            writer.WriteStartElement("kml", KML_NAMESPACE_ADDR);

            writer.WriteStartElement("Document");
            writer.WriteStartElement("name");
            writer.WriteString("BlackCat KML generated file");
            writer.WriteEndElement();

            //Write an open folder icon
            writer.WriteStartElement("open");
            writer.WriteString("1");
            writer.WriteEndElement();
        }

        private void writeKMLStyles(XmlTextWriter writer)
        {
            for (int i = 0; i < this.styles.Count; i++)
            {
                writer.WriteStartElement("Style");

                //<style id = ??>
                writer.WriteAttributeString("id",
                                this.styles[i].styleName);

                writer.WriteStartElement("Polystyle");

                writer.WriteStartElement("Color");
                writer.WriteString(this.styles[i].colorCode);
                writer.WriteEndElement(); //</color>

                writer.WriteEndElement(); //</polystyle>

                writer.WriteEndElement(); //</style>

                writer.Flush();
            }
        }

        private void writeKMLRegion(XmlTextWriter writer)
        {

            writer.WriteStartElement("Folder");

            for (int i = 0; i < this.regions.Count; i++)
            {
                writer.WriteStartElement("Placemark");

                writer.WriteStartElement("name");
                writer.WriteString(this.regions[i].regionName);
                writer.WriteEndElement(); //</name>
                writer.WriteStartElement("visibility"); //indicating the style
                writer.WriteString("0"); //show the polygon
                writer.WriteEndElement(); //</visibility>
                writer.WriteStartElement("styleUrl"); //indicating the style
                writer.WriteString(this.regions[i].
                                        regionStyle.styleName); //show the polygon
                writer.WriteEndElement(); //</styleUrl>


                writer.WriteStartElement("Polystyle");
                writer.WriteStartElement("tessellate"); //indicating the style
                writer.WriteString("1");
                writer.WriteEndElement(); //</tessellate>
                writer.WriteStartElement("altitudeMode"); //indicating the style
                writer.WriteString("absolute"); //show the polygon
                writer.WriteEndElement(); //</styleUrl>

                writer.WriteStartElement("outerBoundaryIs"); //indicating the style
                writer.WriteStartElement("LinearRing"); //indicating the style

                writer.WriteStartElement("coordinates");
                writer.WriteString(this.regions[i].coordinates);
                writer.WriteEndElement(); //</coordinates>
                writer.WriteEndElement(); //</LinearRing>
                writer.WriteEndElement(); //</outerBoundaryIs>
                writer.WriteEndElement(); //</polystyle>


                writer.WriteEndElement(); //</placemark>
                writer.Flush();
            }

            writer.WriteEndElement(); //</Folder>
        }

        private void writeKMLFooter(XmlTextWriter writer)
        {
            writer.WriteEndElement(); //</document>
            writer.WriteEndElement(); //</kml>

            writer.WriteEndDocument(); //commit write
            writer.Close(); //close writer
        }

        #endregion


        public String[] GetRegionIdentifiers()
        {
            String[] names = new String[regions.Count];
            for (int i = 0; i < regions.Count; i++)
            {
                names[i] = regions[i].regionName;
            }
            return names;
        }

        public void setRegionStyle(int regIndex, Style style)
        {
            this.regions[regIndex].regionStyle = style;
        }

        private XmlTextWriter getWriter(String outputPath)
        {
            string filename = "BlackCatKML_"
                                + DateTime.Now.ToString("yyyy.MM.dd")
                                + ".kml";

            XmlTextWriter writer = new
                                XmlTextWriter(outputPath, null);

            writer.WriteStartDocument(); //Open document

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            return writer;
        }

        private XmlTextReader getReader(String fileURL)
        {
            return new XmlTextReader(fileURL);
        }
    }
}
