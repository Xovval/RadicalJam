using UnityEngine;
using UnityEngine.UI;

namespace SK.GeolocatorWebGL.Examples
{
    public class WatchLocationButton : MonoBehaviour
    {
        public Button Button;
        public GeolocationText Text;
        public GeoLocationService gls;

        private void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                SK_Geolocator.WatchLocation((s) => gls.UpdateValues(s), (e) => Text.UpdateText(e));
            });
        }
    }
}
