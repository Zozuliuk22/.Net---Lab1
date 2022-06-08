using System;
using BuildingsCollection.Enums;

namespace BuildingsCollection
{
    public class House
    {
        public string Code { get; set; }

        public ProjectTypes ProjectType { get; set; }

        public int FloatsNumber { get; set; }

        public int EntrencesNumber { get; set; }

        //DataTime -> DateTimeOffset
        public DateTimeOffset CreationDate { get; set; }

        public override string ToString()
        {
            return $"{Code} - {ProjectType} - {CreationDate.Year}";
        }
    }
}
