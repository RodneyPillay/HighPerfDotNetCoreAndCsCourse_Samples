﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _02_03_Demo1_PerfTips
{
    public class HistoricalPriceReader
    {
        public IEnumerable<HistoricalQuote> GetHistoricalQuotes(String Symbol)
        {
            Symbol = Symbol.Replace('.', '_');
            var path = Symbol + ".csv";
            var file = System.IO.File.OpenRead(path);
            using (var reader = new System.IO.StreamReader(file))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var items = line.Split(';');
                    var date = items[0].Split('-');
                    //Console.WriteLine($"{date[0]}, {date[1]}, {date[2]}");
                    yield return new HistoricalQuote
                    {
                        Date = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2])),
                        Close = Decimal.Parse(items[1].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                        High = Decimal.Parse(items[2].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                        Low = Decimal.Parse(items[3].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                        Open = Decimal.Parse(items[4].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                        Volume = long.Parse(items[5].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture)
                    };
                }
            }
        }
    }
}
