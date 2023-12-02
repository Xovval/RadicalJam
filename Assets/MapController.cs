using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public AbstractMap map; // Reference to the AbstractMap component
    public GeoLocationService geoLocationService; // Reference to the GeoLocationService

    void Start()
    {

        // Initial map update
        UpdateMapLocation(geoLocationService.latitude, geoLocationService.longitude);
    }

    void Update()
    {
        // Optionally, update the map location every frame (may not be efficient)
        //UpdateMapLocation(geoLocationService.latitude, geoLocationService.longitude);
    }

    public void UpdateMapLocation(double latitude, double longitude)
    {
        var location = new Vector2d(latitude, longitude);
        map.SetCenterLatitudeLongitude(location);
        map.UpdateMap();
    }
}