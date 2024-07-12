using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class DeviceLocation : MonoBehaviour {
    
    public Location defaultLoc;

    public Location Current { get; private set; }
    public Location Last { get; private set; }

    public bool Initialized { get { return Input.location.status == LocationServiceStatus.Running
                                    || Application.isEditor; } }

    #region Public Methods

    public bool HasReceivedNewGPSInfo() {
        return !LocationLogic.CompareLatLon(Last, Current);
    }

    public void UpdateNewGPSInfo() {
        Last = Current;
    }

    #endregion

    #region Start

    private void CheckDevicePermission() {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }

    private bool DeviceLocationIsEnabled() {
        bool enabled = Input.location.isEnabledByUser;
        if (!enabled) {
            Debug.Log("Location not enabled. Using default latitude and longitude.");
            Current = defaultLoc;
        }
        return enabled;
    }

    void InitGPSEncoderLocalOrigin() {
        if (GPSEncoderUtil.IsReadyToInitOrigin()) {
            GPSEncoder.SetLocalOrigin(new Vector2(Current.Latitude, Current.Longitude));
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

        } else if (DeviceLocationIsEnabled()) {
            StartCoroutine(InitLocation());
        } else {
            // todo
        }
    }

    void Start() {
        Last = new Location(0, 0, 0);
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

    void Update() {
        GetLocation();
    }

    #endregion

}
