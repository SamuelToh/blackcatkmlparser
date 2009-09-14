using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;

namespace BlackCat
{
    public partial class GeoModel : IGeoModel
    {

        ILog log = LogManager.GetLogger(typeof(GeoModel));

        private const string KML_NAMESPACE_ADDR = "http://www.opengis.net/kml/2.2";
        public const string BLUE_CODE = "7dff0000";
        public const string GREEN_CODE = "7d00ff00";
        public const string RED_CODE = "7d0000ff";
        public const string YELLOW_CODE = "7d00ffff";
        public const string POLYGON_CODE = "POLYGON";
        public const string PLINE_CODE = "PLINE";
        public const string LINE_CODE = "LINE";
        public const string POINT_CODE = "POINT";
        const string RAW_INDENTATION = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";

        private List<Style> styles = new List<Style>();
        private List<Region> regions = new List<Region>();
        private List<string> dataFields = new List<string>();

        public class Region
        {
            public List<string> coordinates
                                    = new List<string>();
            private string
                            m_regName,
                            m_regType;

            private Style m_regStyle;

            //Constructor
            public Region(string regionName,
                    string coordinates,
                    string regType)
            {
                this.regionName = regionName;
                this.coordinates.Add(coordinates);
                this.regionType = regionType;
            }
            public Region(string regionName,
                    string coordinates)
            {
                this.regionName = regionName;
                this.coordinates.Add(coordinates);
            }

            public Region(string regionType)
            {
                this.regionType = regionType;
            }

            public Region() { }

            //Properties 
            public string regionName
            {
                get { return m_regName; }
                set { m_regName = value; }
            }


            public Style regionStyle
            {
                get { return m_regStyle; }
                set { m_regStyle = value; }
            }

            public string regionType
            {
                get { return m_regType; }
                set { m_regType = value; }
            }
        }

