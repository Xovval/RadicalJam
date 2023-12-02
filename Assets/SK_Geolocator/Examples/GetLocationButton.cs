using SK.GeolocatorWebGL.Models;
using UnityEngine;
using UnityEngine.UI;

namespace SK.GeolocatorWebGL.Examples
{
    public class GetLocationButton : MonoBehaviour
    {
        public Button Button;
        public GeolocationText Text;

        private void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                var options = new PositionOptions
                {
                    enableHighAccuracy = true,
                    maximumAge = 100,
                    timeout = 10_000
                };

                SK_Geolocator.GetCurrentLocation((s) => Text.UpdateText(s), (e) => Text.UpdateText(e), options);
            });
        }
    }
}
