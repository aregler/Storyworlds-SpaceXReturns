using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Camera cam;
    public PlayerMovement playerMovement;
    public PlayerTarget target;

    private float introDuration = 180.0f;

    public static GameManager shared = null;

    /* shoud be in an separate class*/
    public DialogueTrigger introTrigger;

    // Start is called before the first frame update
    void Start() {
        enableEnvironment();
        Debug.Log("start intro");
        Invoke("startIntro", 3f);
    }

    void Awake() {
        if (shared == null) {
            shared = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        AudioManager.end += soundDidEnd;
        LevelChanger.onLevelChange += onLevelChange;
    }

    void OnDisable() {
        AudioManager.end -= soundDidEnd;
        LevelChanger.onLevelChange -= onLevelChange;
    }

    void onLevelChange(LevelChanger.Scene scene) {
        print("on level change");
        switch (scene) {
            case LevelChanger.Scene.WayToFacility:
                startWayToFacility();
                break;
        }
    }

    void soundDidEnd(string name) {
        print(name + " ended.");
        if (name == "intro_after_landing") {
            playerMovement.isLocked = false;
            AudioManager.instance.stop();
        } else if (name == "landing") {
            CameraShake shakingCam = cam.GetComponent<CameraShake>();
            StopAllCoroutines();
            AudioManager.instance.play("intro_after_landing");
        } else if (name == "way_to_facility") {
            // this is buggy somehow
            //Invoke("loadFacilityDelayed", 2f);
        } else {
            AudioManager.instance.stop();
            AudioManager.instance.play("landing");
        }
    }

    private void loadFacilityDelayed() {
        LevelChanger.shared.loadNextScene();
    }

    // Update is called once per frame
    void Update() { }

    /* Thruster sound now plays one minute then loops using the default loop. */
    void enableEnvironment() {
        AudioManager.instance.play("RocketThrusters");
        CameraShake shakingCam = cam.GetComponent<CameraShake>();
        StartCoroutine(shakingCam.Shake(introDuration, .3f));
        //cam.transform.rotation = Quaternion.Euler(0, 180, 0);
        playerMovement.isLocked = true;
    }

    /* There should be something like a level-content handler. */

    void startIntro() {
        AudioManager.instance.play("intro_pre_landing");
        Invoke("startIntroFade", 3f);
        introTrigger.triggerDialogue();
    }

    private void startIntroFade() {
        StartCoroutine(introFade(5.0f));
    }

    IEnumerator introFade(float timer) {
        Image img = GameObject.Find("FadingImage").GetComponent<Image>();
        if (img != null) print("image ist da");
        while (img.color.a > 0.0f) {
            img.color = new Color(img.color.r,
            img.color.g,
            img.color.b,
            img.color.a - (Time.deltaTime / timer));
            yield return null;
        }
        img.enabled = false;
    }

    public void startWayToFacility() {
        target.isEnabled(false);
        AudioManager.instance.play("way_to_facility");
        Invoke("loadFacilityDelayed", 18f);
    }

    void startGame() { }
}
