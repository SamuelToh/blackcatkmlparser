﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//using System.Threading;
using System.Text.RegularExpressions;
using log4net;

namespace BlackCat
{
    public partial class GeoModel
    {

        public bool BuildGeoModel(string kmlFileURL, ProgressBar bar)
        {
            log.Debug("Start of BuildGeoModel");
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
                return false;
            }

            //startprogress.Abort();
            bar.Value = 100;
            this.totalSize = 0;
            this.currRead = 1;
            return true;
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
                    r.regionType = POLYGON_CODE;
                    r.coordinates = extractCoord(reader);
                }
                else if (reader.Name.ToLower() == "linestring") //or pline
                {
                    r.regionType = LINE_CODE;
                    r.coordinates = extractCoord(reader);
                }
                else if (reader.Name.ToLower() == "point")
                {
                    r.regionType = POINT_CODE;
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
            this.styles.Add(new Style(colorCode, styleId));
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

        private XmlTextReader getReader
                            (String fileURL)
        {
            return new XmlTextReader(fileURL);
        }

    }
}
