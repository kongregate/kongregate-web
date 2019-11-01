﻿#if UNITY_WEBGL && !UNITY_EDITOR
#define ENABLE_KONG_API
#endif

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Kongregate.Web
{
    /// <summary>
    /// Interface to the Kongregate web API.
    /// </summary>
    ///
    /// <remarks>
    /// The bindings to the JavaScript API are defined in KongregateWeb.jslib, which
    /// must be included in the project for this class to work.
    /// </remarks>
    [DisallowMultipleComponent]
    public class KongregateWeb : MonoBehaviour
    {
        public enum ApiStatus
        {
            Uninitialized,
            Unavailable,
            Ready
        }

        private static KongregateWeb _instance;
        private ApiStatus _status = ApiStatus.Uninitialized;
        private string _username;
        private int _userId;
        private string _gameAuthToken;

        private bool _adsAvailable = false;
        private bool _adIsOpen = false;

        private Action _onBecameReady;
        private Action _onLoggedIn;
        private Action<string[]> _onPurchaseSucceeded;
        private Action<string[]> _onPurchaseFailed;
        private Action<StoreItem[]> _onItemsReceived;
        private Action<UserItem[]> _onUserItemsReceived;
        private Action<bool> _onAdAvailabilityChanged;
        private Action _onAdOpened;
        private Action<bool> _onAdClosed;

        /// <summary>
        /// Event broadcast when the web API becomes ready.
        /// </summary>
        ///
        /// <remarks>
        /// If the web API is already ready, then the registered callback will be
        /// invoked immediately.
        /// </remarks>
        public static event Action BecameReady
        {
            add
            {
                AssertInstanceExists();

                if (IsReady)
                {
                    value?.Invoke();
                }
                else
                {
                    _instance._onBecameReady += value;
                }
            }

            remove
            {
                AssertInstanceExists();
                _instance._onBecameReady -= value;
            }
        }

        public static event Action LoggedIn
        {
            add
            {
                AssertInstanceExists();
                _instance._onLoggedIn += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onLoggedIn -= value;
            }
        }

        public static event Action<string[]> PurchaseSucceeded
        {
            add
            {
                AssertInstanceExists();
                _instance._onPurchaseSucceeded += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onPurchaseSucceeded -= value;
            }
        }

        public static event Action<string[]> PurchaseFailed
        {
            add
            {
                AssertInstanceExists();
                _instance._onPurchaseFailed += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onPurchaseFailed -= value;
            }
        }

        public static event Action<StoreItem[]> StoreItemsReceived
        {
            add
            {
                AssertInstanceExists();
                _instance._onItemsReceived += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onItemsReceived -= value;
            }
        }

        public static event Action<UserItem[]> UserItemsReceived
        {
            add
            {
                AssertInstanceExists();
                _instance._onUserItemsReceived += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onUserItemsReceived -= value;
            }
        }

        public static event Action<bool> AdAvailabilityChanged
        {
            add
            {
                AssertInstanceExists();
                _instance._onAdAvailabilityChanged += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onAdAvailabilityChanged -= value;
            }
        }

        public static event Action AdOpened
        {
            add
            {
                AssertInstanceExists();
                _instance._onAdOpened += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onAdOpened -= value;
            }
        }

        public static event Action<bool> AdClosed
        {
            add
            {
                AssertInstanceExists();
                _instance._onAdClosed += value;
            }

            remove
            {
                AssertInstanceExists();
                _instance._onAdClosed -= value;
            }
        }

        public static ApiStatus Status
        {
            get
            {
                AssertInstanceExists();
                return _instance._status;
            }
        }

        public static bool IsReady
        {
            get
            {
                AssertInstanceExists();
                return _instance._status == ApiStatus.Ready;
            }
        }

        public static bool IsGuest
        {
            get
            {
                AssertIsReady();
                return isGuest();
            }
        }

        public static string Username
        {
            get
            {
                AssertIsReady();
                return _instance._username;
            }
        }

        public static int UserId
        {
            get
            {
                AssertIsReady();
                return _instance._userId;
            }
        }

        public static string GameAuthToken
        {
            get
            {
                AssertIsReady();
                return _instance._gameAuthToken;
            }
        }

        public static bool AdsAvailable
        {
            get
            {
                AssertIsReady();
                return _instance._adsAvailable;
            }
        }

        public static bool IsAdOpen
        {
            get
            {
                AssertIsReady();
                return _instance._adIsOpen;
            }
        }

        public static void PrivateMessage(string message)
        {
            AssertIsReady();
            privateMessage(message);
        }

        public static void ResizeGame(int width, int height)
        {
            AssertIsReady();
            resizeGame(width, height);
        }

        public static void ShowRegistrationBox()
        {
            AssertIsReady();
            showRegistrationBox();
        }

        public static void ShowKredPurchaseDialog(KredPurchaseType type = KredPurchaseType.Default)
        {
            AssertIsReady();
            switch (type)
            {
                case KredPurchaseType.Offers:
                    showKredPurchaseDialog("offers");
                    break;

                case KredPurchaseType.Mobile:
                    showKredPurchaseDialog("mobile");
                    break;

                default:
                    showKredPurchaseDialog("default");
                    break;
            }
        }

        public static void PurchaseItems(string[] items)
        {
            AssertIsReady();
            purchaseItems(JsonUtility.ToJson(items));
        }

        public static void RequestItemList(string[] tags = null)
        {
            AssertIsReady();
            requestItemList(tags != null ? JsonUtility.ToJson(tags) : null);
        }

        public static void RequestUserItemList(string username = null)
        {
            AssertIsReady();
            requestUserItemList(username);
        }

        public static void InitializeIncentivizedAds()
        {
            AssertIsReady();
            initializeIncentivizedAds();
        }

        public static void ShowIncentivizedAd()
        {
            AssertIsReady();
            showIncentivizedAd();
        }

        public static void SubmitStats(string statisticName, int value)
        {
            AssertIsReady();
            submitStats(statisticName, value);
        }

    #region Unity Lifecycle Methods
        private void Awake()
        {
            // Only allow one instance of the API bridge.
            if (_instance != null)
            {
                UnityEngine.Debug.LogWarning("Removing duplicate Kongregate API GameObject, only one may be active at a time");
                Destroy(gameObject);
                return;
            }

            // Make this object the current instance and ensure that it isn't destroyed
            // on scene loads.
            _instance = this;
            DontDestroyOnLoad(gameObject);

            initKongregateAPI(name);
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    #endregion

    #region Asserts
        private static void AssertInstanceExists()
        {
            if (_instance == null)
            {
                // Create an instance of KongregateWeb.
                //
                // NOTE: _instance is set the Awake() lifecycle method, which will be called
                // automatically as part of creating the GameObject, so we do not need to set it
                // explicitly here.
                new GameObject("KongregateWeb", typeof(KongregateWeb));
            }
        }

        private static void AssertIsReady()
        {
            AssertInstanceExists();

            if (_instance._status != ApiStatus.Ready)
            {
                throw new Exception($"Do not call any methods on {typeof(KongregateWeb).Name} until the Kongregate web API has finished loading");
            }
        }
    #endregion

    #region Callbacks from JS
        private void OnInitSucceeded()
        {
            _status = ApiStatus.Ready;

            if (!isGuest())
            {
                _userId = getUserId();
                _username = getUsername();
                _gameAuthToken = getGameAuthToken();
            }

            _onBecameReady?.Invoke();
            _onBecameReady = null;
        }

        private void OnInitFailed()
        {
            _status = ApiStatus.Unavailable;
        }

        private void OnLogin(string userInfo)
        {
            _userId = getUserId();
            _username = getUsername();
            _gameAuthToken = getGameAuthToken();

            _onLoggedIn?.Invoke();
        }

        private void OnPurchaseItemsSucceeded(string itemsJSON)
        {
            var items = JsonUtility.FromJson<string[]>(itemsJSON);
            _onPurchaseSucceeded?.Invoke(items);
        }

        private void OnPurchaseItemsFailed(string itemsJSON)
        {
            var items = JsonUtility.FromJson<string[]>(itemsJSON);
            _onPurchaseFailed?.Invoke(items);
        }

        private void OnItemList(string itemJSON)
        {
            var items = JsonUtility.FromJson<StoreItem[]>(itemJSON);
            _onItemsReceived?.Invoke(items);
        }

        private void OnUserItems(string itemJSON)
        {
            var items = JsonUtility.FromJson<UserItem[]>(itemJSON);
            _onUserItemsReceived?.Invoke(items);
        }

        private void OnAdsAvailable(int adsAvailable)
        {
            _adsAvailable = adsAvailable != 0;
            _onAdAvailabilityChanged?.Invoke(_adsAvailable);
        }

        private void OnAdOpened()
        {
            _adIsOpen = true;
            _onAdOpened?.Invoke();
        }

        private void OnAdClosed(int completed)
        {
            _adIsOpen = false;
            _onAdClosed?.Invoke(completed != 0);
        }
    #endregion

    #region JS Function Declarations

    #if ENABLE_KONG_API
    [DllImport("__Internal")]
    private static extern void initKongregateAPI(string gameObjectName);

    [DllImport("__Internal")]
    private static extern bool isGuest();

    [DllImport("__Internal")]
    private static extern int getUserId();

    [DllImport("__Internal")]
    private static extern string getUsername();

    [DllImport("__Internal")]
    private static extern string getGameAuthToken();

    [DllImport("__Internal")]
    private static extern void privateMessage(string message);

    [DllImport("__Internal")]
    private static extern void resizeGame(int width, int height);

    [DllImport("__Internal")]
    private static extern void showRegistrationBox();

    [DllImport("__Internal")]
    private static extern void showKredPurchaseDialog(string type);

    [DllImport("__Internal")]
    private static extern void purchaseItems(string itemJSON);

    [DllImport("__Internal")]
    private static extern void requestItemList(string tagsJSON);

    [DllImport("__Internal")]
    private static extern void requestUserItemList(string username);

    [DllImport("__Internal")]
    private static extern void initializeIncentivizedAds();

    [DllImport("__Internal")]
    private static extern void showIncentivizedAd();

    [DllImport("__Internal")]
    private static extern void submitStats(string statisticName, int value);
    #else
        private static void initKongregateAPI(string gameObjectName)
        {
            _instance._status = ApiStatus.Unavailable;
        }

        private static bool isGuest()
        {
            return true;
        }

        private static int getUserId()
        {
            return 0;
        }

        private static string getUsername()
        {
            return null;
        }

        private static string getGameAuthToken()
        {
            return null;
        }

        private static void privateMessage(string message) { }
        private static void resizeGame(int width, int height) { }
        private static void showRegistrationBox() { }
        private static void showKredPurchaseDialog(string type) { }
        private static void purchaseItems(string itemJSON) { }
        private static void requestItemList(string tagsJSON) { }
        private static void requestUserItemList(string username) { }
        private static void initializeIncentivizedAds() { }
        private static void showIncentivizedAd() { }
        private static void submitStats(string statisticName, int value) { }
    #endif

    #endregion
    }

}
