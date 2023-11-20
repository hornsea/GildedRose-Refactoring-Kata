using GildedRoseKata.Services;
using GildedRoseKata.Services.Interfaces;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;
    private readonly IItemHandler _itemHandler;

    public GildedRose(IList<Item> items, IItemHandler itemHandler = null)
    {
        _items = items;
        _itemHandler = itemHandler ?? (IItemHandler)new DefaultItemHandler();
    }

    public void UpdateQuality()
    {
        _itemHandler.UpdateAllQualities(_items);
    }
}


