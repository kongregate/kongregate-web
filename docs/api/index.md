# Usage

The API is accessible through public, static methods on the [`KongregateWeb`] class. You never need to access the [`KongregateWeb`] instance directly.

The API needs to be loaded before it can be accessed. This will happen automatically on `Awake()` when the [`KongregateWeb`] object is created, however you'll need to wait for [`KongregateWeb`] to become ready before accessing any of the API functions. There are two ways of doing this:

* [`IsReady`] can be used to check if the API is ready.
* The [`BecameReady`] event is emitted once the API becomes ready.

Any attempts to access the API (other than [`IsReady`]) before it becomes ready will throw an exception. It is always okay to register callbacks on public events, though.

> NOTE: [`KongregateWeb`] will automatically create its own instance the first time it is accessed, which may mean that the API won't become ready until a later point. To ensure that the API becomes ready as early as possible, you can explicitly add an instance of [`KongregateWeb`] to your scene file. This will help to reduce the amount of time that your game will have to wait before accessing the Kongregate API.

The [`KongregateWeb`] class itself provides usage documentation for each of the functions and events. Additionally, the API mirrors the JavaScript API, so you can check the Kongregate docs for more details: https://docs.kongregate.com/v1.0/reference

## Cross-Platform Support

The Kongregate API is only available when building for WebGL, and will only work correctly for a game that has been uploaded to the Kongregate website. When deploying your game to another website, or when testing builds locally, you can use `KongregateWeb.Status` to check the availability of the Kongregate JavaScript API. If you have not yet initialized the web API (by creating the [`KongregateWeb`] instance) it will return `ApiStatus.Uninitialized`. Once you have initialized the API, it will return `ApiStatus.IsReady` if the JavaScript API is present, or `ApiStatus.Unavailable` if initialization failed. `KongregateWeb.Ready` will also return `false` if the API is unavailable.

[`KongregateWeb`]: ./Kongregate.Web.KongregateWeb.html
[`IsReady`]: ./Kongregate.Web.KongregateWeb.html#Kongregate_Web_KongregateWeb_IsReady
[`BecameReady`]: ./Kongregate.Web.KongregateWeb.html#Kongregate_Web_KongregateWeb_IsReady
