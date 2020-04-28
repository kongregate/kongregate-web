# Kongregate Web API for Unity

![Build and Test](https://github.com/kongregate/kongregate-web/workflows/Build%20and%20Test/badge.svg) [![openupm](https://img.shields.io/npm/v/com.kongregate.kongregate-web?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.kongregate.kongregate-web/) ![Unity 2019.3](https://img.shields.io/badge/Unity-2019.3-blue) ![Unity 2018.4](https://img.shields.io/badge/Unity-2018.4-blue) [![Latest docs](https://img.shields.io/badge/docs-latest-green)](https://kongregate.github.io/kongregate-web/api/)

This package provides C# bindings to the [Kongregate Web API](https://docs.kongregate.com/v1.0/reference) to be used in developing Unity games that will be published on the Kongregate website.

<span style="font-size: 200%;">
>>> <a href="https://kongregate.github.io/kongregate-web/api/">Usage and API documentation</a> <<<
</span>

```csharp
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

## Setup and Usage

To include kongregate-web as a Unity package, you'll need to be on Unity 2018.3 or later. kongregate-web is [hosted on OpenUPM](https://openupm.com/packages/com.kongregate.kongregate-web/). Follow the [OpenUPM getting started guide](https://openupm.com/docs/getting-started.html) if you're not already using it, then run the following command from within your Unity project:

```text
openupm add com.kongregate.kongregate-web
```

> Alternate setup instructions are also available [on the docs site](https://kongregate.github.io/kongregate-web/#setup).

Once you have added the package to your project you'll also need to [setup a custom WebGL template](https://kongregate.github.io/kongregate-web/#setup-custom-webgl-template) to make the Kongregate JavaScript API available to your game.
