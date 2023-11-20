using System.Collections.Generic;
using System.Xml.Linq;
using GildedRoseKata;
using GildedRoseKata.Services;
using GildedRoseKata.Services.Interfaces;
using NUnit.Framework;

namespace GildedRoseTests;

public class ItemIdentity
{
    IItemHandler _itemService = new DefaultItemHandler();

    [Test]
    public void IsNormalItem()
    {
        var item = new Item { Name = "Armor without a U", Quality = 49, SellIn = 12 };

        Assert.IsFalse(_itemService.IsAgedBrie(item) ||
                        _itemService.IsSulfuras(item) ||
                        _itemService.IsBackstagePass(item) ||
                        _itemService.IsConjured(item),
                        "Item is not an normal item");
    }



    [Test]
    public void IsAgedBrie()
    {
        var item = new Item { Name = "Aged Brie", Quality = 49, SellIn = 12 };

        Assert.IsTrue(_itemService.IsAgedBrie(item), $"Item {item.Name} is not Aged Brie");
    }

    [Test]
    public void IsNotAgedBrie()
    {
         var item = new Item { Name = "Crackin bit of Wensleydale", Quality = 49, SellIn = 12 };

        Assert.IsFalse(_itemService.IsAgedBrie(item), $"Item {item.Name} is Aged Brie");
    }

    [Test]
    public void IsSulfuras()
    {
         var item = new Item { Name = "Sulfuras", Quality = 49, SellIn = 12 };

        Assert.IsTrue(_itemService.IsSulfuras(item), $"Item {item.Name} is not Sulfuras");
    }

    [Test]
    public void IsNotSulfuras()
    {
         var item = new Item { Name = "Egg Sarnie", Quality = 49, SellIn = 12 };

        Assert.IsFalse(_itemService.IsSulfuras(item), $"Item {item.Name} is Sulfuras");
    }

    [Test]
    public void IsBackstagePass()
    {
         var item = new Item { Name = "Backstage passes to see Merlin in Concert", Quality = 49, SellIn = 12 };

        Assert.IsTrue(_itemService.IsBackstagePass(item), $"Item {item.Name} is not a backstage pass");
    }

    [Test]
    public void IsNotBackstagePass()
    {
        var item = new Item { Name = "Backstreet Boys Album", Quality = 49, SellIn = 12 };

        Assert.IsFalse(_itemService.IsBackstagePass(item), $"Item {item.Name} is a backstage pass");
    }

    [Test]
    public void IsConjured()
    {
        var item = new Item { Name = "Conjured Magical Toaster", Quality = 49, SellIn = 12 };

        Assert.IsTrue(_itemService.IsConjured(item), $"Item {item.Name} is not Conjored");
    }

    [Test]
    public void IsNotConjured()
    {
        var item = new Item { Name = "Non-magical wand", Quality = 49, SellIn = 12 };

        Assert.IsFalse(_itemService.IsConjured(item), $"Item {item.Name} is Conjured");
    }


}