        public bool BuildGeoModel(string kmlFileURL, ProgressBar bar)
        {
            bool endOfFile = false;

            XmlTextReader reader = getReader(kmlFileURL);
            try
            {
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
            catch
            {
                return false;
            }

            return true;
        }

        /*public bool BuildGeoModel
                (String midFileURL, String mifFileURL, ProgressBar bar)
        {
            string line = "";
            StreamReader mifReader = this.getReader(mifFileURL, true);
            try
            {
                //Data Section
                while ((line = mifReader.ReadLine()) != null)
                {
                    if (line.ToUpper().StartsWith("DATA"))

                        ReadRegion(mifReader);

                    else if
                        (line.ToUpper().StartsWith("COLUMNS"))

                        populateDataFields(mifReader, line);

                }
            }
            catch
            {
                return false;
            }
            mifReader.Close();
            return true;
        }*/

        //14September
        private void populateDataFields
                (StreamReader mifReader, string data)
        {
            string[] columnInfo = data.Split(' ');

            if (columnInfo.Length > 1)
            {
                int columns =
                        Convert.ToInt16(columnInfo[1]);

                for (int i = 0; i < columns; i++)
                {
                    string[] columnNames = mifReader.ReadLine()
                                                    .ToString()
                                                    .TrimStart()
                                                    .Split(' ');

                    //Take in first identifier, we dont want data type ident
                    this.dataFields.Add(columnNames[0]);
                }
            }
        }

        private void ReadRegion(StreamReader mifReader)
        {
            String line;
            String[] lineParts;

            while ((line = mifReader.ReadLine()) != null)
            {
                lineParts = line.Split(' ');

                if (lineParts.Length > 1)
                {
                    //string test = lineParts[0].ToString().ToUpper();
                    switch (lineParts[0].ToString().ToUpper())
                    {
                        case "REGION": //also known as polygon
                            {
                                ReadPolygon(mifReader,
                                    int.Parse(lineParts[lineParts.Length - 1]));

                                break;
                            }
                        case "PLINE":
                            {
                                ReadPLINE(mifReader,
                                     int.Parse(lineParts[lineParts.Length - 1]));

                                break;
                            }
                        case "LINE":
                            {
                                ReadLine(lineParts);

                                break;
                            }
                        case "POINT":
                            {
                                ReadPoint(mifReader, lineParts);

                                break;
                            }
                    };
                }
            }
        }

        private void ReadPolygon(StreamReader mifReader, int polyCount)
        {
            string coord = "";
            string temp = "";
            Region reg = new Region(POLYGON_CODE);

            Regex brushPattern = new Regex("Brush");
            Regex penPattern = new Regex("Pen");

            for (int i = 0; i < polyCount; i++)
            {
                string debug = mifReader.ReadLine();
                int coordCount = Convert.ToInt32(debug);

                for (int x = 0; x < coordCount; x++)
                {
                    temp = mifReader.ReadLine().
                                 Replace(' ', ',') + ",0 \n"
                                 + RAW_INDENTATION;

                    if (!brushPattern.IsMatch(temp) ||
                           !penPattern.IsMatch(temp))

                        coord += temp;

                }

                reg.coordinates.Add(coord);
                coord = ""; //reset string
            }

            this.regions.Add(reg);
        }

        private void ReadPLINE(StreamReader mifReader, int plineCount)
        {
            string coord = "\n" + RAW_INDENTATION;
            mifReader.ReadLine();

            Region reg = new Region(PLINE_CODE);

            for (int i = 0; i < plineCount; i++)
            {
                //Not last item
                if (i != plineCount - 1)
                    coord += mifReader.ReadLine().
                            Replace(' ', ',') + " \n"
                            + RAW_INDENTATION;
                //else last item we ignore the pen object   
            }

            reg.coordinates.Add(coord);

            this.regions.Add
                    (reg);

        }

        private void ReadLine(string[] lineData)
        {
            string coord = "\n" + RAW_INDENTATION;

            Region reg = new Region(LINE_CODE);

            for (int i = 1; i < lineData.Length; i++)
                if (i % 2 == 0)
                    coord += lineData[i]
                                + "\n" + RAW_INDENTATION;
                else
                    coord += lineData[i] + ",";


            coord = coord.Substring(0,
                            coord.Length - 1); //remove \n and extra .
            // + "\n"
            // + RAW_INDENTATION; 

            reg.coordinates.Add(coord);

            this.regions.Add
                    (reg);

        }

        private void ReadPoint
            (StreamReader mifReader, string[] lineData)
        {
            mifReader.ReadLine(); //Reads Symbol data and ignore it;
            string coord = "";

            Region reg = new Region(POINT_CODE);

            for (int i = 1; i < lineData.Length; i++)
                if (i % 2 == 0)
                    coord += lineData[i]
                                + "\n" + RAW_INDENTATION;
                else
                    coord += lineData[i] + ",";


            coord = coord.Substring(0,
                            coord.Length - 1); //remove \n and extra .
            // + "\n"
            // + RAW_INDENTATION; 

            reg.coordinates.Add(coord);

            this.regions.Add
                    (reg);
        }

        private void buildRegion(XmlTextReader reader)
        {
            string regionName = "",
                             regType = "";
            Region r = new Region();
            reader.Read();

            while (reader.Name.ToLower() != "placemark" &&
                       reader.NodeType != XmlNodeType.EndElement)
            {

                //Search for <name> [Item name] </name>
                if (reader.Name.
                            ToLower() == "name")

                    r.regionName
                        = reader.ReadString();


                //Search for <polygon> 
                else if (reader.Name.
                                 ToLower() == "polygon")
                {
                    r.regionType = POLYGON_CODE;
                    r.coordinates = extractCoord(reader);
                }
                else if (reader.Name.
                                 ToLower() == "linestring") //or pline
                {
                    r.regionType = LINE_CODE;
                    r.coordinates = extractCoord(reader);
                }
                else if (reader.Name.
                                 ToLower() == "point")
                {
                    r.regionType = POINT_CODE;
                    r.coordinates = extractCoord(reader);
                }

                //break;


                reader.Skip();

            }


            this.regions.Add(r);

        }

        private List<string> extractCoord(XmlTextReader reader)
        {
            List<string> coord = new List<string>();

            while (reader.Read())
            {

                if ((reader.Name.ToLower() == "polygon"
                        || reader.Name.ToLower() == "point"
                        || reader.Name.ToLower() == "linestring")
                        && reader.NodeType == XmlNodeType.EndElement)

                    break;


                if (reader.Name.
                           ToLower() == "coordinates")

                    coord.Add(reader.ReadString());

            }

            return coord;

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

        public bool OutputKML(string outputPath, ProgressBar bar)
        {
            XmlTextWriter writer = this.getWriter
                                        (outputPath);
            try
            {
                writeKMLHeader(writer);

                writeKMLStyles(writer);

                writeKMLRegion(writer);

                writeKMLFooter(writer);
            }
            catch
            {
                return false;
            }

            return true;
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

                writer.WriteStartElement("LineStyle");
                writer.WriteStartElement("width");
                writer.WriteString("2");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("PolyStyle");

                writer.WriteStartElement("color");
                writer.WriteString(this.styles[i].colorCode);
                writer.WriteEndElement(); //</color>

                writer.WriteEndElement(); //</polystyle>

                writer.WriteEndElement(); //</style>

                writer.Flush();
            }
        }

        static int objCounter = 0;

        private void writeKMLRegion(XmlTextWriter writer)
        {

            writer.WriteStartElement("Folder");

            for (int i = 0; i < this.regions.Count; i++)
            {
                writer.WriteStartElement("Placemark");
                writer.WriteStartElement("name");
                if (regions[i].regionName != "")

                    writer.WriteString("BlackCat Converted Item #" + objCounter++);

                writer.WriteEndElement(); //</name>
                writer.WriteStartElement("visibility"); //indicating the style
                writer.WriteString("0"); //show the polygon
                writer.WriteEndElement(); //</visibility>

                if (regions[i].regionStyle != null)
                {
                    writer.WriteStartElement("styleUrl"); //indicating the style
                    writer.WriteString(this.regions[i].
                                            regionStyle.styleName); //show the polygon
                    writer.WriteEndElement(); //</styleUrl>
                }

                string kind = regions[i].regionType;

                outputData(regions[i], kind, writer);




                writer.WriteEndElement(); //</placemark>
                writer.Flush();
            }

            writer.WriteEndElement(); //</Folder>
        }


        private void outputData
            (Region data, string kind, XmlTextWriter writer)
        {
            switch (kind)
            {
                case PLINE_CODE:
                    {
                        outputPLINE(data, writer);
                        break;
                    }
                case LINE_CODE:
                    {
                        outputLINE(data, writer);
                        break;
                    }
                case POINT_CODE:
                    {
                        outputPOINT(data, writer);
                        break;
                    }
                case POLYGON_CODE:
                    {
                        outputPOLYGON(data, writer);
                        break;
                    }
            }
        }

        #region DataOutput

        private void outputPLINE
            (Region data, XmlTextWriter writer)
        {

            //nested tags
            writer.WriteStartElement("LineString");

            writer.WriteStartElement("extrude");
            writer.WriteString("1");
            writer.WriteEndElement(); //</extrude>

            writer.WriteStartElement("tessellate");
            writer.WriteString("1");
            writer.WriteEndElement(); //</tessellate>

            writer.WriteStartElement("altitudeMode");
            writer.WriteString("clampedToGround");
            writer.WriteEndElement(); //</altitudeMode>

            writer.WriteStartElement("coordinates");
            writer.WriteRaw(data.coordinates[0]);
            writer.WriteEndElement(); //</coordinates>


            writer.WriteEndElement(); //</point>

        }

        private void outputLINE
            (Region data, XmlTextWriter writer)
        {
            writer.WriteStartElement("LineString");

            writer.WriteStartElement("extrude");
            writer.WriteString("1");
            writer.WriteEndElement(); //</extrude>

            writer.WriteStartElement("tessellate");
            writer.WriteString("1");
            writer.WriteEndElement(); //</tessellate>

            writer.WriteStartElement("altitudeMode");
            writer.WriteString("clampedToGround");
            writer.WriteEndElement(); //</altitudeMode>

            writer.WriteStartElement("coordinates");
            writer.WriteString
                    (data.coordinates[0]);
            writer.WriteEndElement(); //</coordinates>

            writer.WriteEndElement(); //</point>
        }

        private void outputPOINT
            (Region data, XmlTextWriter writer)
        {
            writer.WriteStartElement("Point");

            writer.WriteStartElement("extrude");
            writer.WriteString("1");
            writer.WriteEndElement(); //</extrude>

            writer.WriteStartElement("altitudeMode");
            writer.WriteString("clampedToGround");
            writer.WriteEndElement(); //</altitudeMode>

            writer.WriteStartElement("coordinates");
            writer.WriteString
                    (data.coordinates[0]);
            writer.WriteEndElement(); //</coordinates>

            writer.WriteEndElement(); //</point>
        }

        private void outputPOLYGON
            (Region data, XmlTextWriter writer)
        {
            //First element as inner boundary
            writer.WriteStartElement("Polygon");
            writer.WriteStartElement("extrude"); //indicating the style
            writer.WriteString("1");
            writer.WriteEndElement(); //</extrude>
            writer.WriteStartElement("tessellate"); //indicating the style
            writer.WriteString("1");
            writer.WriteEndElement(); //</tessellate>

            writer.WriteStartElement("altitudeMode"); //indicating the style
            writer.WriteString("clampToGround"); //show the polygon
            writer.WriteEndElement(); //</altitudeMode>

            writer.WriteStartElement("outerBoundaryIs"); //indicating the style
            writer.WriteStartElement("LinearRing"); //indicating the style
            writer.WriteStartElement("coordinates");
            writer.WriteRaw(data.coordinates[0]);
            writer.WriteEndElement(); //</coordinates>
            writer.WriteEndElement(); //</LinearRing>
            writer.WriteEndElement(); //</outerBoundaryIs>

            //rest as optional outer boundaries
            for (int i = 1; i < data.coordinates.Count; i++) //exclude first index
            {
                writer.WriteStartElement("innerBoundaryIs"); //indicating the style
                writer.WriteStartElement("LinearRing"); //indicating the style
                writer.WriteStartElement("coordinates");
                writer.WriteRaw(data.coordinates[i]);
                writer.WriteEndElement(); //</coordinates>
                writer.WriteEndElement(); //</LinearRing>
                writer.WriteEndElement(); //</innerBoundaryIs>
            }

            writer.WriteEndElement(); //</polystyle> 
        }

        #endregion

        private void writeKMLFooter(XmlTextWriter writer)
        {
            writer.WriteEndElement(); //</document>
            writer.WriteEndElement(); //</kml>

            writer.WriteEndDocument(); //commit write
            writer.Close(); //close writer
        }

        #endregion


        public void SetRegionStyle
               (string regionIdentifier, Style style)
        {

            foreach (Region r in regions)

                if (r.regionName == regionIdentifier)
                {
                    r.regionStyle = style;
                    break;
                }

            chkModelStyle(style);
        }

        private void chkModelStyle(Style style)
        {
            if (this.styles.Count < 1)
                styles.Add(style);
            else
            {
                bool hasStyle = false;

                foreach (Style s in styles)
                    if (s.styleName == style.styleName)
                        hasStyle = true;

                if (!hasStyle)
                    this.styles.Add(style);
            }
        }

        public String[] GetRegionIdentifiers()
        {
            string[] ident =
                new string[regions.Count];

            for (int i = 0; i < ident.Length; i++)
                ident[i] = regions[i].regionName;

            return ident;
        }

        private XmlTextWriter getWriter
                            (String outputPath)
        {
            string filename = "BlackCatKML_"
                                + DateTime.Now.ToString("yyyy.MM.dd")
                                + ".kml";

            XmlTextWriter writer = new
                                XmlTextWriter(outputPath, null);

            writer.WriteStartDocument(); //Open document

            writer.Formatting = Formatting.Indented;
            writer.IndentChar = '\t';
            writer.Indentation = 4;

            return writer;
        }

        private XmlTextReader getReader
                            (String fileURL)
        {
            return new XmlTextReader(fileURL);
        }

        private StreamReader getReader
                (String fileUrl, bool isMapFile)
        {
            return new StreamReader(fileUrl);
        }

        /*
         *Added on 14 Sep 
         */
        public List<String> DataFieldNames()
        {
            return dataFields;
        }

    }
}
