using UnityEngine;
using UnityEngine.UI;

namespace SK.GeolocatorWebGL.Examples
{
    public class WatchLocationButton : MonoBehaviour
    {
        public Button Button;
        public GeolocationText Text;

        private void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                SK_Geolocator.WatchLocation((s) => Text.UpdateText(s), (e) => Text.UpdateText(e));
            });
        }
    }
}
