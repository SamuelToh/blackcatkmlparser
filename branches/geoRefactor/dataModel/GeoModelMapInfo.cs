using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
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

            // READ mif region information
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

    }
}
