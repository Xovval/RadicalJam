using System.Text;
using SK.GeolocatorWebGL.Models;
using UnityEngine;
using UnityEngine.UI;

namespace SK.GeolocatorWebGL.Examples
{
    public class GeolocationText : MonoBehaviour
    {
        public Text Text;

        public void UpdateText(GeolocationPosition position)
        {
            var str = new StringBuilder();
            str.AppendLine($"Accuracy: {position.coords.accuracy}");
            str.AppendLine($"Altitude: {position.coords.altitude}");
            str.AppendLine($"Altitude Accuracy: {position.coords.altitudeAccuracy}");
            str.AppendLine($"Heading: {position.coords.heading}");
            str.AppendLine($"Latitude: {position.coords.latitude}");
            str.AppendLine($"Longitude: {position.coords.longitude}");

            Text.text = str.ToString();
        }

        public void UpdateText(GeolocationPositionError error)
        {
            Text.text = error.message;
        }
    }
}