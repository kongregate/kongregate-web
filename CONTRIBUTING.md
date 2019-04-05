# Contributing

Due to the way a Unity package is structured, you can't directly open this repo in a Unity project. Instead, you'll have to clone the repository locally and then add it as a local dependency to an existing Unity project:

1. Create a new Unity project (or use an existing one, if you'd prefer to do so).
2. Clone this repository into the `Packages` folder of the Unity project. This should result in a `kongregate-web` folder adjacent to the project's `manifest.json` file.
3. Open `manifest.json` and add "com.kongregate.kongregate-web" as a local dependency:

```json
{
  "dependencies": {
    "com.kongregate.kongregate-web": "file:./kongregate-web"
  }
}
```

You will now be able to make changes to the local copy of the repository and test the changes in the Unity project you just setup. Doing this also allows Unity to generate meta files for any new files you add, which is necessary in order to correctly import the package into other Unity projects.

We appreciate pull requests, bug reports, and feature requests :heart: 