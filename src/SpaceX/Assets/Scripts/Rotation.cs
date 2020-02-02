using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationX, rotationY, rotationZ;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(Time.deltaTime * rotationX, Time.deltaTime * rotationY, Time.deltaTime * rotationZ), Space.Self);
    }
}
