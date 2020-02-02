using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaining : MonoBehaviour
{
    // Start is called before the first frame update
    public float offsetX, offsetY, offsetZ;
    public int iterations;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(offsetX, offsetY, offsetZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] createClones()
    {
        GameObject[] res = new GameObject[iterations];
        for(int i=0; i<iterations; i++)
        {
            res[i] = Instantiate(this.gameObject, this.gameObject.transform.position +  (i-1) * offset, this.gameObject.transform.rotation);
        }
        return res;
    }
}
