using GildedRoseKata.Services;
using GildedRoseKata.Services.Interfaces;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;
    private readonly IItemService _itemHandler;


    public GildedRose(IList<Item> items, IItemService itemHandler = null)
    {
        _items = items;
        _itemHandler = itemHandler ?? (IItemService)new DefaultItemService();
    }

    public void UpdateQuality()
    {
        _itemHandler.DegradeAllItems(_items);
    }
}


