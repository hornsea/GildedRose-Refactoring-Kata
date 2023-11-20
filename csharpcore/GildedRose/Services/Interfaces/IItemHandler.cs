using GildedRoseKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Services.Interfaces
{
    public interface IItemHandler
    {
        void UpdateAllQualities(IList<Item> items);

        void UpdateQuality(Item item);

        bool IsAgedBrie(Item item);

        bool IsSulfuras(Item item);
        
        bool IsBackstagePass(Item item);

        bool IsConjured(Item item);

        void UpdateNormalItem(Item item);

        void UpdateAgedBrie(Item item);

        void UpdateSulfuras(Item item);
        
        void UpdateBackstagePass(Item item);

        void UpdateConjured(Item item);
    }
}
