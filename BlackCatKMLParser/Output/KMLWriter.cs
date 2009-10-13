﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using log4net;

namespace BlackCat
{
    public class KMLWriter : IGeoWriter
    {
        private ILog log;
        const string RAW_INDENTATION = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
        private const string KML_NAMESPACE_ADDR = "http://www.opengis.net/kml/2.2";
        private IGeoModel geoModel;
        /*private string[] regCategories = new string[5]
            {"Northern Queensland", "East Queensland", 
                "Southern Queensland", "West Queensland", "Others"};*/

        private List<Category> district;

        public KMLWriter()
        {
            log = LogManager.GetLogger(this.ToString());
        }

        public bool WriteToFile(IGeoModel model, List<String> dataFieldsToDisplay, String outputPath, ProgressWrapper progressBar)
        {
            this.geoModel = model;

            InitializeCategoryList();

            XmlTextWriter writer = this.GetWriter(outputPath);
            try
            {
                int progress = progressBar.GetPercentage();
                log.Debug("Start Write - percentage is " + progress);
                int progressIncrement = (100 - progress)/4;
                WriteKMLHeader(writer);
                progressBar.Increment(progressIncrement);
                log.Debug("Writing styles");
                WriteKMLStyles(writer);
                progressBar.Increment(progressIncrement);
                log.Debug("Writing regions");
                WriteKMLRegion(writer, dataFieldsToDisplay);
                log.Debug("Incrementing progress bar #3");
                progressBar.Increment(progressIncrement);
                log.Debug("Writing footer");
                WriteKMLFooter(writer);
                progressBar.Increment(progressIncrement);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void InitializeCategoryList()
        {
            this.district = new List<Category>();

            if (geoModel.Regions.Count() > 0)
            {
                district.Add(geoModel.Regions[0].RegionCategory);

                foreach (Region r in geoModel.Regions)
                {
                    bool hasDistrict = false;

                    for (int i = 0; i < district.Count; i++)
                    {
                        if (district[i].CategoryName
                                == r.RegionCategory.CategoryName)
                        {
                            hasDistrict = true;
                            break;
                        }
                    }

                    if (!hasDistrict)
                        district.Add(r.RegionCategory);
                }
            }
        }

        private void WriteKMLHeader(XmlTextWriter writer)
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

        private void WriteKMLStyles(XmlTextWriter writer)
        {
            for (int i = 0; i < geoModel.Styles.Length; i++)
            {
                writer.WriteStartElement("Style");

                //<style id = ??>
                writer.WriteAttributeString("id",
                                geoModel.Styles[i].StyleName);

                writer.WriteStartElement("LineStyle");
                writer.WriteStartElement("width");
                writer.WriteString("3");
                writer.WriteEndElement();
                writer.WriteStartElement("color");
                writer.WriteString("ff000000");
                writer.WriteEndElement(); //</color>
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

        //11 october
        private void WriteKMLRegion(XmlTextWriter writer, List<String> dataFieldsToDisplay)
        {
            //Region[] regions = this.geoModel.Regions;
            List<Region> regions = this.geoModel.Regions.ToList<Region>();
            bool isEnhancedKML = false; //flag

            foreach (Category c in district)
            {
                //Console.WriteLine("Writing District > " + c.CategoryName);
                writer.WriteStartElement("Folder");

                writer.WriteStartElement("name");
                writer.WriteString(c.CategoryName);
                writer.WriteEndElement(); //</name>

                //if desc not empty
                if(c.CategoryDesc != null)
                {
                    writer.WriteStartElement("description"); //<description>
                    writer.WriteString(c.CategoryDesc); //write the description out
                    writer.WriteEndElement(); //</description>
                }

                //Check every region's category
                for(int i = 0; i < regions.Count; i ++)
                {
                    //If selected region data is the same as curr category, print data
                    if (regions[i].RegionCategory.CategoryName == c.CategoryName)
                    {
                        //Write KML Data
                        writer.WriteStartElement("Placemark");
                        writer.WriteStartElement("name");
                        writer.WriteString(regions[i].RegionName);
                        writer.WriteEndElement(); //</name>

                        //check region data value
                        if(regions[i].DataNames.Count > 0)
                        {
                            writer.WriteStartElement("description"); //<description>
                            StringBuilder sb = new StringBuilder();
                            
                            if (dataFieldsToDisplay == null)
                            {
                                dataFieldsToDisplay = new List<string>();
                                isEnhancedKML = true;
                            }

                            //12 October Display only the items the user requested
                            if (isEnhancedKML)
                            {
                                //Enhanecd KML route
                                
                                if (regions[i].DataNames.Count > 1) //if has preserved item
                                {
                                    sb.Append("Preserved Map info data<br><hr>");
                                    sb.Append("<table border='1'>");
                                    sb.Append("<tr><td colspan='2'>");
                                    sb.Append(regions[i].GetDataValue(0));
                                    sb.Append("</td></tr>");
                                    sb.Append("</table>");
                                    sb.Append("<br>");
                                    sb.Append("<b>New</b><br>");

                                }

                            }

                                sb.Append("Map info data<br><hr>");
                                sb.Append("<table>");
                            
                            int[] selectedIndex = new int[dataFieldsToDisplay.Count];

                            //First we convert all selected index to array
                            for (int x = 0; x < dataFieldsToDisplay.Count; x++)
                            {
                                selectedIndex[x] = Convert.ToInt16(dataFieldsToDisplay[x]);
                            }

                            //retrieve a list of datanames out
                            List<String> dataNames = regions[i].DataNames;

                            //12 October to display the alternative data
                            if (dataNames.Count > 1) //has last two values (winner and safety keys);
                            {
                                //Seat winner value
                                sb.Append("<tr><td>");
                                sb.Append(dataNames[dataNames.Count - 1]); //take last name
                                sb.Append("</td><td>");
                                sb.Append(regions[i].GetDataValue(dataNames.Count - 1)); //take corresponding datavalue out
                                sb.Append("</td></tr>");

                                //Seat Safety Value
                                sb.Append("<tr><td>");
                                sb.Append(dataNames[dataNames.Count - 2]); //take last name
                                sb.Append("</td><td>");
                                sb.Append(regions[i].GetDataValue(dataNames.Count - 2)); //take corresponding datavalue out
                                sb.Append("</td></tr>");
                            }

                            //for each selected index we append its names and value to the <desc> tag
                            for (int x = 0; x < selectedIndex.Length; x++)
                            {
                                sb.Append("<tr><td>");
                                sb.Append(dataNames[selectedIndex[x]]);
                                sb.Append("</td><td>");
                                sb.Append(regions[i].GetDataValue(selectedIndex[x]));
                                sb.Append("</td></tr>");
                            }

                            //sb.Append("]]>");
                            sb.Append("</table><br><hr>");
                            writer.WriteCData(sb.ToString());
                            //writer.WriteString(sb.ToString());
                            writer.WriteEndElement(); //</description>
                        }


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

                        OutputData(regions[i], kind, writer);

                        writer.WriteEndElement(); //</placemark>
                        writer.Flush();
                        //11october
                        regions.RemoveAt(i); //remove added data
                        i --; //decrease index back by 1
                    }//End if
                }//end For loop

                writer.WriteEndElement(); //</Folder>

            }//End for each loop
            
            //writer.WriteStartElement("Folder");

            #region "Obsolete KML Folder Items"
            /*
            writer.WriteStartElement("name");
            writer.WriteString("BlackCat KML Parser Object(s) Folder");
            writer.WriteEndElement();
            writer.WriteStartElement("description");
            writer.WriteString("The parsing was done on : " + DateTime.Now.ToString() +
                                    " using BlackCat KML Parser Version 1.0");
            writer.WriteEndElement();

            
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

            #region Old Phase 1 method
            //Region[] regions = this.geoModel.Regions;
            /*for (int i = 0; i < regions.Length; i++)
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

                OutputData(regions[i], kind, writer);

                writer.WriteEndElement(); //</placemark>
                writer.Flush();
            }

            writer.WriteEndElement(); //</Folder>*/
            #endregion
            
        }


        private void OutputData
            (Region data, string kind, XmlTextWriter writer)
        {
            switch (kind)
            {
                case Region.PLINE_CODE:
                    {
                        OutputPLINE(data, writer);
                        break;
                    }
                case Region.LINE_CODE:
                    {
                        OutputLINE(data, writer);
                        break;
                    }
                case Region.POINT_CODE:
                    {
                        OutputPOINT(data, writer);
                        break;
                    }
                case Region.POLYGON_CODE:
                    {
                        OutputPOLYGON(data, writer);
                        break;
                    }
            }
        }

        #region DataOutput

        private void OutputPLINE
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

        private void OutputLINE
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

        private void OutputPOINT
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

        private void OutputPOLYGON
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
            String coordinates = CheckCoordinatesLength(data.RegionName, data.Coordinates[0]);
            writer.WriteRaw(coordinates);
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

        private String CheckCoordinatesLength(String region, String coordinates)
        {
            //More than 20000 or so coordinates confuses google earth - reduce 'em
            String[] coords = coordinates.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            log.Debug("Coord count " + region + " - " + coords.Length);
            if (coords.Length > 20000)
            {
                List<String> smaller = new List<string>(coords.Length / 2);
                for (int i = 0; i < coords.Length; i += 2)
                {
                    smaller.Add(coords[i]);
                }
                log.Debug("Region " + region + " reduced to " + smaller.Count);
                StringBuilder builder = new StringBuilder("\r\n");
                foreach (String s in smaller)
                {
                    builder.Append(s);
                    builder.Append("\r\n");
                }
                return builder.ToString();
            }
            return coordinates;
        }

        #endregion


        private XmlTextWriter GetWriter(String outputPath)
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

        private void WriteKMLFooter(XmlTextWriter writer)
        {
            writer.WriteEndElement(); //</document>
            writer.WriteEndElement(); //</kml>

            writer.WriteEndDocument(); //commit write
            writer.Close(); //close writer
        }
    }
}
