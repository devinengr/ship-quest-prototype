using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class DeviceLocation : MonoBehaviour {
    
    public Location defaultLoc;

    public Location Current { get; private set; }

    public bool Initialized { get {
        return GPSEncoderUtil.OriginInitComplete &&
                (Input.location.status == LocationServiceStatus.Running ||
                Application.isEditor ); } }

    public bool DeviceLocationIsEnabled { get {
        return Input.location.isEnabledByUser || Application.isEditor; } }

    #region Start

    private void CheckDevicePermission() {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    void InitGPSEncoderLocalOrigin() {
        if (GPSEncoderUtil.IsReadyToInitOrigin()) {
            GPSEncoderUtil.UpdateOrigin(Current);
            GPSEncoderUtil.OriginInitComplete = true;
        }
    }

    private IEnumerator InitLocation() {
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing) {
            yield return new WaitForSeconds(1);
        }
        GetLocation();
        InitGPSEncoderLocalOrigin(); 
    }

    private void SetLocationBasedOnDevice() {
        if (Application.isEditor) {
            Current = defaultLoc;
            InitGPSEncoderLocalOrigin(); 
        } else if (DeviceLocationIsEnabled) {
            StartCoroutine(InitLocation());
        } else {
            // A popup will close the application. This is handled elsewhere.
            // If the app is to run with a default location, uncomment these two lines
            // to initialize the default location instead:
            // Current = defaultLoc;
            // InitGPSEncoderLocalOrigin(); 
        }
    }

    void Start() {
        CheckDevicePermission();
        SetLocationBasedOnDevice();
    }

    #endregion

    #region Update

    void GetLocation() {
        if (!Application.isEditor) {
            if (Input.location.status != LocationServiceStatus.Running) {
                Debug.LogFormat("Unable to fetch device location with status {0}.", Input.location.status);
                return;
            }
            Current = new Location(
                Input.location.lastData.latitude,
                Input.location.lastData.longitude,
                Input.location.lastData.altitude
            );
        }
    }

    void UpdateGPSOrigin() {
        if (!GPSEncoderUtil.OriginIsUpToDate(Current)) {
            GPSEncoderUtil.UpdateOrigin(Current);
        }
    }

    void Update() {
        LocationLogic.LocationIsInitialized = Initialized;
        GetLocation();
        UpdateGPSOrigin();
    }

    #endregion

}
