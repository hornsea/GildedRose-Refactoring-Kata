using GildedRoseKata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Services
{
    internal class DefaultItemHandler : IItemHandler
    {
        public void UpdateAllQualities(IList<Item> items)
        {
            foreach (var item in items)
                UpdateQuality(item);
        }

        public void UpdateQuality(Item item)
        {
            if (IsAgedBrie(item))
                UpdateAgedBrie(item);

            else if (IsSulfuras(item))
                UpdateSulfuras(item);

            else if (IsBackstagePass(item))
                UpdateBackstagePass(item);

            else if (IsConjured(item))
                UpdateConjured(item);

            else
                UpdateNormalItem(item);
        }


        public bool IsAgedBrie(Item item) => item.Name == "Aged Brie";
        

        public bool IsSulfuras(Item item) => item.Name.StartsWith("Sulfuras");


        public bool IsBackstagePass(Item item) => item.Name.StartsWith("Backstage pass");

        public bool IsConjured(Item item) => item.Name.StartsWith("Conjured");


        public void UpdateNormalItem(Item item)
        {
            item.SellIn--;

            if (item.Quality > 0)
                item.Quality--;

            if (item.Quality > 0 && item.SellIn < 0)
                item.Quality--;
        }

        public void UpdateAgedBrie(Item item)
        {
            item.SellIn--;
            item.Quality = Math.Min(50, item.Quality + (item.SellIn < 0 ? 2 : 1));
        }

        public void UpdateSulfuras(Item item)
        {
            item.Quality = 80;
        }

        public void UpdateBackstagePass(Item item)
        {
            item.SellIn--;


            if (item.SellIn == 0)
                item.Quality = 50;

            else if (item.SellIn <= 0)
                item.Quality = 0;

            else
            {
                int qualityChange = 1;

                if (item.SellIn < 5)
                    qualityChange = 3;

                else if (item.SellIn < 10)
                    qualityChange = 2;


                item.Quality = Math.Min(50, item.Quality + qualityChange);
            }
        }

        public void UpdateConjured(Item item)
        {
            item.SellIn--;
            item.Quality = Math.Max(0, item.Quality - (item.SellIn < 0 ? 2 : 1));   //TODO - the TEST file is incorrect
        }
    }
}
