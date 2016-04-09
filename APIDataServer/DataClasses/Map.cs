using System;
using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Map
    {
        public string Name;
        public string Code;
        public double Lat;
        public double Long;
        public string ImageLink;

        /// <summary>
        /// Creates a new Map instance from CSV text
        /// </summary>
        /// <param name="csvStr">CSV text</param>
        /// <returns>instance of Map</returns>
        public static Map FromCsv(string csvStr)
        {
            var parts = csvStr.Split(',');
            return new Map
            {
                Name = parts[0],
                Code = parts[1],
                Lat = Double.Parse(parts[2]),
                Long = Double.Parse(parts[3]),
                ImageLink = parts[4],
            };
        }

        /// <summary>
        /// Creates an array of Map instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Map array</returns>
        public static Map[] FromCsvMulti(string targetPath)
        {
            return (from line in File.ReadAllLines(targetPath)
                    select FromCsv(line)).ToArray();
        }

        /// <summary>
        /// Serializes instance to CSV text
        /// </summary>
        /// <returns>CSV text</returns>
        public string ToCsv()
        {
            return string.Format("{0},{1},{2},{3},{4}",
                Name,
                Code,
                Lat,
                Long,
                ImageLink);
        }
    }
}
