using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smplMov : MonoBehaviour
{
    [SerializeField]
    private float xSpeed, ySpeed, zSpeed;
    private Vector3 movement;
    // Start is called before the first frame update
    private void Start()
    {
        movement = new Vector3(xSpeed, ySpeed, zSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Time.deltaTime * Input.GetAxis("Vertical") * movement);
    }
}
