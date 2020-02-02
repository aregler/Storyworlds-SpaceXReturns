using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_manager : MonoBehaviour
{
    private int counter = -100;
    public GameObject[] lights;
    private bool[] on;
    private int[] startingTime;
    [SerializeField]
    private float intensity;

    // Start is called before the first frame update
    void Start()
    {
        on = new bool[lights.Length];
        startingTime = new int[lights.Length];
        for(int i=0; i<startingTime.Length; i++)
        {
            startingTime[i] = (int)Random.Range(10, 50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        for (int i=0; i<lights.Length; i++)
        {
            if(startingTime[i] == counter)
            {
                if(on[i])
                {
                    on[i] = false;
                    lights[i].transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                    lights[i].transform.GetChild(1).GetComponent<Renderer>().enabled = false;
                    lights[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Light>().intensity = 0;
                    startingTime[i] = counter + 1 +(int)Random.Range(0, 40);
                } else
                {
                    on[i] = true;
                    lights[i].transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                    lights[i].transform.GetChild(1).GetComponent<Renderer>().enabled = true;
                    lights[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Light>().intensity = intensity;
                    startingTime[i] = counter + (int)Random.Range(-30, 10);
                }
            }
        }
    }
}
