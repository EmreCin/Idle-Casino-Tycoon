using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine.Android;
//using Firebase.Crashlytics;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;

           
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Initialize Firebase
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    var dependencyStatus = task.Result;
        //    if (dependencyStatus == Firebase.DependencyStatus.Available)
        //    {
        //        // Create and hold a reference to your FirebaseApp,
        //        // where app is a Firebase.FirebaseApp property of your application class.
        //        // Crashlytics will use the DefaultInstance, as well;
        //        // this ensures that Crashlytics is initialized.
        //        Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

        //        // When this property is set to true, Crashlytics will report all
        //        // uncaught exceptions as fatal events. This is the recommended behavior.
        //        Crashlytics.ReportUncaughtExceptionsAsFatal = true;

        //        // Set a flag here for indicating that your project is ready to use Firebase.
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError(System.String.Format(
        //          "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //        // Firebase Unity SDK is not safe to use here.
        //    }
        //});
    }

    #region Events
    public void LogGenericEvent(string eventName)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
    }

    public void LogGenericEventWithParameter(string eventName, Dictionary<string, string> parameter)
    {
        Parameter[] parameterList = new Parameter[parameter.Count];
        int count = 0;

        foreach (var paramItem in parameter)
        {
            parameterList[count] = new Parameter(paramItem.Key, paramItem.Value);
            count++;
        }
        new Parameter("Test", 1);
;        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, parameterList);
    }
    public void LogGenericEventWithIntParameter(string eventName, Dictionary<string, int> parameter)
    {
        Parameter[] parameterList = new Parameter[parameter.Count];
        int count = 0;

        foreach (var paramItem in parameter)
        {
            parameterList[count] = new Parameter(paramItem.Key, paramItem.Value);
            count++;
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, parameterList);
    }
    #endregion Events

    #region Notification
    //public async UniTask CreateToken()
    //{
    //   await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    //    {
    //        Debug.LogError("FirebaseController - CreateToken ");
    //        var app = FirebaseApp.DefaultInstance;
    //        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
    //        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

    //        Firebase.Messaging.FirebaseMessaging.GetTokenAsync().ContinueWithOnMainThread(tokenTask =>
    //        {
    //            if (tokenTask.IsFaulted || tokenTask.IsCanceled)
    //            {
    //                Debug.LogError("FirebaseController - Failed to get FCM token: " + tokenTask.Exception);
    //            }
    //            else
    //            {
    //                string token = tokenTask.Result;
    //                Debug.Log("FirebaseController - FCM token manually retrieved: " + token);
    //                PlayerPrefs.SetString("FirebaseNotificationToken", token);
    //            }
    //        });
    //    });

    //}






    //public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    PlayerPrefs.SetString("FirebaseNotificationToken", token.Token);
    //    //Debug.LogError("FirebaseController - Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    //{
    //    Debug.LogError("FirebaseController - Received a new message from: " + e.Message.From);
    //}

    //public string GetNotificationToken()
    //{
    //    var notificationToken = PlayerPrefs.GetString("FirebaseNotificationToken", "");
    //    Debug.LogError("FirebaseController - CURRENT TOKEN:" + notificationToken);
    //    return notificationToken;
    //}

    //public async UniTask CreateAndGetToken()
    //{
    //    Debug.LogError("FirebaseController - Create And Get Token ");

    //    var app = FirebaseApp.DefaultInstance;
    //    await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    //    {
    //        //Debug.LogError("FirebaseController - CreateToken ");

    //        FirebaseApp.Create();

    //        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
    //        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

    //        Firebase.Messaging.FirebaseMessaging.GetTokenAsync().ContinueWithOnMainThread(tokenTask =>
    //        {
    //            if (tokenTask.IsFaulted || tokenTask.IsCanceled)
    //            {
    //                Debug.LogError("FirebaseController - Failed to get FCM token: " + tokenTask.Exception);
    //            }
    //            else
    //            {
    //                string token = tokenTask.Result;
    //                Debug.Log("FirebaseController - FCM token manually retrieved: " + token);
    //                PlayerPrefs.SetString("FirebaseNotificationToken", token);
    //            }
    //        });
    //    });
    //    //    .ContinueWithOnMainThread(task => {

    //    //    var token = PlayerPrefs.GetString("FirebaseNotificationToken", "");
    //    //    Debug.LogError("FirebaseController ------------------> CURRENT TOKEN:" + token);

    //    //});


    //}

    //public void RequestPushNotificationPermission()
    //{
    //    Debug.Log("FirebaseController - RequestPushNotificationPermission");


    //    AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.BIND_JOB_SERVICE");
    //    if (result == AndroidRuntimePermissions.Permission.Granted)
    //        Debug.Log("We have permission to read from external storage!");
    //    else
    //        Debug.Log("Permission state: " + result);

    //    // Requesting ACCESS_FINE_LOCATION and CAMERA permissions simultaneously
    //    //AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions( "android.permission.ACCESS_FINE_LOCATION", "android.permission.CAMERA" );
    //    //if( result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted )
    //    //	Debug.Log( "We have all the permissions!" );
    //    //else
    //    //	Debug.Log( "Some permission(s) are not granted..." );

    //    //Firebase.Messaging.FirebaseMessaging.RequestPermissionAsync().ContinueWith(task =>
    //    //{
    //    //    if (task.IsCanceled)
    //    //    {
    //    //        Debug.Log("FirebaseController - Push Notification request is canceled.");
    //    //        return;
    //    //    }

    //    //    if (task.IsFaulted)
    //    //    {
    //    //        Debug.Log("FirebaseController - Push Notification request is faulted.");
    //    //        return;
    //    //    }

    //    //    // Push Notification permission granted.
    //    //    Debug.Log("FirebaseController - Push Notification permission is granted.");
    //    //});
    //    //  Firebase.Messaging.FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
    //    //  task => {
    //    //      Debug.LogError("FirebaseController - "+ task.Status+  " RequestPermissionAsync");
    //    //  }
    //    //);

    //    //if (!Permission.HasUserAuthorizedPermission("android.permission.WAKE_LOCK"))
    //    //{
    //    //Debug.LogError("FirebaseController - android.permission.WAKE_LOCK" );
    //    //   Permission.RequestUserPermission("android.permission.WAKE_LOCK");

    //    //Debug.LogError("FirebaseController - android.permission.POST_NOTIFICATIONS");
    //    //Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
    //    //}

    //    //if (Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
    //    //{
    //    //    // The user authorized use of the microphone.
    //    //    Debug.Log("FirebaseController - authorized use");
    //    //}
    //    //else
    //    //{
    //    //    bool useCallbacks = false;
    //    //    if (!useCallbacks)
    //    //    {
    //    //        // We do not have permission to use the microphone.
    //    //        // Ask for permission or proceed without the functionality enabled.
    //    //        Debug.Log("FirebaseController - Ask for permission or proceed without the functionality enabled.");
    //    //        Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
    //    //    }
    //    //    else
    //    //    {
    //    //        var callbacks = new PermissionCallbacks();
    //    //        //callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
    //    //        //callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
    //    //        //callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
    //    //        Debug.Log("FirebaseController - callbacks");
    //    //        Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS", callbacks);
    //    //    }
    //    //}

    //}
    #endregion Notification


}
