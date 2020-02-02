using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript4shadows : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().receiveShadows = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
