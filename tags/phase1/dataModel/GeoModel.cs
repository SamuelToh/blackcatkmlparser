using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
//using System.Threading;
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

        long totalSize;
        long currRead = 1;
        //ProgressBar tempBar;
        //static Mutex mutex = new Mutex(false, "blackCatLock");

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

        private long getFileSize(string fileURL)
        {
            return File.ReadAllLines(fileURL).Length;
            //return fileSize.Length;
        }


        //14September
 /*       private void populateDataFields
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
                    incrementRead();
                    //Take in first identifier, we dont want data type ident
                    this.dataFields.Add(columnNames[0]);
                }
            }
        }*/

        /*private void updateBar()
        {
            while (tempBar.Value < 90)
            {
                Thread.Sleep(50);
                mutex.WaitOne();
                double m_Value = Convert.ToDouble(currRead) / Convert.ToDouble(this.totalSize);
                tempBar.Value = Convert.ToInt16(m_Value * 100);
                //Console.WriteLine("The value now is : " + tempBar.Value);
                mutex.ReleaseMutex();
            }
        }*/


        //14September - Threading for progress Bar
        /*private void incrementRead()
        {
            mutex.WaitOne();
            this.currRead++;
            mutex.ReleaseMutex();
        }*/





        #region Output KML Codes

        public bool OutputKML(string outputPath, ProgressBar bar)
        {
            XmlTextWriter writer = this.getWriter(outputPath);
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
                                this.styles[i].StyleName);

                writer.WriteStartElement("LineStyle");
                writer.WriteStartElement("width");
                writer.WriteString("2");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("PolyStyle");

                writer.WriteStartElement("color");
                writer.WriteString(this.styles[i].ColorCode);
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

            #region "KML Folder Items"
            writer.WriteStartElement("name");
            writer.WriteString("BlackCat KML Parser Object(s) Folder");
            writer.WriteEndElement();
            writer.WriteStartElement("description");
            writer.WriteString("The parsing was done on : " + DateTime.Now.ToString() +
                                    " using BlackCat KML Parser Version 1.0");
            writer.WriteEndElement();

            /*
            writer.WriteStartElement("LookAt");
            
            writer.WriteStartElement("longitude");
            writer.WriteString("-122.0839597145766");
            writer.WriteEndElement();

            writer.WriteStartElement("latitude");
            writer.WriteString("37.42222904525232");
            writer.WriteEndElement();
            writer.WriteStartElement("altitude");
            writer.WriteString("0");
            writer.WriteEndElement();
            writer.WriteStartElement("heading");
            writer.WriteString("-148.4122922628044");
            writer.WriteEndElement();
            writer.WriteStartElement("tilt");
            writer.WriteString("40.5575073395506");
            writer.WriteEndElement();
            writer.WriteStartElement("range");
            writer.WriteString("500.6566641072245");
            writer.WriteEndElement();
            writer.WriteEndElement(); //</lookat>
             */
            #endregion
           

            for (int i = 0; i < this.regions.Count; i++)
            {
                writer.WriteStartElement("Placemark");
                writer.WriteStartElement("name");
                //if (regions[i].regionName != "")

                  //  writer.WriteString("BlackCat Converted Item #" + objCounter++);
                writer.WriteString(regions[i].regionName);

                writer.WriteEndElement(); //</name>
                writer.WriteStartElement("visibility"); //indicating the style
                writer.WriteString("0"); //show the polygon
                writer.WriteEndElement(); //</visibility>

                if (regions[i].regionStyle != null)
                {
                    writer.WriteStartElement("styleUrl"); //indicating the style
                    writer.WriteString(this.regions[i].regionStyle.StyleName); //show the polygon
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

        public Style GetRegionStyle(String regionIdentifier)
        {
            foreach (Region r in regions)
                if (r.regionName == regionIdentifier)
                    return r.regionStyle;
            return null;
        }

        private void chkModelStyle(Style style)
        {
            if (this.styles.Count < 1)
                styles.Add(style);
            else
            {
                bool hasStyle = false;

                foreach (Style s in styles)
                    if (s.StyleName == style.StyleName)
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

        //Added for testing purposes
        public String[] GetRegionCoordinates(String regionIdentifier)
        {
            for (int i = 0; i < regions.Count; i++)
                if (regions[i].regionName == regionIdentifier)
                    return regions[i].coordinates.ToArray();
            return null;
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


