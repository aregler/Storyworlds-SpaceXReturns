using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTarget : MonoBehaviour {

    public Camera fpsCam;
    public float maxHitRange = 50f;

    public Text actionText;

    private bool isDisplayingText;
    private bool isFocusingDoor = false;

    public void isEnabled(bool value) {
        actionText.enabled = value;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && isFocusingDoor) {
            LevelChanger.shared.loadNextScene();
        }
    }

    void FixedUpdate() {
        onTarget();
    }

    void onTarget() {
        RaycastHit target;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, maxHitRange)) {
            if (target.transform.tag == "AvailableActionTrigger") {
                isFocusingDoor = true;
                if (!isDisplayingText) {
                    StartCoroutine(fadeTextToFullAlpha(1.0f));
                }
            } else if (isDisplayingText) {
                StartCoroutine(fadeTextToZeroAlpha(1.0f));
            } else {
                isFocusingDoor = false;
            }
        }
    }

    IEnumerator fadeTextToFullAlpha(float timer) {
        actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 0);
        actionText.text = "F zum Öffnen";
        while (actionText.color.a < 1.0f) {
            actionText.color = new Color(actionText.color.r,
            actionText.color.g,
            actionText.color.b,
            actionText.color.a + (Time.deltaTime / timer));
            yield return null;
        }
        isDisplayingText = true;
    }

    IEnumerator fadeTextToZeroAlpha(float timer) {
        actionText.color = new Color(actionText.color.r, actionText.color.g, actionText.color.b, 1);
        while (actionText.color.a > 0.0f) {
            actionText.color = new Color(actionText.color.r,
            actionText.color.g,
            actionText.color.b,
            actionText.color.a - (Time.deltaTime / timer));
            yield return null;
        }
        isDisplayingText = false;
    }
}
