using System.Collections.Generic;
using System.Xml.Linq;
using GildedRoseKata;
using GildedRoseKata.Services;
using GildedRoseKata.Services.Interfaces;
using NUnit.Framework;

namespace GildedRoseTests;

public class ItemDegredation
{
    IItemService _itemService = new DefaultItemService();

    [Test]
    public void NormalItemDegredation()
    {
        var item = new Item { Name = "Armor without a U", Quality = 3, SellIn = 12 };

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 2, $"Quality is {item.Quality} - should be 2");
        Assert.AreEqual(item.SellIn, 11, $"SellIn is {item.SellIn} - should be 11");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 1, $"Quality is {item.Quality} - should be 1");
        Assert.AreEqual(item.SellIn, 10, $"SellIn is {item.SellIn} - should be 10");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 0, $"Quality is {item.Quality} - should be 0");
        Assert.AreEqual(item.SellIn, 9, $"SellIn is {item.SellIn} - should be 9");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 0, $"Quality is {item.Quality} - should be 0");
        Assert.AreEqual(item.SellIn, 8, $"SellIn is {item.SellIn} - should be 8");
    }



    [Test]
    public void BrieDegredation()
    {
        var item = new Item { Name = "Aged Brie", Quality = 48, SellIn = 12 };

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 49, $"Quality is {item.Quality} - should be 2");
        Assert.AreEqual(item.SellIn, 11, $"SellIn is {item.SellIn} - should be 11");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 50, $"Quality is {item.Quality} - should be 1");
        Assert.AreEqual(item.SellIn, 10, $"SellIn is {item.SellIn} - should be 10");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 50, $"Quality is {item.Quality} - should be 0");
        Assert.AreEqual(item.SellIn, 9, $"SellIn is {item.SellIn} - should be 9");

    }

    [Test]
    public void SulfurasDegredation()
    {
        var item = new Item { Name = "Sulfuras", Quality = 48, SellIn = 12 };

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 80, $"Quality is {item.Quality} - should be 80");
        Assert.AreEqual(item.SellIn, 12, $"SellIn is {item.SellIn} - should be 12");

    }

    [Test]
    public void BackstagePassesDegredation()
    {
        var item = new Item { Name = "Backstage passes for Wizrd Weatherwax", Quality = 5, SellIn = 11 };

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 6, $"Quality is {item.Quality} - should be 6");
        Assert.AreEqual(item.SellIn, 10, $"SellIn is {item.SellIn} - should be 10");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 8, $"Quality is {item.Quality} - should be 8");
        Assert.AreEqual(item.SellIn, 9, $"SellIn is {item.SellIn} - should be 9");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 10, $"Quality is {item.Quality} - should be 10");
        Assert.AreEqual(item.SellIn, 8, $"SellIn is {item.SellIn} - should be 8");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 12, $"Quality is {item.Quality} - should be 12");
        Assert.AreEqual(item.SellIn, 7, $"SellIn is {item.SellIn} - should be 7");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 14, $"Quality is {item.Quality} - should be 14");
        Assert.AreEqual(item.SellIn, 6, $"SellIn is {item.SellIn} - should be 6");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 16, $"Quality is {item.Quality} - should be 16");
        Assert.AreEqual(item.SellIn, 5, $"SellIn is {item.SellIn} - should be 5");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 19, $"Quality is {item.Quality} - should be 19");
        Assert.AreEqual(item.SellIn, 4, $"SellIn is {item.SellIn} - should be 4");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 22, $"Quality is {item.Quality} - should be 22");
        Assert.AreEqual(item.SellIn, 3, $"SellIn is {item.SellIn} - should be 3");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 25, $"Quality is {item.Quality} - should be 25");
        Assert.AreEqual(item.SellIn, 2, $"SellIn is {item.SellIn} - should be 2");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 28, $"Quality is {item.Quality} - should be 28");
        Assert.AreEqual(item.SellIn, 1, $"SellIn is {item.SellIn} - should be 1");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 50, $"Quality is {item.Quality} - should be 50");
        Assert.AreEqual(item.SellIn, 0, $"SellIn is {item.SellIn} - should be 0");

        _itemService.DegradeItem(item);
        Assert.AreEqual(item.Quality, 0, $"Quality is {item.Quality} - should be 0");
        Assert.AreEqual(item.SellIn, -1, $"SellIn is {item.SellIn} - should be -1");
    }

    [Test]
    public void ConjuredDegredation()
    {
         var item = new Item { Name = "Egg Sarnie", Quality = 49, SellIn = 12 };

        Assert.IsFalse(_itemService.IsSulfuras(item), $"Item {item.Name} is Sulfuras");
    }



}