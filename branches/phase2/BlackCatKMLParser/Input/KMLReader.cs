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
        //private long totalSize;
        //private int currRead;
        private List<Region> regions;
        public List<Category> category; //11october

        public KMLReader(String kmlURL)
        {
            this.kmlFileURL = kmlURL;
            this.dataFields = new List<string>();
            this.regions = new List<Region>();
            this.category = new List<Category>();
            log = LogManager.GetLogger(this.ToString());
        }

        public List<Region> ReadRegions(ProgressWrapper progress)
        {
            log.Debug("Start of BuildGeoModel");
            this.dataFields.Clear();
            dataFields.Add("name"); //Standard data field for kml
            bool endOfFile = false;

            int totalSize = GetFileSize(kmlFileURL);

            XmlTextReader reader = GetReader(kmlFileURL);
            if (!reader.HasLineInfo())
                log.Warn("No line information available - progress will be inaccurate");
            progress.SetPercentage(1);
            string currCategoryName = "";

            try
            {
                while (reader.Read() && !endOfFile)
                {
                    //Get the curr node's tag name
                    string tagName = reader.Name.ToLower();                    

                    switch (tagName)
                    {
                        case "folder":
                            {
                                //10 October
                                if (reader.NodeType == XmlNodeType.Element)
                                    currCategoryName = BuildCategory(reader);
                                break;
                            }
                        case "placemark":
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                    BuildRegion(reader, currCategoryName);
                                break;
                            }
                        case "style":
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                    BuildStyle(reader);
                                break;
                            }
                        case "kml":
                            {
                                if (reader.NodeType == XmlNodeType.EndElement)
                                    endOfFile = true;
                                break;
                            }
                    }//End Switch
                    int lineNumber = reader.LineNumber;
                    if (lineNumber != 0)
                        progress.SetPercentage((int)(lineNumber * 100d / totalSize));
                }//End While
            }
            catch
            {
                //TODO: react better to exceptions return false;
            }

            progress.SetPercentage(100);
            reader.Close();
            return regions;
        }

        private string BuildCategory(XmlTextReader reader)
        {
            log.Debug("Start of BuildCategory");
            Category c = new Category();

            while (reader.Read())
            {
                if (reader.Name.ToLower() == "name")
                {
                    c.CategoryName = reader.ReadString();
                    return c.CategoryName;
                }
                else if (reader.Name.ToLower() == "")
                { /*White space indentation we ignore this and chk next node*/ }
                else
                    return ""; //return nothing

            }

            return "";
        }

        private void ChkCategoryExist(Category c)
        {
            if (this.category.Count < 1)
                category.Add(c);
            else
            {
                bool hasCat = false;

                foreach (Category cat in category)
                    if (cat.CategoryName == c.CategoryName)
                        break;

                if(!hasCat) 
                    this.category.Add(c);
            }
        }

        private void BuildRegion(XmlTextReader reader, string category)
        {
            log.Debug("Start of BuildRegion");
            Region r = new Region();
            //12 October
            Category c = new Category(category);
            r.RegionCategory = c;
            reader.Read();
            //incrementRead();

            while (reader.Name.ToLower() != "placemark" && reader.NodeType != XmlNodeType.EndElement)
            {
                //Search for <name> [Item name] </name>
                if (reader.Name.ToLower() == "name")
                    r.RegionName = reader.ReadString();
                //10 October - read in MapInfo data 
                else if (reader.Name.ToLower() == "description")
                {
                    List<String> dataKey = r.DataNames;
                    dataKey.Add("OriginalDesc");
                    r.DataNames = dataKey;
                    r.AddDataValue(reader.ReadString()); //read everything in
                }
                //Search for <polygon> 
                else if (reader.Name.ToLower() == "polygon")
                {
                    r.RegionType = Region.POLYGON_CODE;
                    r.Coordinates = ExtractCoord(reader);
                }
                else if (reader.Name.ToLower() == "linestring") //or pline
                {
                    r.RegionType = Region.LINE_CODE;
                    r.Coordinates = ExtractCoord(reader);
                }
                else if (reader.Name.ToLower() == "point")
                {
                    r.RegionType = Region.POINT_CODE;
                    r.Coordinates = ExtractCoord(reader);
                }

                //break;
                reader.Skip();
                //incrementRead();                 
            }
            log.Debug("Region created - adding to model with name " + r.RegionName);
            this.regions.Add(r);
        }

        private List<string> ExtractCoord(XmlTextReader reader)
        {
            List<string> coord = new List<string>();

            while (reader.Read())
            {
                //incrementRead();
                if ((reader.Name.ToLower() == "polygon"
                        || reader.Name.ToLower() == "point"
                        || reader.Name.ToLower() == "linestring")
                        && reader.NodeType == XmlNodeType.EndElement)

                    break;

                //TODO: Ignoring inner boundaries as our model does not currently support this.
                if (reader.Name.ToLower() == "innerboundaryis")
                {
                    reader.Skip();
                }

                if (reader.Name.ToLower() == "coordinates")
                {
                    coord.Add(reader.ReadString());
                }

            }

            return coord;

        }

        private void BuildStyle(XmlTextReader reader)
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
                    colorCode = ExtractColor(reader);
                    break; //we found what we want so we break out of loop
                }
            }
            //this.styles.Add(new Style(colorCode, styleId)); TODO: this will probably break styles, check regions for styles before output
        }

        private string ExtractColor(XmlTextReader reader)
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


        private int GetFileSize(string fileURL)
        {
            return File.ReadAllLines(fileURL).Length;
            //return fileSize.Length;
        }

        private XmlTextReader GetReader(String fileURL)
        {
            return new XmlTextReader(fileURL);
        }

    }
}
