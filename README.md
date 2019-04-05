# Kongregate Web API for Unity

This package provides C# bindings to the [Kongregate Web API](https://docs.kongregate.com/v1.0/reference) to be used in developing Unity games that will be published on the Kongregate website.

```c#
KongregateWeb.BecameReady += () =>
{
    if (KongregateWeb.IsGuest)
    {
        Debug.Log("Player is a guest");
    }
    else
    {
        Debug.Log("Player is logged in as " + KongregateWeb.Username);
    }
};

KongregateWeb.LoggedIn += () =>
{
    Debug.Log("Player is logged in as " + KongregateWeb.Username);
};
```

## Setup

To include kongregate-web as a Unity package, you'll need to be on Unity 2018.3 or later. Open `Packages/manifest.json` in your project and add "com.kongregate.kongregate-web" to the "dependencies" object:

```json
{
  "dependencies": {
    "com.kongregate.kongregate-web": "https://github.com/randomPoison/kongregate-web.git"
  }
}
```

> NOTE: You'll need to have Git installed on your development machine for Unity to be able to download the dependency. See https://git-scm.com/ for more information.

> NOTE: If you're using an older version of Unity, you can still use kongregate-web by copying the contents of `Plugins` into your project's `Plugins` folder.

To use kongregate-web, you'll first need to include the Kongregate JavaScript API in your build's generated `index.html` file. To do so, follow the Unity instructions for [setting up a custom WebGL template](https://docs.unity3d.com/Manual/webgl-templates.html), then add a link to `kongregate_api.js` as described in the [Kongregate JavaScript guide](https://docs.kongregate.com/docs/javascript-api#section-loading-the-api).

## Usage

The API is accessible through public, static methods on the `KongregateWeb` class. You never need to access the `KongregateWeb` instance directly.

The API needs to be loaded before it can be accessed. This will happen automatically on `Awake()` when the `KongregateWeb` object is created, however you'll need to wait for `KongregateWeb` to become ready before accessing any of the API functions. There are two ways of doing this:

* `IsReady` can be used to check if the API is ready.
* The `BecameReady` event is emitted once the API becomes ready.

Any attempts to access the API (other than `IsReady`) before it becomes ready will throw an exception. It is always okay to register callbacks on public events, though.

> NOTE: `KongregateWeb` will automatically create its own instance the first time it is accessed, which may mean that the API won't become ready until a later point. To ensure that the API becomes ready as early as possible, you can explicitly add an instance of `KongregateWeb` to your scene file. This will help to reduce the amount of time that your game will have to wait before accessing the Kongregate API.

The `KongregateWeb` class itself provides usage documentation for each of the functions and events. Additionally, the API mirrors the JavaScript API, so you can check the Kongregate docs for more details: https://docs.kongregate.com/v1.0/reference

## Cross-Platform Support

The Kongregate API is only available when building for WebGL, and will only work correctly for a game that has been uploaded to the Kongregate website. Any attempts to access the API on other platforms will behave as if the API isn

## Additional Resources

* [Kongregate Web API Guide](https://docs.kongregate.com/docs/javascript-api)
* [Kongregate Web API Reference](https://docs.kongregate.com/v1.0/reference)
* [Unity WebGL Template Reference](https://docs.unity3d.com/Manual/webgl-templates.html)