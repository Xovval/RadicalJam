using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateManager : MonoBehaviour
{
    public UnityEvent updateData;

    [SerializeField] private float updateInterval = 30.0f;
    [SerializeField] private bool running;
    
    // Start is called before the first frame update
    void Start()
    {
        running = true;
        StartCoroutine( UpdateMe() );
    }

    IEnumerator UpdateMe()
    {
        while( running )
        {
            yield return new WaitForSeconds( updateInterval * Time.deltaTime );
            updateData.Invoke();
        }
    }
}
