using System.Collections.Generic;
using System.Xml.Linq;
using GildedRoseKata;
using GildedRoseKata.Services;
using GildedRoseKata.Services.Interfaces;
using NUnit.Framework;

namespace GildedRoseTests;

public class ItemValidity
{
    IItemHandler _itemService = new DefaultItemHandler();

    [Test]
    public void ValidNormalItem()
    {
        var itemCheck = _itemService.ValidateItem(new Item { Name = "Armour with a U", Quality = 49, SellIn = 12 });

        Assert.IsTrue(itemCheck.Item1, $"Normal item failure - {itemCheck.Item2}");
    }

    [Test]
    public void ValidSulfurasItem()
    {
        var itemCheck = _itemService.ValidateItem(new Item { Name = "Sulfuras", Quality = 80, SellIn = 1 });

        Assert.IsTrue(itemCheck.Item1, $"Sulfuras item failure - {itemCheck.Item2}");
    }

    [Test]
    public void InvalidItem()
    {
        var itemCheck = _itemService.ValidateItem(new Item { Name = "Cheese", Quality = 80, SellIn = 1 });

        Assert.IsFalse(itemCheck.Item1, $"Invalid item failure - {itemCheck.Item2}");
    }

    [Test]
    public void AllItemsValid()
    {
        var items = new List<Item>
        {   new Item{ Name = "Sulfuras", Quality = 10, SellIn = 1 },
            new Item{ Name = "Chocolate Orange", Quality = 12, SellIn = -1 },
            new Item{ Name = "Magical Amulet", Quality = 1, SellIn = 1 }
        };

        var itemsCheck = _itemService.ValidateAllItems(items);

        Assert.IsTrue(itemsCheck.Item1, "All items should be valid");
    }

    [Test]
    public void ThreeItemsInvalid()
    {
        var items = new List<Item>
        {   new Item{ Name = "Sulfuras", Quality = 80, SellIn = 1 },
            new Item{ Name = "Backpack", Quality = 12, SellIn = 12 },
            new Item{ Name = "Spare backpack", Quality = 51, SellIn = 84 },
            new Item{ Name = "Backpack for life", Quality = 5, SellIn = 10 },
            new Item{ Name = "Mystical Beans", Quality = 60, SellIn = 3 },
            new Item{ Name = "Normal Beans", Quality = 99, SellIn = 1 }
        };

        var itemsCheck = _itemService.ValidateAllItems(items);

        Assert.IsTrue(itemsCheck.Item1 == false && itemsCheck.Item2.Count == 3, "Three items should be invalid");
    }
}