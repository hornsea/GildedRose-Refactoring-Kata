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
        void DegradeAllItems(IList<Item> items);

        void DegradeItem(Item item);

        bool IsAgedBrie(Item item);

        bool IsSulfuras(Item item);
        
        bool IsBackstagePass(Item item);

        bool IsConjured(Item item);

        void DegradeNormalItem(Item item);

        void DegradeAgedBrie(Item item);

        void DegradeSulfuras(Item item);
        
        void DegradeBackstagePass(Item item);

        void DegradeConjured(Item item);
    }
}
