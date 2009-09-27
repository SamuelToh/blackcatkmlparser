using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using log4net;

namespace BlackCat
{
    public class KMLReader : IGeoReader
    {
        private string kmlFileURL;
        private ILog log;
        private List<String> dataFields; //TODO: necessary? move to Region
        private long totalSize;
        private int currRead;
        private List<Region> regions;

        public KMLReader(String kmlURL)
        {
            this.kmlFileURL = kmlURL;
            this.dataFields = new List<string>();
            this.regions = new List<Region>();
            log = LogManager.GetLogger(this.ToString());
        }

        public List<Region> ReadRegions(ProgressBar bar)
        {
            log.Debug("Start of BuildGeoModel");
            this.dataFields.Clear();
            dataFields.Add("name"); //Standard data field for kml
            bool endOfFile = false;

            this.totalSize = getFileSize(kmlFileURL);
            //this.tempBar = bar;
            //ThreadStart theprogress = new ThreadStart(updateBar);
            //Thread startprogress = new Thread(theprogress);
            //startprogress.Start();

            XmlTextReader reader = getReader(kmlFileURL);
            try
            {
                while (reader.Read() && !endOfFile)
                {
                    //Get the curr node's tag name
                    string tagName = reader.Name.ToLower();

                    //incrementRead();

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
                //TODO: react better to exceptions return false;
            }

            //startprogress.Abort();
            bar.Value = 100;
            this.totalSize = 0;
            this.currRead = 1;
            return regions;
        }


        private void buildRegion(XmlTextReader reader)
        {
            log.Debug("Start of buildRegion");
            Region r = new Region();
            reader.Read();
            //incrementRead();

            while (reader.Name.ToLower() != "placemark" && reader.NodeType != XmlNodeType.EndElement)
            {
                //Search for <name> [Item name] </name>
                if (reader.Name.ToLower() == "name")
                    r.regionName = reader.ReadString();


                //Search for <polygon> 
                else if (reader.Name.ToLower() == "polygon")
                {
                    r.regionType = Region.POLYGON_CODE;
                    r.coordinates = extractCoord(reader);
                }
                else if (reader.Name.ToLower() == "linestring") //or pline
                {
                    r.regionType = Region.LINE_CODE;
                    r.coordinates = extractCoord(reader);
                }
                else if (reader.Name.ToLower() == "point")
                {
                    r.regionType = Region.POINT_CODE;
                    r.coordinates = extractCoord(reader);
                }

                //break;
                reader.Skip();
                //incrementRead();                 
            }
            log.Debug("Region created - adding to model with name " + r.regionName);
            this.regions.Add(r);
        }

        private List<string> extractCoord(XmlTextReader reader)
        {
            List<string> coord = new List<string>();

            while (reader.Read())
            {
                //incrementRead();
                log.Debug("Reading node - " + reader.Name);

                if ((reader.Name.ToLower() == "polygon"
                        || reader.Name.ToLower() == "point"
                        || reader.Name.ToLower() == "linestring")
                        && reader.NodeType == XmlNodeType.EndElement)

                    break;

                //TODO: Ignoring inner boundaries as our model does not currently support this.
                if (reader.Name.ToLower() == "innerboundaryis")
                {
                    log.Debug("Skipping node - " + reader.Name);
                    reader.Skip();
                }

                if (reader.Name.ToLower() == "coordinates")
                {
                    coord.Add(reader.ReadString());
                }

            }

            return coord;

        }

        private void buildStyle(XmlTextReader reader)
        {
            log.Debug("Building style");
            string styleId = "";
            string colorCode = "";

            styleId = reader.GetAttribute("id");

            while (reader.Read())
            {

                if (reader.Name.ToLower() == "style" && reader.NodeType == XmlNodeType.EndElement)
                    break;

                else if (reader.Name.ToLower() == "polystyle")
                {
                    colorCode = extractColor(reader);
                    break; //we found what we want so we break out of loop
                }
            }
            //this.styles.Add(new Style(colorCode, styleId)); TODO: this will probably break styles, check regions for styles before output
        }

        private string extractColor(XmlTextReader reader)
        {
            string color = "#000000"; //default color 

            while (reader.Read())
            {
                //incrementRead();
                //Incase we couldnt find color tag we break off the loop
                if (reader.Name.ToLower() == "polystyle" && reader.NodeType == XmlNodeType.EndElement)
                    break;

                if (reader.Name.ToLower() == "color")
                {
                    color = reader.ReadString();
                    log.Debug("Found a color - " + color);
                    break;
                }
            }

            return color;
        }


        private long getFileSize(string fileURL)
        {
            return File.ReadAllLines(fileURL).Length;
            //return fileSize.Length;
        }

        private XmlTextReader getReader
                            (String fileURL)
        {
            return new XmlTextReader(fileURL);
        }

    }
}
