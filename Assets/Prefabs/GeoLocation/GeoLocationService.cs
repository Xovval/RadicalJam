using System;
using System.Collections;
using System.Collections.Generic;
using SK.GeolocatorWebGL;
using SK.GeolocatorWebGL.Examples;
using UnityEngine;
using SK.GeolocatorWebGL.Models;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace SK.GeolocatorWebGL
{
    public class GeoLocationService : MonoBehaviour
    {

        public double latitude = 0.0;

        public double longitude = 0.0;

        public double altitude = 0.0;

        [SerializeField] private GeolocationText _displaytext;

        // Update is called once per frame
        void Update()
        {

        }
        
        private void OnDestroy()
        {
            SK_Geolocator.ClearWatch();
        }

        public void UpdateValues(GeolocationPosition s)
        {
            latitude = s.coords.latitude;
            longitude = s.coords.longitude;

            if (s.coords.altitude != null && s.coords.altitude.HasValue)
            {
                altitude = s.coords.altitude.Value;
            }
            
            Debug.Log("Location: " + latitude + " " + longitude + " " + altitude);
            _displaytext.UpdateText(s);
        }

        /*IEnumerator Start()
        {
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
                Debug.Log("Location not enabled on device or app does not have permission to access location");

            // Starts the location service.
            Input.location.Start(100,100);

            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // If the service didn't initialize in 20 seconds this cancels location service use.
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.LogError("Unable to determine device location");
                yield break;
            }
            else
            {
                // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
                Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
                altitude = Input.location.lastData.altitude;
            }

            // Stops the location service if there is no need to query location updates continuously.
            Input.location.Stop();
        }*/
    }
}