var LibraryKongregate = {
	$instance: {
		kongregate: null,
		gameObjectName: null,

		sendMessage: function (methodName, message) {
			SendMessage(instance.gameObjectName, methodName, message);
		}
	},

	$stringToBuffer: function (value) {
		// Convert the JS string into a UTF-8 buffer and return it.
		var bufferSize = lengthBytesUTF8(value) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(value, buffer, bufferSize);
		return buffer;
	},

	initKongregateAPI: function (gameObjectName) {
		// Save the name of the Unity GameObject that we will send messages to.
		instance.gameObjectName = Pointer_stringify(gameObjectName);

		kongregateAPI.loadAPI(function () {
			instance.kongregate = kongregateAPI.getAPI();

			// Register callbacks for the various events that the Kongregate API can emit.
			instance.kongregate.services.addEventListener('login', function () {
				instance.sendMessage('OnLogin');
			});

			instance.kongregate.mtx.addEventListener('adsAvailable', function () {
				instance.sendMessage('OnAdsAvailable', true);
			});

			instance.kongregate.mtx.addEventListener('adsUnavailable', function () {
				instance.sendMessage('OnAdsAvailable', false);
			});

			instance.kongregate.mtx.addEventListener('adOpened', function () {
				instance.sendMessage('OnAdOpened');
			});

			instance.kongregate.mtx.addEventListener('adCompleted', function () {
				instance.sendMessage('OnAdCompleted');
			});

			instance.kongregate.mtx.addEventListener('adAbandoned', function () {
				instance.sendMessage('OnAdAbandoned');
			});

			// Notify the game that the Kongregate API is ready.
			instance.sendMessage('OnInitSucceeded');
		});
	},

	isGuest: function () {
		return instance.kongregate.services.isGuest();
	},

	getUserId: function () {
		return instance.kongregate.services.getUserId();
	},

	getUsername: function () {
		// Get the username from the Kongregate API.
		var username = instance.kongregate.services.getUsername();
		return stringToBuffer(username);
	},

	getGameAuthToken: function () {
		// Get the token from the Kongregate API.
		var token = instance.kongregate.services.getGameAuthToken();
		return stringToBuffer(token);
	},

	privateMessage: function (message) {
		instance.kongregate.services.privateMessage(Pointer_stringify(message));
	},

	resizeGame: function (width, height) {
		instance.kongregate.services.resizeGame(width, height);
	},

	showRegistrationBox: function () {
		instance.kongregate.services.showRegistrationBox();
	},

	showKredPurchaseDialog: function (type) {
		instance.kongregate.mtx.showKredPurchaseDialog(type);
	},

	purchaseItems: function (itemsJSON) {
		var items = JSON.parse(Pointer_stringify(itemsJSON));
		instance.kongregate.mtx.purchaseItems(items, function (result) {
			if (result.success) {
				instance.sendMessage('OnPurchaseItemsSucceeded', itemsJSON);
			} else {
				instance.sendMessage('OnPurchaseItemsFailed', itemsJSON);
			}
		});
	},

	requestItemList: function (tagsJSON) {
		// TODO: Do we need to explicitly handle if `tagsJSON` is null?
		var tags = JSON.parse(Pointer_stringify(tagsJSON));
		instance.kongregate.mtx.requestItemList(tags, function (result) {
			if (result.success) {
				instance.sendMessage('OnItemList', JSON.stringify(result.data));
			} else {
				// TODO: What do we do if there's an error?
			}
		});
	},

	requestUserItemList: function (username) {
		// TODO: Do we need to explicitly handle if `username` is null?
		username = Pointer_stringify(username);
		instance.kongregate.mtx.requestUserItemList(username, function (result) {
			if (result.success) {
				instance.sendMessage('OnUserItems', JSON.stringify(result.data));
			} else {
				// TODO: What do we do if there's an error?
			}
		});
	},

	initializeIncentivizedAds: function () {
		instance.kongregate.mtx.initializeIncentivizedAds();
	},

	showIncentivizedAd: function () {
		instance.kongregate.mtx.showIncentivizedAd();
	},

	submitStats: function (statistic_name, value) {
		instance.kongregate.stats.submit(Pointer_stringify(statistic_name), value);
	},
};

autoAddDeps(LibraryKongregate, '$instance');
autoAddDeps(LibraryKongregate, '$stringToBuffer');
mergeInto(LibraryManager.library, LibraryKongregate);