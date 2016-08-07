﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wrox.ProCSharp.LINQ
{
  public static class StringExtension
  {
    public static string FirstName(this string name)
    {
      int ix = name.LastIndexOf(' ');
      return name.Substring(0, ix);
    }
    public static string LastName(this string name)
    {
      int ix = name.LastIndexOf(' ');
      return name.Substring(ix + 1);
    }
  }

  class Program
  {
    static void Main()
    {
      // Filtering();
      // IndexFiltering();
      // TypeFiltering();
      // CompoundFrom();
      // Sorting();
      // Grouping();
      // GroupingWithNestedObjects();
      // InnerJoin();
      // LeftOuterJoin();
      GroupJoin2();
      // SetOperations();
      // Except();
      // ZipOperation();
      // Partitioning();
      // Aggregate();
      // Aggregate2();
      // Untyped();
      // CombineRacers();
      // SelectMany2();
    }

    private static void SelectMany2()
    {
      // flatten the year list to return a list of all racers and positions in the championship 
      var racers = Formula1.GetChampionships()
        .SelectMany(cs => new List<dynamic>()
       {
         new {
           Year = cs.Year,
           Position = 1,
           Name = cs.First          
         },
         new {
           Year = cs.Year,
           Position = 2,
           Name = cs.Second
         },
         new {
           Year = cs.Year,
           Position = 3,
           Name = cs.Third
         }
       });


      foreach (var s in racers)
      {
        Console.WriteLine(s);
      }

    }

    private static void CombineRacers()
    {
      var q = from r in Formula1.GetChampions()
              join r2 in Formula1.GetChampionships().GetRacerInfo() on
              new
              {
                FirstName = r.FirstName,
                LastName = r.LastName
              }
                equals
              new
              {
                FirstName = r2.FirstName,
                LastName = r2.LastName
              }
              into yearResults
              select new
              {
                FirstName = r.FirstName,
                LastName = r.LastName,
                Wins = r.Wins,
                Starts = r.Starts,
                Results = yearResults
              };

      foreach (var item in q)
      {
        Console.WriteLine("{0} {1}", item.FirstName, item.LastName);
        foreach (var item2 in item.Results)
        {
          Console.WriteLine("{0} {1}", item2.Year, item2.Position);
        }
      }
    }

    private static void Except()
    {
      var racers = Formula1.GetChampionships().SelectMany(cs => new List<RacerInfo>()
       {
         new RacerInfo {
           Year = cs.Year,
           Position = 1,
           FirstName = cs.First.FirstName(),
           LastName = cs.First.LastName()
         },
         new RacerInfo {
           Year = cs.Year,
           Position = 2,
           FirstName = cs.Second.FirstName(),
           LastName = cs.Second.LastName()
         },
         new RacerInfo {
           Year = cs.Year,
           Position = 3,
           FirstName = cs.Third.FirstName(),
           LastName = cs.Third.LastName()
         }
       });


      var nonChampions = racers.Select(r =>
        new
        {
          FirstName = r.FirstName,
          LastName = r.LastName
        }).Except(Formula1.GetChampions().Select(r =>
          new
          {
            FirstName = r.FirstName,
            LastName = r.LastName
          }));

      foreach (var r in nonChampions)
      {
        Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
      }

    }

    private static void Sorting()
    {
      var racers = (from r in Formula1.GetChampions()
                    orderby r.Country, r.LastName, r.FirstName
                    select r).Take(10);

      foreach (var racer in racers)
      {
        Console.WriteLine("{0}: {1}, {2}", racer.Country, racer.LastName, racer.FirstName);
      }

    }

    static void Untyped()
    {
      var list = new System.Collections.ArrayList(Formula1.GetChampions() as System.Collections.ICollection);

      var query = from r in list.Cast<Racer>()
                  where r.Country == "USA"
                  orderby r.Wins descending
                  select r;
      foreach (var racer in query)
      {
        Console.WriteLine("{0:A}", racer);
      }

    }

    static void ZipOperation()
    {
      var racerNames = from r in Formula1.GetChampions()
                       where r.Country == "Italy"
                       orderby r.Wins descending
                       select new
                       {
                         Name = r.FirstName + " " + r.LastName
                       };

      var racerNamesAndStarts = from r in Formula1.GetChampions()
                                where r.Country == "Italy"
                                orderby r.Wins descending
                                select new
                                {
                                  LastName = r.LastName,
                                  Starts = r.Starts
                                };


      //var racers = racerNames.Zip(racerNamesAndStarts, (first, second) => first.Name + ", starts: " + second.Starts);
      //foreach (var r in racers)
      //{
      //    Console.WriteLine(r);
      //}

    }

    static void Aggregate2()
    {
      var countries = (from c in
                         from r in Formula1.GetChampions()
                         group r by r.Country into c
                         select new
                         {
                           Country = c.Key,
                           Wins = (from r1 in c
                                   select r1.Wins).Sum()
                         }
                       orderby c.Wins descending, c.Country
                       select c).Take(5);

      foreach (var country in countries)
      {
        Console.WriteLine("{0} {1}", country.Country, country.Wins);
      }

    }

    static void Aggregate()
    {
      var query = from r in Formula1.GetChampions()
                  let numberYears = r.Years.Count()
                  where numberYears >= 3
                  orderby numberYears descending, r.LastName
                  select new
                  {
                    Name = r.FirstName + " " + r.LastName,
                    TimesChampion = numberYears
                  };

      foreach (var r in query)
      {
        Console.WriteLine("{0} {1}", r.Name, r.TimesChampion);
      }
    }

    static void Partitioning()
    {
      int pageSize = 5;

      int numberPages = (int)Math.Ceiling(Formula1.GetChampions().Count() /
            (double)pageSize);

      for (int page = 0; page < numberPages; page++)
      {
        Console.WriteLine("Page {0}", page);

        var racers =
           (from r in Formula1.GetChampions()
            orderby r.LastName, r.FirstName
            select r.FirstName + " " + r.LastName).
           Skip(page * pageSize).Take(pageSize);

        foreach (var name in racers)
        {
          Console.WriteLine(name);
        }
        Console.WriteLine();
      }

    }

    static void SetOperations()
    {
      Func<string, IEnumerable<Racer>> racersByCar =
          car => from r in Formula1.GetChampions()
                 from c in r.Cars
                 where c == car
                 orderby r.LastName
                 select r;

      Console.WriteLine("World champion with Ferrari and McLaren");
      foreach (var racer in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
      {
        Console.WriteLine(racer);
      }
    }


    static void InnerJoin()
    {
      var racers = from r in Formula1.GetChampions()
                   from y in r.Years
                   select new
                   {
                     Year = y,
                     Name = r.FirstName + " " + r.LastName
                   };

      var teams = from t in Formula1.GetContructorChampions()
                  from y in t.Years
                  select new
                  {
                    Year = y,
                    Name = t.Name
                  };

      var racersAndTeams =
            (from r in racers
             join t in teams on r.Year equals t.Year
             orderby t.Year
             select new
             {
               Year = r.Year,
               Champion = r.Name,
               Constructor = t.Name
             }).Take(10);

      Console.WriteLine("Year  World Champion\t   Constructor Title");
      foreach (var item in racersAndTeams)
      {
        Console.WriteLine("{0}: {1,-20} {2}",
           item.Year, item.Champion, item.Constructor);
      }

    }

    static void GroupJoin2()
    {
      var racers = Formula1.GetChampionships()
        .SelectMany(cs => new List<RacerInfo>()
        {
         new RacerInfo {
           Year = cs.Year,
           Position = 1,
           FirstName = cs.First.FirstName(),
           LastName = cs.First.LastName()        
         },
         new RacerInfo {
           Year = cs.Year,
           Position = 2,
           FirstName = cs.Second.FirstName(),
           LastName = cs.Second.LastName()        
         },
         new RacerInfo {
           Year = cs.Year,
           Position = 3,
           FirstName = cs.Third.FirstName(),
           LastName = cs.Third.LastName()        
         }
       });

      var q = (from r in Formula1.GetChampions()
               join r2 in racers on
               new
               {
                 FirstName = r.FirstName,
                 LastName = r.LastName
               }
               equals
               new
               {
                 FirstName = r2.FirstName,
                 LastName = r2.LastName
               }
               into yearResults
               select new
               {
                 FirstName = r.FirstName,
                 LastName = r.LastName,
                 Wins = r.Wins,
                 Starts = r.Starts,
                 Results = yearResults
               });

      foreach (var r in q)
      {
        Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
        foreach (var results in r.Results)
        {
          Console.WriteLine("{0} {1}", results.Year, results.Position);
        }
      }

    }

    static void GroupJoin()
    {
      //  var q =
      //from c in categories
      //join p in products on c equals p.Category into ps
      //select new { Category = c, Products = ps }; 

      var racers = from r in Formula1.GetChampions()
                   from y in r.Years
                   select new
                   {
                     Year = y,
                     Name = r.FirstName + " " + r.LastName
                   };

      var teams = from t in Formula1.GetContructorChampions()
                  from y in t.Years
                  select new
                  {
                    Year = y,
                    Name = t.Name
                  };

      var racersAndTeams =
            from r in racers
            join t in teams on r.Year equals t.Year into ts
            select new
            {
              Year = r.Year,
              Racer = r.Name,
              Constructor = ts
            };

      foreach (var r in racersAndTeams)
      {
        Console.WriteLine("{0} {1}", r.Year, r.Racer);
        foreach (var t in r.Constructor)
        {
          Console.WriteLine("\t{0}", t.Name);
        }
      }
    }

    static void CrossJoinWithGroupJoin()
    {
      //  var q =
      //from c in categories
      //join p in products on c equals p.Category into ps
      //from p in ps
      //select new { Category = c, p.ProductName }; 

    }

    static void LeftOuterJoin()
    {
      var racers = from r in Formula1.GetChampions()
                   from y in r.Years
                   select new
                   {
                     Year = y,
                     Name = r.FirstName + " " + r.LastName
                   };

      var teams = from t in Formula1.GetContructorChampions()
                  from y in t.Years
                  select new
                  {
                    Year = y,
                    Name = t.Name
                  };

      var racersAndTeams =
        (from r in racers
         join t in teams on r.Year equals t.Year into rt
         from t in rt.DefaultIfEmpty()
         orderby r.Year
         select new
         {
           Year = r.Year,
           Champion = r.Name,
           Constructor = t == null ? "no constructor championship" : t.Name
         }).Take(10);

      Console.WriteLine("Year  Champion\t\t   Constructor Title");
      foreach (var item in racersAndTeams)
      {
        Console.WriteLine("{0}: {1,-20} {2}",
           item.Year, item.Champion, item.Constructor);
      }
    }


    static void GroupingWithNestedObjects()
    {
      var countries = from r in Formula1.GetChampions()
                      group r by r.Country into g
                      orderby g.Count() descending, g.Key
                      where g.Count() >= 2
                      select new
                      {
                        Country = g.Key,
                        Count = g.Count(),
                        Racers = from r1 in g
                                 orderby r1.LastName
                                 select r1.FirstName + " " + r1.LastName
                      };
      foreach (var item in countries)
      {
        Console.WriteLine("{0, -10} {1}", item.Country, item.Count);
        foreach (var name in item.Racers)
        {
          Console.Write("{0}; ", name);
        }
        Console.WriteLine();
      }

    }

    static void Grouping()
    {
      var countries = from r in Formula1.GetChampions()
                      group r by r.Country into g
                      orderby g.Count() descending, g.Key
                      where g.Count() >= 2
                      select new
                      {
                        Country = g.Key,
                        Count = g.Count()
                      };

      foreach (var item in countries)
      {
        Console.WriteLine("{0, -10} {1}", item.Country, item.Count);
      }

    }

    static void CompoundFrom()
    {
      var ferrariDrivers = from r in Formula1.GetChampions()
                           from c in r.Cars
                           where c == "Ferrari"
                           orderby r.LastName
                           select r.FirstName + " " + r.LastName;

      foreach (var racer in ferrariDrivers)
      {
        Console.WriteLine(racer);
      }

    }

    static void TypeFiltering()
    {
      object[] data = { "one", 2, 3, "four", "five", 6 };
      var query = data.OfType<string>();
      foreach (var s in query)
      {
        Console.WriteLine(s);
      }

    }

    static void IndexFiltering()
    {
      var racers = Formula1.GetChampions().
              Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
      foreach (var r in racers)
      {
        Console.WriteLine("{0:A}", r);
      }

    }


    static void Filtering()
    {
      var racers = from r in Formula1.GetChampions()
                   where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                   select r;

      foreach (var r in racers)
      {
        Console.WriteLine("{0:A}", r);
      }

    }
  }
}
