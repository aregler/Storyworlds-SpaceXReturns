using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelChanger : MonoBehaviour {
    public static LevelChanger shared = null;

    public delegate void LevelChangeDelegate(LevelChanger.Scene scene);
    public static event LevelChangeDelegate onLevelChange;

    public enum Scene {
        WayToFacility,
        Facility,
        video,
        sound
    }
    private string levelToLoad;

    public Queue<Scene> scenes;
    public Animator animator;

    public bool canChangeLevel {
        get { return scenes.Count > 0; }
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

    void Start() {
        scenes = new Queue<Scene>();
        scenes.Enqueue(Scene.WayToFacility);
        scenes.Enqueue(Scene.Facility);
        scenes.Enqueue(Scene.video);
        scenes.Enqueue(Scene.sound);
    }

    public void fadeToLevel(string name) {
        levelToLoad = name;
        onFadeComplete();
    }

    public void loadNextScene() {
        if (!canChangeLevel) { return; };
        LevelChanger.Scene nextScene = scenes.Dequeue();
        switch (nextScene) {
            case LevelChanger.Scene.WayToFacility:
                fadeToLevel("WayToFacilityScene");
                break;
            case LevelChanger.Scene.Facility:
                fadeToLevel("Facility");
                break;
            case LevelChanger.Scene.video:
                fadeToLevel("video");
                break;
            case LevelChanger.Scene.sound:
                fadeToLevel("sound");
                break;
        }
        onLevelChange(nextScene);
    }

    public void onFadeComplete() {
        print("fade complete");
        SceneManager.LoadScene(levelToLoad);
    }

}
