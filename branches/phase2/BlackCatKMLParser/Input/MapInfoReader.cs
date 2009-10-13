using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using log4net;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace BlackCat
{
    public class MapInfoReader : IGeoReader
    {
        private ILog log = LogManager.GetLogger(typeof(MapInfoReader));

        private String midURL;
        private String mifURL;
        private ProgressWrapper progress;
        private List<Region> regions;
        private int objCounter; //TODO: identical to region.count?
        private List<String> dataFieldNames; //TODO: data should be held only in region
        private Char delimiter = '\t';

        //private OleDbConnection con; //db connection

        public MapInfoReader(String midURL, String mifURL)
        {
            this.midURL = midURL;
            this.mifURL = mifURL;
        }

        public List<Region> ReadRegions(ProgressWrapper progress)
        {
            this.regions = new List<Region>();
            this.dataFieldNames = new List<String>();
            this.objCounter = 0;
            this.progress = progress;

            try
            {
                StreamReader mifReader = new StreamReader(mifURL);
                StreamReader midReader = new StreamReader(midURL);

                // READ mif header
                //int dataCount;
                log.Debug("Reading header");
                readHeader(mifReader);
                progress.SetPercentage(2);

                //TODO: this is hard coded to be the second data item - in our test MapInfo files, this is Elect_div
                int regionNameIndex = 0;
                regionNameIndex = 1;

                // READ mid file - if there is data
                log.Debug("Reading mid file");
                readMidFile(midReader, delimiter, regionNameIndex);
                log.Debug("Setting data field names");

                foreach (Region r in regions)
                {
                    string[] newFieldNames = new string[dataFieldNames.Count];
                    dataFieldNames.CopyTo(newFieldNames);
                    r.DataNames = newFieldNames.ToList<string>();
                }
                progress.SetPercentage(5);

                // READ mif DATA information
                log.Debug("Reading mif data");
                int regionCount = 0;
                String line = mifReader.ReadLine();
                while (!mifReader.EndOfStream)
                {
                    log.Debug("Reading line");
                    if (line != null)
                    {
                        if (line.ToUpper().StartsWith("REGION"))
                        {
                            line = line.Substring(6).Trim();
                            int polyCount;
                            bool polyCountSuccess = int.TryParse(line, out polyCount);
                            if (!polyCountSuccess)
                                throw new MapInfoFormatException("Unexpected \"Region\" line format - count of polygons is not an integer");
                            readMapInfoRegion(mifReader, polyCount, regionCount);
                            log.Debug("Region " + regionCount + " read, incrementing count");
                            regionCount++;
                        }
                        //16 September 
                        else if (line.ToUpper().StartsWith("PLINE"))
                        {
                            string[] plineCount = line.Split(' ');
                            ReadPLINE(mifReader,
                                            Convert.ToInt16(plineCount[1]),
                                            regionCount);
                            regionCount++;
                        }
                        else if (line.ToUpper().StartsWith("POINT"))
                        {
                            ReadPoint(mifReader,
                                           line.Split(' '),
                                           regionCount);
                            regionCount++;
                        }
                        else if (line.ToUpper().StartsWith("LINE"))
                        {
                            ReadLine(line.Split(' '), regionCount);
                            regionCount++;
                        }
                    }

                    line = mifReader.ReadLine();
                    //Remove Pen, Brush, Center information - if it is there
                    while (line != null &&
                        (line.Trim().ToUpper().StartsWith("PEN") ||
                        line.Trim().ToUpper().StartsWith("BRUSH") ||
                        line.Trim().ToUpper().StartsWith("CENTER")))
                    {
                        line = mifReader.ReadLine();
                    }

                    //increment bar 
                    int percentageDone = (int)(regionCount * 95d / regions.Count) + 5;
                    log.Debug("Setting percentage - " + percentageDone); 
                    progress.SetPercentage(percentageDone);
                }
                //TODO: check return true;
                progress.SetPercentage(100);
            }
            catch (MapInfoFormatException e)
            {
                //TODO: catch more specific exceptions - and throw it!
                log.Error("MapInfoFormatException occurred while building GeoModel from MapInfo files");
                log.Error(e.Message);
            }
            catch (MapInfoMismatchException e)
            {
                log.Error("MapInfoFormatException occurred while building GeoModel from MapInfo files");
                log.Error(e.Message);
            }
            return regions;
        }

        //Reads info from a mif file until it reaches a line beginning with "data" (case insensitive)
        //Returns : Char[] where:
        // index 0 - the delimiter defined in the header, or the default delimiter if not defined - '\t'
        // index 1 - the number of data items per region in the mid file 
        //private String[] readHeader(StreamReader mifReader)
        private void readHeader(StreamReader mifReader)
        {
            log.Debug("Reading mif header file");

            String line = mifReader.ReadLine();
            while (!line.Trim().ToUpper().Equals("DATA"))
            {
                if (line.Trim().ToUpper().StartsWith("DELIMITER"))
                {
                    Boolean delimSuccess = Char.TryParse(line.Substring(line.IndexOf('"') + 1, 1),out delimiter);
                    if (!delimSuccess)
                        throw new MapInfoFormatException("Unexpected delimiter line");
                    log.Debug("Delimiter located, value is - \"" + delimiter + "\"");
                }
                else if (line.Trim().ToUpper().StartsWith("COLUMNS"))
                {
                    String[] lineParts = line.Split(new Char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                    if (lineParts.Length > 0)
                    {
                        int dataCount;
                        bool success = int.TryParse(lineParts[1], out dataCount);
                        if (!success)
                            throw new MapInfoFormatException("Mif file Column count information in unexpected format");

                        this.dataFieldNames = new List<string>(dataCount);                        
                        for (int i = 0; i < dataCount; i++)
                        {
                            line = mifReader.ReadLine();
                            if (line.Trim().ToUpper().Equals("DATA"))
                                throw new MapInfoFormatException("Not enough column names found in .mif column section");
                            lineParts = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            dataFieldNames.Add(lineParts[0].Trim());
                        }
                    }
                }
                line = mifReader.ReadLine();
            }
            return;
        }

        //Reads the data from the stream, splitting each line using the delimiter supplied.
        //A region will be created for each line. 
        // The name of each region will be the data item at regionNameIndex on that line
        private void readMidFile(StreamReader midReader, Char delim, int regionNameIndex)
        {
            log.Debug("Reading mid data file, delim is \"" + delim + "\"");
            String line = midReader.ReadLine();
            while (line != null)
            {
                String[] lineParts = line.Split(delimiter);
                Region reg = new Region();
                if (regionNameIndex < lineParts.Length)
                {
                    //Region name
                    //Remove inverted commas if necessary - mapinfo adds these to string data
                    char[] commas = new char[]{'\"'}; 
                    string name = lineParts[regionNameIndex];
                    name = name.TrimStart(commas);
                    name = name.TrimEnd(commas);
                    log.Debug("Setting region name = " + name);
                    reg.RegionName = name;

                    //all data
                    foreach (String d in lineParts)
                        reg.AddDataValue(d);
                }
                else
                    throw new MapInfoMismatchException("Data in .mif header does not match data in .mid file");
                regions.Add(reg);
                line = midReader.ReadLine();
            }
        }

        //Reads a MapInfo "Region" - kml calls this a "Polygon"
        private void readMapInfoRegion(StreamReader mifReader, int polygonCount, int regionIndex)
        {
            log.Debug("Reading mapinfo region, index " + regionIndex + " polyCount " + polygonCount);
            //Create new region if necessary
            if (regions[regionIndex] == null)
                regions[regionIndex] = new Region();

            //define the KML type
            regions[regionIndex].RegionType = Region.POLYGON_CODE;

            String line;
            int currentPoly = 0;
            while (currentPoly < polygonCount)
            {
                line = mifReader.ReadLine();

                //Read the number of coordinates in this polygon
                int coordCount;
                Boolean coordSuccess = int.TryParse(line.Trim(), out coordCount);
                if (!coordSuccess)
                    throw new MapInfoFormatException("Unexpected format in Region - count of coordinates in a polygon was not an integer");

                //Add all coords to region object
                StringBuilder coordSet = new StringBuilder("");
                for (int i = 0; i < coordCount; i++)
                {
                    string coords = convertToKMLCoords(mifReader.ReadLine());
                    coordSet.Append("\r\n");
                    coordSet.Append(coords);
                }
                //regions[regionIndex].regionName = "BlackCat Polygon Obj #" + objCounter++;
                regions[regionIndex].Coordinates.Add(coordSet.ToString());
                currentPoly++;
            }   
        }

        private string convertToKMLCoords(String mapInfoCoords)
        {
            //mapinfo format is "xCoord yCoord"
            //kml format is "xCoord,yCoord,height"
            string kmlCoords = mapInfoCoords.Replace(' ', ',');
            kmlCoords += ",0";
            return kmlCoords;
        }


        private void ReadPolygon(StreamReader mifReader, int polyCount)
        {
            string coord = "";
            string temp = "";
            Region reg = new Region(Region.POLYGON_CODE);

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
                                 /*TODO:raw+ RAW_INDENTATION*/;

                    //incrementRead();

                    if (!brushPattern.IsMatch(temp) ||
                           !penPattern.IsMatch(temp))

                        coord += temp;

                }

                reg.Coordinates.Add(coord);
                coord = ""; //reset string
            }

            this.regions.Add(reg);
        }

        private void ReadPLINE(StreamReader mifReader, int plineCount, int regCount)
        {
            string coord = "\n" /*TODO:raw + RAW_INDENTATION*/;
            mifReader.ReadLine();

            //17 sep
            //Create new region if necessary
            if (regions[regCount] == null)
                regions[regCount] = new Region(Region.PLINE_CODE);
            else
                regions[regCount].RegionType = Region.PLINE_CODE;

            //Region reg = new Region(PLINE_CODE);

            for (int i = 0; i < plineCount; i++)
            {
                //incrementRead();
                //Not last item
                if (i != plineCount - 1)
                    coord += mifReader.ReadLine().
                            Replace(' ', ',') + " \n"
                            /*TODO:raw + RAW_INDENTATION*/;
                //else last item we ignore the pen object   
            }

            //see point 3rd variable?
            //16 september
            regions[regCount].RegionName = "BlackCat PolyLine Obj #" + objCounter++;
            regions[regCount].Coordinates.Add(coord);
            //reg.Coordinates.Add(coord);

            //this.regions.Add(reg);
          
        }

        private void ReadLine(string[] lineData, int lineCount)
        {
            string coord = "\n" /*TODO:raw + RAW_INDENTATION*/;

            //17 sep
            //Create new region if necessary
            if (regions[lineCount] == null)
                regions[lineCount] = new Region(Region.LINE_CODE);
            else
                regions[lineCount].RegionType = Region.LINE_CODE;


            for (int i = 1; i < lineData.Length; i++)
            {
                //incrementRead();

                if (i % 2 == 0)
                    coord += lineData[i]
                                + "\n" /*TODO:raw + RAW_INDENTATION*/;
                else
                    coord += lineData[i] + ",";

            }
            coord = coord.Substring(0,
                            coord.Length - 1); //remove \n and extra .
            // + "\n"
            // + RAW_INDENTATION; 
            regions[lineCount].RegionName = "BlackCat Line Obj #" + objCounter++;
            regions[lineCount].Coordinates.Add(coord);

            //reg.Coordinates.Add(coord);

            //this.regions.Add
              //      (reg);

        }

        private void ReadPoint
            (StreamReader mifReader, string[] lineData, int ptCount)
        {
            mifReader.ReadLine(); //Reads Symbol data and ignore it;
            //incrementRead();
            string coord = "";

            //Create new region if necessary
            if (regions[ptCount] == null)
                regions[ptCount] = new Region(Region.POINT_CODE);
            else
                regions[ptCount].RegionType = Region.POINT_CODE;
            //define the KML type
            //regions[regionIndex].regionType = POLYGON_CODE;


            //Region reg = new Region(POINT_CODE);

            for (int i = 1; i < lineData.Length; i++)
                if (i % 2 == 0)
                    coord += lineData[i]
                                + "\n" /*TODO:raw + RAW_INDENTATION*/;
                else
                    coord += lineData[i] + ",";


            coord = coord.Substring(0,
                            coord.Length - 1); //remove \n and extra .
            // + "\n"
            // + RAW_INDENTATION; 

            //reg.Coordinates.Add(coord);

            //16 september
            regions[ptCount].RegionName = "BlackCat Point Obj #" + objCounter++;
            regions[ptCount].Coordinates.Add(coord);
            //this.regions[ptCount].Add
              //      (reg);
        }

        private StreamReader getReader
                (String fileUrl, bool isMapFile)
        {
            return new StreamReader(fileUrl);
        }
            
    }
}
