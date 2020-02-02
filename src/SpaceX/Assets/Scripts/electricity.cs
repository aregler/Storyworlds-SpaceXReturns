using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class electricity : MonoBehaviour
{
    private bool isDisplayingText = false, playerInRange = false;
    public GameObject oldGrab, newGrab, video;
    public Text actionText;
    public bool On = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange && !On)
        {
            StartCoroutine(fadeTextToZeroAlpha(1.0f));
            oldGrab.SetActive(false);
            newGrab.SetActive(true);
            On = true;
            video.GetComponent<Renderer>().enabled = true;
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
        if(On)
            actionText.text = "Generator läuft!";
        else 
            actionText.text = "Aktivieren (F)";
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
