using System;
using System.IO;
using System.Linq;

namespace DataClasses
{
    public class Map
    {
        public const string DEFAULT_CSV_HEADER = "Name,Code,Lat,Long,ImageLink";

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
        public static Map FromCsv(string csvStr,string hdr = DEFAULT_CSV_HEADER)
        {
            var parts = csvStr.SplitCsv().ToArray();
            var header = hdr.Split(',');
            var result = new Map();
            for (int i = 0; i < header.Length; i++)
            {
                switch (header[i].ToUpper())
                {
                    case "NAME":
                        result.Name = parts[i];
                        break;
                    case "CODE":
                        result.Code = parts[i];
                        break;
                    case "LAT":
                        if (!Double.TryParse(parts[i],out result.Lat))
                            throw new FormatException(string.Format("'{0}' is not a valid latitude value.",parts[i]));
                        break;
                    case "LONG":
                        if (!Double.TryParse(parts[i], out result.Long))
                            throw new FormatException(string.Format("'{0}' is not a valid longitude value.",parts[i]));
                        break;
                    case "IMAGELINK":
                        result.ImageLink = parts[i];
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Creates an array of Map instances from a CSV file contents
        /// </summary>
        /// <param name="targetPath">target CSV file path</param>
        /// <returns>Map array</returns>
        public static Map[] FromCsvMulti(string targetPath,bool hasHeader = true)
        {
            var contents = File.ReadAllLines(targetPath);
            var header = contents.First();
            return (from line in contents
                    where !hasHeader || !line.Equals(header)
                    select hasHeader ? FromCsv(line,header) : FromCsv(line)).ToArray();
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
