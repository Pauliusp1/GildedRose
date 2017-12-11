using GildedRose.Console;
using System.Collections.Generic;
using Xunit;

namespace GildedRose.Tests
{
    public class Tests
    {
        /// <summary>
        /// This test compares if old countings and refactored give the same results after 1 day.
        /// </summary>
        [Fact]
        public void OldCountingsVsRefactored_WithoutConjured_After1day()
        {
            var oldCountingsResult = InitiateData(new List<Item>());
            var refactoredCountingsResult = InitiateData(new List<Item>());
            var gildedRose = new Program();

            UpdateQualityOLD(oldCountingsResult);           
            gildedRose.UpdateQuality(refactoredCountingsResult);
            gildedRose.UpdateSellInValues(refactoredCountingsResult);


            for (int i = 0; i < oldCountingsResult.Count; i++)
            {
                if (oldCountingsResult[i].Name != "Conjured Mana Cake")
                {
                    Assert.Equal(oldCountingsResult[i].Quality, refactoredCountingsResult[i].Quality);
                    Assert.Equal(oldCountingsResult[i].SellIn, refactoredCountingsResult[i].SellIn);
                }
            }
        }

        [Fact]
        public void ConjuredQualityDoubleDecrease_After1day()
        {
            var items = InitiateData(new List<Item>());
            var oldCountingsResult = InitiateData(new List<Item>());
            var newCountingsResult = InitiateData(new List<Item>());
            var gildedRose = new Program();

            UpdateQualityOLD(oldCountingsResult);
            gildedRose.UpdateQuality(newCountingsResult);
            gildedRose.UpdateSellInValues(newCountingsResult);


            for (int i = 0; i < oldCountingsResult.Count; i++)
            {
                if (oldCountingsResult[i].Name == "Conjured Mana Cake" && items[i].Quality > oldCountingsResult[i].Quality)
                {
                    var oldDecrease = items[i].Quality - oldCountingsResult[i].Quality;
                    var newDecrease = items[i].Quality - newCountingsResult[i].Quality;
                    Assert.Equal(oldDecrease * 2, newDecrease);
                }
            }
        }


        [Fact]
        public void OldCountingsVsRefactored_WithoutConjured_After10days()
        {
            var oldCountingsResult = InitiateData(new List<Item>());
            var refactoredCountingsResult = InitiateData(new List<Item>());
            var gildedRose = new Program();

            for (int i = 0; i < 10; i++)
            {
                UpdateQualityOLD(oldCountingsResult);
                gildedRose.UpdateQuality(refactoredCountingsResult);
                gildedRose.UpdateSellInValues(refactoredCountingsResult);
            }

            for (int i = 0; i < oldCountingsResult.Count; i++)
            {
                if (oldCountingsResult[i].Name != "Conjured Mana Cake")
                {
                    Assert.Equal(oldCountingsResult[i].Quality, refactoredCountingsResult[i].Quality);
                    Assert.Equal(oldCountingsResult[i].SellIn, refactoredCountingsResult[i].SellIn);
                }
            }
        }

        [Fact]
        public void ConjuredQualityDoubleDecrease_After10days()
        {
            var items = InitiateData(new List<Item>());
            var oldCountingsResult = InitiateData(new List<Item>());
            var newCountingsResult = InitiateData(new List<Item>());
            var gildedRose = new Program();

            // Array of Conjured quality values. I Will loop through this array to see when and how Conjured qualities decrease.
            int daysChecking = 10;
            int[,] conjuredQualities = new int[daysChecking + 1, 2];
            conjuredQualities[0, 0] = conjuredQualities[0, 1] = items.Find(a => a.Name == "Conjured Mana Cake").Quality;

            for (int i = 1; i < daysChecking + 1; i++)
            {
                UpdateQualityOLD(oldCountingsResult);
                conjuredQualities[i, 0] = oldCountingsResult.Find(a => a.Name == "Conjured Mana Cake").Quality;
                gildedRose.UpdateQuality(newCountingsResult);
                gildedRose.UpdateSellInValues(newCountingsResult);
                conjuredQualities[i, 1] = newCountingsResult.Find(a => a.Name == "Conjured Mana Cake").Quality;
            }

            for (int i = 1; i < daysChecking +1 ; i++)
            {
                if (conjuredQualities[i,0] < conjuredQualities[i-1,0] && conjuredQualities[i - 1, 0] != 0 && conjuredQualities[i - 1, 1] != 0)
                    Assert.Equal((conjuredQualities[i - 1, 0] - conjuredQualities[i, 0]) * 2, conjuredQualities[i -1, 1] - conjuredQualities[i, 1]);
            }
        }

        [Fact]
        public void OldCountingsVsRefactored_WithoutConjured_After100days()
        {
            var oldCountingsResult = InitiateData(new List<Item>());
            var refactoredCountingsResult = InitiateData(new List<Item>());
            var gildedRose = new Program();

            for (int i = 0; i < 100; i++)
            {
                UpdateQualityOLD(oldCountingsResult);
                gildedRose.UpdateQuality(refactoredCountingsResult);
                gildedRose.UpdateSellInValues(refactoredCountingsResult);
            }

            for (int i = 0; i < oldCountingsResult.Count; i++)
            {
                if (oldCountingsResult[i].Name != "Conjured Mana Cake")
                {
                    Assert.Equal(oldCountingsResult[i].Quality, refactoredCountingsResult[i].Quality);
                    Assert.Equal(oldCountingsResult[i].SellIn, refactoredCountingsResult[i].SellIn);
                }
            }
        }

        [Fact]
        public void ConjuredQualityDoubleDecrease_After100days()
        {
            var items = InitiateData(new List<Item>());
            var oldCountingsResult = InitiateData(new List<Item>());
            var newCountingsResult = InitiateData(new List<Item>());
            var gildedRose = new Program();

            // Array of Conjured quality values. I Will loop through this array to see when and how Conjured qualities decrease.
            int daysChecking = 100;
            int[,] conjuredQualities = new int[daysChecking + 1, 2];
            conjuredQualities[0, 0] = conjuredQualities[0, 1] = items.Find(a => a.Name == "Conjured Mana Cake").Quality;

            for (int i = 1; i < daysChecking + 1; i++)
            {
                UpdateQualityOLD(oldCountingsResult);
                conjuredQualities[i, 0] = oldCountingsResult.Find(a => a.Name == "Conjured Mana Cake").Quality;
                gildedRose.UpdateQuality(newCountingsResult);
                gildedRose.UpdateSellInValues(newCountingsResult);
                conjuredQualities[i, 1] = newCountingsResult.Find(a => a.Name == "Conjured Mana Cake").Quality;
            }

            for (int i = 1; i < daysChecking + 1; i++)
            {
                if (conjuredQualities[i, 0] < conjuredQualities[i - 1, 0] && conjuredQualities[i - 1, 0] != 0 && conjuredQualities[i - 1, 1] != 0)
                    Assert.Equal((conjuredQualities[i - 1, 0] - conjuredQualities[i, 0]) * 2, conjuredQualities[i - 1, 1] - conjuredQualities[i, 1]);
            }
        }

        private List<Item> InitiateData(List<Item> Items)
        {
            Items = new List<Item>
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
                                          };

            return Items;
        }

        /// <summary>
        /// This old method (before refactor) that counted products quality and SellIn.
        /// </summary>
        /// <param name="Items"></param>
        private List<Item> UpdateQualityOLD(List<Item> Items)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }

            return Items;
        }
    }
}
