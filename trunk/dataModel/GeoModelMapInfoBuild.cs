using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;

namespace BlackCat
{
    public partial class GeoModel
    {
        public bool BuildGeoModel(string midURL, string mifURL, ProgressBar bar)
        {
            regions = new List<Region>();

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
                regionNameIndex = 1;

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
                }
                line = mifReader.ReadLine();
                //Remove Pen, Brush, Center information - if it is there
                while (line.Trim().ToUpper().StartsWith("PEN") || line.Trim().ToUpper().StartsWith("BRUSH") || line.Trim().ToUpper().StartsWith("CENTER"))
                {
                    line = mifReader.ReadLine();
                }

                //increment bar 
                bar.Value = (int)(regionCount / regions.Count * 100);
            }
            return true;
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
                if(regionNameIndex < lineParts.Length)
                    reg.regionName = lineParts[regionNameIndex];
                else
                    throw new MapInfoMismatchException("Data in .mif header does not match data in .mid file");
                regions.Add(reg);
                line = midReader.ReadLine();
            }
        }

        private void readMapInfoRegion(StreamReader mifReader, int polygonCount, int regionIndex)
        {
            log.Debug("Reading mapinfo region, index " + regionIndex + " polyCount " + polygonCount);
            //Create new region if necessary
            if (regions[regionIndex] == null)
                regions[regionIndex] = new Region();

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
                for (int i = 0; i < coordCount; i++)
                {
                    regions[regionIndex].coordinates.Add(mifReader.ReadLine());
                }
                currentPoly++;
            }
                
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

        private void ReadPLINE(StreamReader mifReader, int plineCount)
        {
            string coord = "\n" + RAW_INDENTATION;
            mifReader.ReadLine();

            Region reg = new Region(PLINE_CODE);

            for (int i = 0; i < plineCount; i++)
            {
                //incrementRead();
                //Not last item
                if (i != plineCount - 1)
                    coord += mifReader.ReadLine().
                            Replace(' ', ',') + " \n"
                            + RAW_INDENTATION;
                //else last item we ignore the pen object   
            }

            reg.coordinates.Add(coord);

            this.regions.Add(reg);

        }

        private void ReadLine(string[] lineData)
        {
            string coord = "\n" + RAW_INDENTATION;

            Region reg = new Region(LINE_CODE);

            for (int i = 1; i < lineData.Length; i++)
            {
                //incrementRead();

                if (i % 2 == 0)
                    coord += lineData[i]
                                + "\n" + RAW_INDENTATION;
                else
                    coord += lineData[i] + ",";

            }
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
            //incrementRead();
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

        private StreamReader getReader
                (String fileUrl, bool isMapFile)
        {
            return new StreamReader(fileUrl);
        }

    }
}
