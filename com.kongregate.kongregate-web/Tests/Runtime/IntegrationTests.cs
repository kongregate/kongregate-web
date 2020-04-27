using Kongregate.Web;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
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

    private static readonly string UserItemJson = @"{
        ""success"": true,
        ""data"": [
            {
            ""id"": 123,
            ""identifier"": ""cool_item"",
            ""data"": ""some metadata goes here"",
            ""remaining_uses"": 1
            }
        ]
    }";

    private GameObject _kongWebInstance = null;

    [OneTimeSetUp]
    public void Setup()
    {
        // Create a fresh instance of KongregateWeb object.
        _kongWebInstance = new GameObject("KongregateWeb", typeof(KongregateWeb));

        // Simulate the JavaScript API being successfully initialized.
        _kongWebInstance.SendMessage("OnInitSucceeded");
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        GameObject.Destroy(_kongWebInstance);
    }

    [Test]
    public void TestInitialization()
    {
        Assert.AreEqual(ApiStatus.Ready, KongregateWeb.Status);
    }

    [Test]
    public void TestStoreItems()
    {
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
        _kongWebInstance.SendMessage("OnItemList", StoreItemJson);
        Assert.IsTrue(storeItemsReceived);
    }

    [Test]
    public void TestUserItems()
    {
        bool userItemsRecieved = false;
        KongregateWeb.UserItemsReceived += items =>
        {
            userItemsRecieved = true;
            Assert.AreEqual(
                new UserItem[]
                {
                    new UserItem(123, "cool_item", "some metadata goes here", 1),
                },
                items);
        };

        KongregateWeb.RequestUserItemList();
        _kongWebInstance.SendMessage("OnUserItems", UserItemJson);
        Assert.IsTrue(userItemsRecieved);
    }
}
