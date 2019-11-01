using Kongregate.Web;
using NUnit.Framework;
using UnityEngine;

public class IntegrationTests
{
    [Test]
    public static void TestInitialization()
    {
        var instance = new GameObject("KongregateWeb", typeof(KongregateWeb));

        // When running in the editor, the API should be treated as unavailable.
        Assert.AreEqual(ApiStatus.Unavailable, KongregateWeb.Status);

        // Simulate the JavaScript API being successfully initialized.
        instance.SendMessage("OnInitSucceeded");

        Assert.AreEqual(ApiStatus.Ready, KongregateWeb.Status);

        GameObject.Destroy(instance);
    }
}
