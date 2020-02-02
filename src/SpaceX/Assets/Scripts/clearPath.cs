using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clearPath : MonoBehaviour
{
    private bool isDisplayingText = false, playerInRange = false;
    public GameObject clear;
    public Text actionText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            StartCoroutine(fadeTextToZeroAlpha(1.0f));
            Destroy(this.gameObject);
            clear.SetActive(true);
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
        actionText.text = "Freiräumen (F)";
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
