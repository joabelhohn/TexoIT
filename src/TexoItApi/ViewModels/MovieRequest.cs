using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TexoIt.Api.ViewModels
{
    public class MovieRequest
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public List<string> Studios { get; set; }
        public List<string> Producers { get; set; }
        public bool Winner { get; set; }
    }
    public class MovieRequestConf : ClassMap<MovieRequest>
    {
        public MovieRequestConf()
        {
            Map(p => p.Year).Name("year");
            Map(p => p.Title).Name("title");
            Map(p => p.Studios).Name("studios").TypeConverter<SplitConverter>();
            Map(p => p.Producers).Name("producers").TypeConverter<SplitConverter>();
            Map(p => p.Winner).Name("winner").TypeConverter<WinnerConverter>();
            
        }
    }

    public class WinnerConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text == "yes";
        }
    }
    public class SplitConverter : DefaultTypeConverter
    {
        Regex regex = new Regex(",| and ");
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return regex.Split(text).Select(p => p.Trim()).ToList();
        }
    }
}
