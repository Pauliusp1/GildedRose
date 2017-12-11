using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        IList<Item> items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

            };


            System.Console.WriteLine("Insert a number of days to count:");

            int daysToCount = 0;
            Int32.TryParse(System.Console.ReadLine(), out daysToCount);

            for (int i = 0; i < daysToCount; i++)
            {
                app.items = app.UpdateQuality(app.items);
                app.items = app.UpdateSellInValues(app.items);
            }

            foreach (var item in app.items)
                System.Console.WriteLine(item.Name + " / " + item.Quality + " / " + item.SellIn);

            System.Console.ReadKey();


        }

        public IList<Item> UpdateQuality(IList<Item> items)
        {
            foreach (var item in items)
            {
                if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    
                    if (item.SellIn < 0)
                    {
                       item.Quality = 0;
                       continue;
                    }

                    if (item.Quality < 50)
                    {
                        item.Quality++;
                        if (item.Quality < 50 && item.SellIn <= 10)
                            item.Quality++;
                        if (item.Quality < 50 && item.SellIn <= 5)
                            item.Quality++;
                    }
                    continue;
                }

                if (item.Name == "Aged Brie")
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;
                        if (item.SellIn < 1 && item.Quality < 50)
                            item.Quality++;
                    }
                    continue;
                }

                if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.Quality = item.Name == "Conjured Mana Cake" ? item.Quality - 2 : item.Quality - 1;
                    if (item.SellIn < 1)
                        item.Quality = item.Name == "Conjured Mana Cake" ? item.Quality - 2 : item.Quality - 1;

                    if (item.Quality < 0)
                        item.Quality = 0;
                }
            }
            return items;
        }

        public IList<Item> UpdateSellInValues(IList<Item> items)
        {
            foreach (var item in items)
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn = item.SellIn - 1;
                }

            return items;
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
