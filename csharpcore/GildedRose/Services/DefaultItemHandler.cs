using GildedRoseKata.Services.Interfaces;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml.Linq;


namespace GildedRoseKata.Services
{
    /// <summary>
    /// Default Gilded Rose Kata Item Handler
    /// 
    /// Without altering the Item class the alternative to service sependancy would be possibly
    /// using extension methods to handle the Item updates (plus many more esoteric solutions).
    /// 
    /// The default handler obeys the Item handling rules and will produce the results as per
    /// the standard acceptable test output
    /// 
    /// Default Item degradation rules:
    /// 
    ///	- All items have a SellIn value which denotes the number of days we have to sell the item
	/// - All items have a Quality value which denotes how valuable the item is
	/// - At the end of each day our system lowers both values for every item
    ///
    /// - Once the sell by date has passed, Quality degrades twice as fast
	/// - The Quality of an item is never negative
	/// - "Aged Brie" actually increases in Quality the older it gets
	/// - The Quality of an item is never more than 50
	/// - "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	/// - "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
	///              Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
	///              Quality drops to 0 after the concert
    /// "Conjured" items degrade in Quality twice as fast as normal items
    ///
    /// Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
    /// legendary item and as such its Quality is 80 and it never alters.
    /// 
    /// </summary>
    public class DefaultItemHandler : IItemHandler
    {

        /// <summary>
        /// Validate all Items in the list are valid. 
        /// This will be the limits on Quality
        /// </summary>
        /// <param name="items">List of all items</param>
        /// <returns>List of tuple Item, string for invalid items. String will be error description</returns>
        public (bool, IList<(Item, string)>) ValidateAllItems(IList<Item> items)
        {
            IList<(Item, string)> errors = new List<(Item, string)>();

            foreach(var item in items)
            {
                var validation = ValidateItem(item);
                if (!validation.Item1)
                    errors.Add((item, validation.Item2));
            }

            return (errors.Count == 0, errors);
        }
        

        /// <summary>
        /// Validate an item. Cehck quality falls within correct range
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>Tuple (bool, string) for valid and error (where applicable)</returns>
        public (bool, string) ValidateItem(Item item)
        {
            if (item.Quality < 0)
                return (false, "Quality cannot be negative");

            if (item.Quality > 50 && !IsSulfuras(item))
                return (false, "Quality cannot be > 50");

            return (true, null);
        }


        /// <summary>
        /// A day has passed - update the SellIn and Quality values for all the Items
        /// </summary>
        /// <param name="items">List of the Items</param>
        public void DegradeAllItems(IList<Item> items)
        {
            foreach (var item in items)
                DegradeItem(item);
        }

        /// <summary>
        /// Update the Quality and SellIn for a specific Item
        /// </summary>
        /// <param name="item"></param>
        public void DegradeItem(Item item)
        {
            if (IsAgedBrie(item))
                DegradeAgedBrie(item);

            else if (IsSulfuras(item))
                DegradeSulfuras(item);

            else if (IsBackstagePass(item))
                DegradeBackstagePass(item);

            else if (IsConjured(item))
                DegradeConjured(item);

            else
                DegradeNormalItem(item);
        }


        /// <summary>
        /// Check if the item is aged Brie. Extend this if rule become complicated.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>TRUE if this is aged Brie</returns>
        public bool IsAgedBrie(Item item) => item.Name == "Aged Brie";
        
        /// <summary>
        /// Check if the item is sulfuras. Extend this if rule become complicated.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>TRUE if this is sulfurus</returns>
        public bool IsSulfuras(Item item) => item.Name.StartsWith("Sulfuras");

        /// <summary>
        /// Check if the item is a backstage pass. Extend this if rule become complicated.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>TRUE if this is a backstage pass</returns>
        public bool IsBackstagePass(Item item) => item.Name.StartsWith("Backstage pass");

        /// <summary>
        /// Check if the item is a conjured item. Extend this if rule become complicated.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>TRUE if this is conjured</returns>
        public bool IsConjured(Item item) => item.Name.StartsWith("Conjured");

        /// <summary>
        /// Degrade a normal item
        /// Normal items quality degrade by one a day
        /// Should SellIn be negative they degrade twice as fast
        /// Quality can never be negative
        /// </summary>
        /// <param name="item">Item to degrade</param>
        public void DegradeNormalItem(Item item)
        {
            item.SellIn--;

            item.Quality = Math.Max(0, item.Quality - (item.SellIn < 0 ? 2 : 1));
        }

        /// <summary>
        /// Degrade Aged Brie
        /// Aged brie actually increases in quality by one every day to a maximum of 50
        /// </summary>
        /// <param name="item">Item to degrade</param>
        public void DegradeAgedBrie(Item item)
        {
            item.SellIn--;
            item.Quality = Math.Min(50, item.Quality + (item.SellIn < 0 ? 2 : 1));
        }

        /// <summary>
        /// Degrade Sulfuras
        /// Sulfuras never degrades and its quality is always 80
        /// </summary>
        /// <param name="item">Item to degrade</param>
        public void DegradeSulfuras(Item item)
        {
            item.Quality = 80;
        }

        /// <summary>
        /// Degrade a backstage pass
        /// These increase in quality be one every day (quality maxes out at 50)...
        /// ...in the last ten days of sale they increase in quality by 2
        /// ...in the last fice days they increase by 3
        /// ...on the last day of sale they are worth 50
        /// ...after the concert they are worthless.
        /// </summary>
        /// <param name="item">Item to degrade</param>
        public void DegradeBackstagePass(Item item)
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

        /// <summary>
        /// Degrade a conhured item
        /// The specification states that the conjured items degrade twice as fast as normal items
        /// The original accepted test file does NOT reflect this. 
        /// </summary>
        /// <param name="item">Item to degrade</param>
        public void DegradeConjured(Item item)
        {
            item.SellIn--;
            item.Quality = Math.Max(0, item.Quality - (item.SellIn < 0 ? 4 : 2));
        }

    }
}
