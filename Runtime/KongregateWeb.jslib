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

	$parseJSON: function (stringPointer) {
		var stringValue = Pointer_stringify(stringPointer);
		if (stringValue.trim()) {
			return JSON.parse(stringValue);
		}
	},

	isKongregateAPIAvailable: function () {
		return (typeof kongregateAPI !== 'undefined');
	},

	initKongregateAPI: function (gameObjectName) {
		// Save the name of the Unity GameObject that we will send messages to.
		instance.gameObjectName = Pointer_stringify(gameObjectName);

		if (typeof kongregateAPI === 'undefined') {
			return;
		}

		kongregateAPI.loadAPI(function () {
			instance.kongregate = kongregateAPI.getAPI();

			// Register callbacks for the various events that the Kongregate API can emit.
			instance.kongregate.services.addEventListener('login', function () {
				instance.sendMessage('OnLogin');
			});

			instance.kongregate.mtx.addEventListener('adsAvailable', function () {
				instance.sendMessage('OnAdsAvailable', 1);
			});

			instance.kongregate.mtx.addEventListener('adsUnavailable', function () {
				instance.sendMessage('OnAdsAvailable', 0);
			});

			instance.kongregate.mtx.addEventListener('adOpened', function () {
				instance.sendMessage('OnAdOpened');
			});

			instance.kongregate.mtx.addEventListener('adCompleted', function () {
				instance.sendMessage('OnAdClosed', 1);
			});

			instance.kongregate.mtx.addEventListener('adAbandoned', function () {
				instance.sendMessage('OnAdClosed', 0);
			});

			// Notify the game that the Kongregate API is ready.
			instance.sendMessage('OnInitSucceeded');
		});
	},

	isGuest: function () {
		if (instance.kongregate !== null) {
			return instance.kongregate.services.isGuest();
		}
		else {
			return true;
		} 
	},

	getUserId: function () {
		if (instance.kongregate !== null) {
			return instance.kongregate.services.getUserId();
		}
		else {
			return 0;
		} 
	},

	getUsername: function () {
		if (instance.kongregate !== null) {
			// Get the username from the Kongregate API.
			var username = instance.kongregate.services.getUsername();
			return stringToBuffer(username);
		}
		else {
			return null;
		} 
	},

	getGameAuthToken: function () {
		if (instance.kongregate !== null) {
			// Get the token from the Kongregate API.
			var token = instance.kongregate.services.getGameAuthToken();
			return stringToBuffer(token);
		}
		else {
			return null;
		} 
	},

	privateMessage: function (message) {
		if (instance.kongregate !== null) {
			instance.kongregate.services.privateMessage(Pointer_stringify(message));
		}
	},

	resizeGame: function (width, height) {
		if (instance.kongregate !== null) {
			instance.kongregate.services.resizeGame(width, height);
		}
	},

	showRegistrationBox: function () {
		if (instance.kongregate !== null) {
			instance.kongregate.services.showRegistrationBox();
		}
	},

	showKredPurchaseDialog: function (type) {
		if (instance.kongregate !== null) {
			instance.kongregate.mtx.showKredPurchaseDialog(type);
		}
	},

	purchaseItems: function (itemsJSON) {
		if (instance.kongregate !== null) {
			var items = parseJSON(itemsJSON);
			instance.kongregate.mtx.purchaseItems(items, function (result) {
				if (result.success) {
					instance.sendMessage('OnPurchaseItemsSucceeded', JSON.stringify(items));
				} else {
					instance.sendMessage('OnPurchaseItemsFailed', JSON.stringify(items));
				}
			});
		}
	},

	requestItemList: function (tagsJSON) {
		if (instance.kongregate !== null) {
			var tags = parseJSON(tagsJSON);
			instance.kongregate.mtx.requestItemList(tags, function (result) {
				if (result.success) {
					instance.sendMessage('OnItemList', JSON.stringify(result.data));
				} else {
					// TODO: What do we do if there's an error?
				}
			});
		}	
	},

	requestUserItemList: function (username) {
		if (instance.kongregate !== null) {
			// TODO: Do we need to explicitly handle if `username` is null?
			username = Pointer_stringify(username);
			instance.kongregate.mtx.requestUserItemList(username, function (result) {
				if (result.success) {
					instance.sendMessage('OnUserItems', JSON.stringify(result.data));
				} else {
					// TODO: What do we do if there's an error?
				}
			});
		}
	},

	initializeIncentivizedAds: function () {
		if (instance.kongregate !== null) {
			instance.kongregate.mtx.initializeIncentivizedAds();
		}
	},

	showIncentivizedAd: function () {
		if (instance.kongregate !== null) {
			instance.kongregate.mtx.showIncentivizedAd();
		}
	},

	submitStats: function (statistic_name, value) {
		if (instance.kongregate !== null) {
			instance.kongregate.stats.submit(Pointer_stringify(statistic_name), value);
		}
	},
};

autoAddDeps(LibraryKongregate, '$instance');
autoAddDeps(LibraryKongregate, '$stringToBuffer');
autoAddDeps(LibraryKongregate, '$parseJSON');
mergeInto(LibraryManager.library, LibraryKongregate);