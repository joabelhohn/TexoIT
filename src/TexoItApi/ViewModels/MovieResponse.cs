using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TexoIt.Api.ViewModels
{
    public class MovieResponse
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public List<string> Studios { get; set; }
        public List<string> Producers { get; set; }
        public bool Winner { get; set; }
    }
    
    public class WinnerResponse
    {
        public List<MovieInterval> Min { get; set; }
        public List<MovieInterval> Max { get; set; }
    }

    public class MovieInterval
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }
}
