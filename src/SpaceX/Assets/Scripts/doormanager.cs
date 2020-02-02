using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doormanager : MonoBehaviour
{
    public GameObject door;
    public Text actionText;
    private bool isDisplayingText = false, playerInRange = false;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        if(open)
            door.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            if(open)
            {
                door.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                open = false;
            } else
            {
                door.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                open = true;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isDisplayingText)
            {
                StartCoroutine(fadeTextToFullAlpha(1.0f));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
                StartCoroutine(fadeTextToZeroAlpha(1.0f));
        }

    }
    IEnumerator fadeTextToFullAlpha(float timer)
    {
        actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 0);
        actionText.text = "F zum Öffnen";
        while (actionText.color.a < 1.0f)
        {
            actionText.color = new Color(actionText.color.r,
            actionText.color.g,
            actionText.color.b,
            actionText.color.a + (Time.deltaTime / timer));
            yield return null;
        }
        isDisplayingText = true;
    }

    IEnumerator fadeTextToZeroAlpha(float timer)
    {
        actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 1);
        while (actionText.color.a > 0.0f)
        {
            actionText.color = new Color(actionText.color.r,
            actionText.color.g,
            actionText.color.b,
            actionText.color.a - (Time.deltaTime / timer));
            yield return null;
        }
        isDisplayingText = false;
    }
}
