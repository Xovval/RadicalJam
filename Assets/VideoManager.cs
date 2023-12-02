using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{

    public GameObject VP;
    public SceneService sceneService;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkOver();
    }
    private void checkOver()
{
       long playerCurrentFrame = VP.GetComponent<VideoPlayer>().frame;
       long playerFrameCount = Convert.ToInt64(VP.GetComponent<VideoPlayer>().frameCount);
     


       if(playerCurrentFrame > 510)
       {
                      sceneService.LoadLoginScene();
       }
       else
       {

       }
}
}
