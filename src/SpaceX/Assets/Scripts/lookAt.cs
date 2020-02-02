using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public GameObject target, soundeffect;
    public float timer;
    bool sound = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            if(!sound)
            {
                sound = true;
                Instantiate(soundeffect, this.transform);
            }
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(target.transform.position - this.transform.position, Vector3.up), 0.1f);
        }
    }
}
