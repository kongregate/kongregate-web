using Kongregate.Web;
using NUnit.Framework;
using UnityEngine;

public static class SerializationTests
{
    private static readonly string UserItem = @"{
        ""id"": 123,
        ""identifier"": ""cool_item"",
        ""data"": ""some metadata goes here"",
        ""remaining_uses"": 1
    }";

    private static readonly string StoreItem = @"{
        ""id"": 123,
        ""identifier"": ""cool_item"",
        ""name"": ""Cool Item"",
        ""description"": ""An item that is cool"",
        ""price"": 170,
        ""tags"": [""tag""],
        ""image_url"": ""https://example.com/image.png""
    }";

    [Test]
    public static void DeserializeUserItem()
    {
        var item = JsonUtility.FromJson<UserItem>(UserItem);

        Assert.AreEqual(123, item.Id);
        Assert.AreEqual("cool_item", item.Identifier);
        Assert.AreEqual("some metadata goes here", item.Data);
        Assert.AreEqual(1, item.RemainingUses);
    }

    [Test]
    public static void DeserializeStoreItem()
    {
        var item = JsonUtility.FromJson<StoreItem>(StoreItem);

        Assert.AreEqual(123, item.Id);
        Assert.AreEqual("cool_item", item.Identifier);
        Assert.AreEqual("Cool Item", item.Name);
        Assert.AreEqual("An item that is cool", item.Description);
        Assert.AreEqual(170, item.Price);
        Assert.AreEqual(new string[] { "tag" }, item.Tags);
        Assert.AreEqual("https://example.com/image.png", item.ImageUrl);
    }
}
