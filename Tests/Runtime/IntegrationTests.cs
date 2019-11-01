using Kongregate.Web;
using NUnit.Framework;
using UnityEngine;

public class IntegrationTests
{
    private static readonly string StoreItemJson = @"{
        ""success"": true,
        ""data"": [
            {
                ""id"": 123,
                ""identifier"": ""cool_item"",
                ""name"": ""Cool Item"",
                ""description"": ""An item that is cool"",
                ""price"": 170,
                ""tags"": [""tag""],
                ""image_url"": ""https://example.com/image.png""
            }
        ]
    }";

    [Test]
    public static void TestInitialization()
    {
        var instance = new GameObject("KongregateWeb", typeof(KongregateWeb));

        // When running in the editor, the API should be treated as unavailable.
        Assert.AreEqual(ApiStatus.Unavailable, KongregateWeb.Status);

        // Simulate the JavaScript API being successfully initialized.
        instance.SendMessage("OnInitSucceeded");

        Assert.AreEqual(ApiStatus.Ready, KongregateWeb.Status);

        // Simulate the flow for RequestItemsList.
        bool storeItemsReceived = false;
        KongregateWeb.StoreItemsReceived += items =>
        {
            storeItemsReceived = true;
            Assert.AreEqual(
                new StoreItem[]
                {
                    new StoreItem(
                        123,
                        "cool_item",
                        "Cool Item",
                        "An item that is cool",
                        170,
                        new string[] { "tag" },
                        "https://example.com/image.png"),
                },
                items);
        };

        KongregateWeb.RequestItemList();
        instance.SendMessage("OnItemList", StoreItemJson);
        Assert.IsTrue(storeItemsReceived);

        GameObject.Destroy(instance);
    }
}
