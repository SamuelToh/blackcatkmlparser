using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using log4net;
using System.Text.RegularExpressions;

namespace BlackCat
{
    public class MapInfoReader : IGeoReader
    {
        private ILog log = LogManager.GetLogger(typeof(GeoModel));

        private String midURL;
        private String mifURL;
        private ProgressBar bar;
        private List<Region> regions;
        private int objCounter; //TODO: identical to region.count?
        private List<String> dataFields; //TODO: data should be held only in region

        public MapInfoReader(String midURL, String mifURL)
        {
            this.midURL = midURL;
            this.mifURL = mifURL;
        }

        public List<Region> ReadRegions(ProgressBar bar)
        {
            this.regions = new List<Region>();
            this.dataFields = new List<String>();
            this.objCounter = 0;
            this.bar = bar;
        
            try
            {
                StreamReader mifReader = new StreamReader(mifURL);
                StreamReader midReader = new StreamReader(midURL);
                bar.Maximum = 100;

                // READ mif header
                Char delim;
                int dataCount;
                String[] headerData = readHeader(mifReader);

                bool delimSuccess = char.TryParse(headerData[0], out delim);
                bool dataCountSuccess = int.TryParse(headerData[1], out dataCount);
                if (!delimSuccess)
                    throw new MapInfoFormatException("Mif file Delimiter information in unexpected format");
                if (!dataCountSuccess)
                    throw new MapInfoFormatException("Mif file Column information in unexpected format");

                //TODO: this is hard coded to be the second data item - in our test MapInfo files, this is Elect_div
                int regionNameIndex = 0;
                if (dataCount > 1)
                {
                    regionNameIndex = 1;
                    dataFields.Add("Elect_div");
                }

                // READ mid file - if there is data
                if (dataCount > 0)
                    readMidFile(midReader, delim, regionNameIndex);
                else
                    log.Info("There was no column information in the mif file - not reading mid file");

                // READ mif DATA information
                int regionCount = 0;
                String line = mifReader.ReadLine();
                while (!mifReader.EndOfStream)
                {
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
                    bar.Value = (int)(regionCount / regions.Count * 100);
                }
                //TODO: check return true;
            }
            catch (Exception e)
            {
                //TODO: catch more specific exceptions
                log.Error("Exception occurred while building GeoModel from MapInfo files");
                log.Error(e.Message);
            }
            return regions;
        }

        //Reads info from a mif file until it reaches a line beginning with "data" (case insensitive)
        //Returns : Char[] where:
        // index 0 - the delimiter defined in the header, or the default delimiter if not defined - '\t'
        // index 1 - the number of data items per region in the mid file 
        private String[] readHeader(StreamReader mifReader)
        {
            log.Debug("Reading mif header file");
            String[] data = new String[2];
            data[0] = "\t";
            data[1] = "0";

            String line = mifReader.ReadLine();
            while (!line.Trim().ToUpper().Equals("DATA"))
            {
                if (line.Trim().ToUpper().StartsWith("DELIMITER"))
                {
                    data[0] = line.Substring(line.IndexOf('"') + 1, 1);
                }
                else if (line.Trim().ToUpper().StartsWith("COLUMNS"))
                {
                    String[] lineParts = line.Split(new Char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                    if(lineParts.Length > 0)
                        data[1] = lineParts[1];
                }
                line = mifReader.ReadLine();
            }
            log.Debug("Returning delim : \"" + data[0] + "\"");
            log.Debug("Returning column count : " + data[1]);
            return data;
        }

        //Reads the data from the stream, splitting each line using the delimiter supplied.
        //A region will be created for each line. 
        // The name of each region will be the data item at regionNameIndex on that line
        private void readMidFile(StreamReader midReader, Char delim, int regionNameIndex)
        {
            log.Debug("Reading mid data file");
            String line = midReader.ReadLine();
            while (line != null)
            {
                String[] lineParts = line.Split(delim);
                Region reg = new Region();
                if (regionNameIndex < lineParts.Length)
                {
                    //Remove inverted commas if necessary - mapinfo adds these to string data
                    char[] commas = new char[]{'\"'}; 
                    string name = lineParts[regionNameIndex];
                    name = name.TrimStart(commas);
                    name = name.TrimEnd(commas);
                    reg.regionName = name;
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
            regions[regionIndex].regionType = Region.POLYGON_CODE;

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
                regions[regionIndex].coordinates.Add(coordSet.ToString());
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

        /*        private void ReadRegion(StreamReader mifReader)
        {
            String line;
            String[] lineParts;

            while ((line = mifReader.ReadLine()) != null)
            {
                incrementRead();

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
        }*/

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

                reg.coordinates.Add(coord);
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
                regions[regCount].regionType = Region.PLINE_CODE;

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
            regions[regCount].regionName = "BlackCat PolyLine Obj #" + objCounter++;
            regions[regCount].coordinates.Add(coord);
            //reg.coordinates.Add(coord);

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
                regions[lineCount].regionType = Region.LINE_CODE;


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
            regions[lineCount].regionName = "BlackCat Line Obj #" + objCounter++;
            regions[lineCount].coordinates.Add(coord);

            //reg.coordinates.Add(coord);

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
                regions[ptCount].regionType = Region.POINT_CODE;
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

            //reg.coordinates.Add(coord);

            //16 september
            regions[ptCount].regionName = "BlackCat Point Obj #" + objCounter++;
            regions[ptCount].coordinates.Add(coord);
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
