﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviorFacility : MonoBehaviour
{
    public Text actionText;
    private bool isDisplayingText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnTrigger(Collider other)
    {
        Debug.Log("true");
        if (other.CompareTag("Player"))
        {
            if (!isDisplayingText)
            {
                StartCoroutine(fadeTextToFullAlpha(1.0f));
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("true");
            if (!isDisplayingText)
            {
                StartCoroutine(fadeTextToFullAlpha(1.0f));
            }
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
