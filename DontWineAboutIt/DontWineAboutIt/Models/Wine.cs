﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DontWineAboutIt.Models
{
    public class Wine
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Designation { get; set; }
        public string Points { get; set; }
        public string Price { get; set; }
        public string Province { get; set; }
        public string Region1 { get; set; }
        public string Region2 { get; set; }
        public string Variety { get; set; }
        public string Winery { get; set; }

        public static List<Wine> GetWineList()
        {
            List<Wine> myWine = new List<Wine>();
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\wine.csv"));
            using (StreamReader reader = new StreamReader(newPath))
            {
                int counter = 0;
                string line;

                //only grab the top 1000 wines. 
                while (((line = reader.ReadLine()) != null) && counter < 2000)
                {
                    Regex parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] wineList = parser.Split(line);

                    //Add new wine object to the list. 
                    myWine.Add(new Wine
                    {
                        Id = wineList[0],
                        Country = wineList[1],
                        Description = wineList[2],
                        Designation = wineList[3],
                        Points = wineList[4],
                        Price = wineList[5],
                        Region1 = wineList[6],
                        Region2 = wineList[7],
                        Variety = wineList[8],
                        Winery = wineList[9]
                    });

                    counter++;
                }
            }
            return myWine;
        }

        public static List<Wine> FilterWineList(string price, string pointRating)
        {
            List<Wine> allWines = GetWineList();
            IEnumerable<Wine> filteredByPrice = allWines.Where(w => w.Price == price);
            return filteredByPrice.Where(w => w.Points == pointRating).ToList();
        }
    }
}
