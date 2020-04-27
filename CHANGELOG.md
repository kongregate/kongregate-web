# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [v0.2.0]

### Improvements

* Remove dependency on Json.NET. ([#11])

### Breaking Changes

* Rename `KongregateUserItem` to `UserItem`. ([#11])
* Rename `KongregateStoreItem` to `StoreItem`. ([#11])
* Changed `UserItem` and `StoreItem` to be structs instead of classes. This may require that you update logic that was checking an item against `null`. ([#11])
* Moved `ApiStatus` out of `KongregateWeb`. ([#11])

### Internal Improvements

* Add unit and integrations tests. ([#11])
* Setup projects to test against Unity 2018.4 and 2019.3. ([#14])
* Run tests and builds against supported Unity versions using GitHub Actions. ([#15])

[#11]: https://github.com/kongregate/kongregate-web/pull/11
[#14]: https://github.com/kongregate/kongregate-web/pull/14
[#15]: https://github.com/kongregate/kongregate-web/pull/15

## [v0.1.0] - 2019-08-21

Initial release, adds `KongregateWeb` class, `KongregateWeb.jslib`, bindings to core parts of the Kongregate JavaScript API, and basic usage instructions.

[Unreleased]: https://github.com/kongregate/kongregate-web/compare/v0.2.0...HEAD
[v0.2.0]: https://github.com/kongregate/kongregate-web/compare/v0.1.0...v0.2.0
[v0.1.0]: https://github.com/kongregate/kongregate-web/compare/f97322f...v0.1.0
