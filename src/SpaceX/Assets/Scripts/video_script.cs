using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class video_script : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().loadNextScene();
        }
        if(timer <= -4)
        {
            Application.Quit();
        }
    }
}
