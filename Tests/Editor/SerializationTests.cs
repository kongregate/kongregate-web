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

    [Test]
    public static void DeserializeUserItemList()
    {
        var item = JsonUtility.FromJson<UserItem>(UserItem);

        Assert.AreEqual(123, item.Id);
        Assert.AreEqual("cool_item", item.Identifier);
        Assert.AreEqual("some metadata goes here", item.Data);
        Assert.AreEqual(1, item.RemainingUses);
    }
}
