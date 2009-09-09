using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;

namespace BlackCat
{
    //this class stores the [ region ] information found in both mid and mif files
    public class GeographModel : IGeographModel
    {
        //data members
        private int dataCount;
        private String[] dataHeaders;
		private char dataDelim;
        private List<Region> regions;
        private ILog log;
        
        //Constructors
        public GeographModel()
        {
            initialiseLogging();
        }

        //private methods
        private void initialiseLogging()
        {
            BasicConfigurator.Configure();  
            log = LogManager.GetLogger( typeof( GeographModel ) );
        }

        //public methods
        public void buildGeographModel(StreamReader midReader, StreamReader mifReader)
        {
            log.Debug("Beginning the build process");
			String line;
			String[] lineParts;
			//Read header information
            dataDelim = '\t'; //tab is the default delimiter
			while(!(line = mifReader.ReadLine()).ToUpper().StartsWith("COLUMNS")) //Version
			{
				if(line.ToUpper().StartsWith("DELIMITER"))
				{
					lineParts = line.Split('"');
					dataDelim = Char.Parse(lineParts[lineParts.Length-1]);
				}
			}
			
			//Column info
			lineParts = line.Split(' ');
            dataCount = int.Parse(lineParts[1]);
			dataHeaders = new String[dataCount];
			for(int i = 0 ; i < dataHeaders.Length ; i++)
			{
				line = mifReader.ReadLine();
				lineParts = line.Split(' ');
				dataHeaders[i] = lineParts[0];
			}
			
			//Data Section
			while((line = mifReader.ReadLine()) != null)
			{
				if(line.ToUpper().StartsWith("REGION"))
				{
					lineParts = line.Split(' ');					
					ReadRegion(mifReader, int.Parse(lineParts[lineParts.Length - 1]));
				}
			}
            mifReader.Close();
        }
		
		private void ReadRegion(StreamReader mifReader,int polyCount)
		{
            log.Debug("Reading region");
            String line;
            String[] lineParts;
            Region region = new Region(dataCount, polyCount);
            for (int i = 0; i < polyCount; i++)
            {
                int coordCount = int.Parse(mifReader.ReadLine());
                Coordinate[] polygon = new Coordinate[coordCount];
                for (int j = 0; j < coordCount; j++)
                {
                    line = mifReader.ReadLine();
                    lineParts = line.Split(' ');
                    float x = float.Parse(lineParts[0]);
                    float y = float.Parse(lineParts[1]);
                    polygon[j] = new Coordinate(x, y);
                }
                log.Debug("Adding polygon to region");
                region.addPolygon(polygon);
            }
		}
        
		///
		///Base class for all MapInfo elements
		///
        private abstract class MapElement
        {
            //data members
            private String[] dataValues;

            //constructors
            public MapElement(int dataCount)
            {
                this.dataValues = new String[dataCount];
            }

            //properties
            public String[] DataValues
            {
                get { return dataValues; }
                set { dataValues = value; }
            }

            //methods
            public String getDataValue(int index)
            {
                if (index < dataValues.Length)
                    return dataValues[index];
                return null;
            }
        }

        private class Region : MapElement
        {
            //data members
            private Coordinate[][] coords; //TODO: better desc - first index is polygon, second is coordinate
            private int currentPoly = 0;

            //constructors
            public Region(int dataCount, int polygonCount)
                : base(dataCount)
            {
                coords = new Coordinate[polygonCount][];
            }    
        
            //methods
            public void addPolygon(Coordinate[] coords)
            {
                this.coords[currentPoly] = coords;
                currentPoly++;
                if(currentPoly == coords.GetLength(0))
                    currentPoly--;
            }
        }

        private class Coordinate
        {
            private float x;
            private float y;

            public Coordinate(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public float X
            { 
                get { return this.x; } 
                set { this.x = value; } 
            }

            public float Y
            {
                get { return this.y; }
                set { this.y = value; }
            }
        }
    }
}
