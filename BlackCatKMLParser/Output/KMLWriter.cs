using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace BlackCat
{
    public class KMLWriter : IGeoWriter
    {
        const string RAW_INDENTATION = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
        private const string KML_NAMESPACE_ADDR = "http://www.opengis.net/kml/2.2";
        private IGeoModel geoModel;

        public bool WriteToFile(IGeoModel model, List<String> dataFieldsToDisplay, String outputPath, ProgressBar progressBar)
        {
            this.geoModel = model; 

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
            for (int i = 0; i < geoModel.Styles.Length; i++)
            {
                writer.WriteStartElement("Style");

                //<style id = ??>
                writer.WriteAttributeString("id",
                                geoModel.Styles[i].StyleName);

                writer.WriteStartElement("LineStyle");
                writer.WriteStartElement("width");
                writer.WriteString("2");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteStartElement("PolyStyle");

                writer.WriteStartElement("color");
                writer.WriteString(geoModel.Styles[i].ColorCode);
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
           
            Region[] regions = this.geoModel.Regions;
            for (int i = 0; i < regions.Length; i++)
            {
                writer.WriteStartElement("Placemark");
                writer.WriteStartElement("name");
                //if (regions[i].regionName != "")

                  //  writer.WriteString("BlackCat Converted Item #" + objCounter++);
                writer.WriteString(regions[i].RegionName);

                writer.WriteEndElement(); //</name>
                writer.WriteStartElement("visibility"); //indicating the style
                writer.WriteString("0"); //show the polygon
                writer.WriteEndElement(); //</visibility>

                if (regions[i].RegionStyle != null)
                {
                    writer.WriteStartElement("styleUrl"); //indicating the style
                    writer.WriteString(regions[i].RegionStyle.StyleName); //show the polygon
                    writer.WriteEndElement(); //</styleUrl>
                }

                string kind = regions[i].RegionType;

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
                case Region.PLINE_CODE:
                    {
                        outputPLINE(data, writer);
                        break;
                    }
                case Region.LINE_CODE:
                    {
                        outputLINE(data, writer);
                        break;
                    }
                case Region.POINT_CODE:
                    {
                        outputPOINT(data, writer);
                        break;
                    }
                case Region.POLYGON_CODE:
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
            writer.WriteRaw(data.Coordinates[0]);
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
                    (data.Coordinates[0]);
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
                    (data.Coordinates[0]);
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
            writer.WriteRaw(data.Coordinates[0]);
            writer.WriteEndElement(); //</coordinates>
            writer.WriteEndElement(); //</LinearRing>
            writer.WriteEndElement(); //</outerBoundaryIs>

            //rest as optional outer boundaries
            for (int i = 1; i < data.Coordinates.Count; i++) //exclude first index
            {
                writer.WriteStartElement("innerBoundaryIs"); //indicating the style
                writer.WriteStartElement("LinearRing"); //indicating the style
                writer.WriteStartElement("coordinates");
                writer.WriteRaw(data.Coordinates[i]);
                writer.WriteEndElement(); //</coordinates>
                writer.WriteEndElement(); //</LinearRing>
                writer.WriteEndElement(); //</innerBoundaryIs>
            }

            writer.WriteEndElement(); //</polystyle> 
        }

        #endregion


        private XmlTextWriter getWriter(String outputPath)
        {
            string filename = "BlackCatKML_"
                                + DateTime.Now.ToString("yyyy.MM.dd")
                                + ".kml";

            XmlTextWriter writer = new XmlTextWriter(outputPath, null);

            writer.WriteStartDocument(); //Open document

            writer.Formatting = Formatting.Indented;
            writer.IndentChar = '\t';
            writer.Indentation = 4;

            return writer;
        }

        private void writeKMLFooter(XmlTextWriter writer)
        {
            writer.WriteEndElement(); //</document>
            writer.WriteEndElement(); //</kml>

            writer.WriteEndDocument(); //commit write
            writer.Close(); //close writer
        }
    }
}
