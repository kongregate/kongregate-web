# Setting Up kongregate-web

This site includes usage instructions for the kongregate-web package. [Full API documentation is available here as well](./api/index.html).

## Setup

To include kongregate-web as a Unity package, you'll need to be on Unity 2018.3 or later. You have a few options for how you add the package to your project, though adding via OpenUPM is the recommended method.

### Via OpenUPM

kongregate-web is [hosted on OpenUPM](https://openupm.com/packages/com.kongregate.kongregate-web/). Follow the [OpenUPM getting started guide](https://openupm.com/docs/getting-started.html) if you're not already using it, then run the following command from within your Unity project:

```text
openupm add com.kongregate.kongregate-web
```

### Via Git

If you can't use OpenUPM for whatever reason, you can also add kongregate-web as a Git dependency. This requires Unity 2020.1 or later. Open `Packages/manifest.json` in your project and add "com.kongregate.kongregate-web" to the "dependencies" object:

```json
{
  "dependencies": {
    "com.kongregate.kongregate-web": "https://github.com/kongregate/kongregate-web.git?path=/com.kongregate.kongregate-web#v0.1.0"
  }
}
```

> NOTE: You'll need to have Git installed on your development machine for Unity to be able to download the dependency. See https://git-scm.com/ for more information.

## Setup Custom WebGL Template

To use kongregate-web, you'll first need to include the Kongregate JavaScript API in your build's generated `index.html` file. To do so, follow the Unity instructions for [setting up a custom WebGL template](https://docs.unity3d.com/Manual/webgl-templates.html), then add a link to `kongregate_api.js` as described in the [Kongregate JavaScript guide](https://docs.kongregate.com/docs/javascript-api#section-loading-the-api).


## Additional Resources

* [Kongregate Web API Guide](https://docs.kongregate.com/docs/javascript-api)
* [Kongregate Web API Reference](https://docs.kongregate.com/v1.0/reference)
* [Unity WebGL Template Reference](https://docs.unity3d.com/Manual/webgl-templates.html)
