using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    public bool isLooping = false;

    public delegate void SoundDidEndDelegate(string name);
    public static event SoundDidEndDelegate end;

    private string currentSound;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void stop() {
        foreach (Sound s in sounds) {
            s.source.Stop();
        }
    }

    public void play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
        s.source.Play();
        currentSound = s.name;
        Invoke("onSoundEnd", Mathf.Ceil(s.source.clip.length));
        print(s.name + " length: " + s.source.clip.length);
    }

    public void onSoundEnd() {
        if (currentSound != null && end != null) {
            end(currentSound);
        }
    }

    public void loopSound(string name, string name2) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s2 = Array.Find(sounds, sound => sound.name == name2);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
        isLooping = true;
        StartCoroutine(loop(s, s2));
    }

    private IEnumerator loop(Sound s, Sound s2) {
        s.source.Play();
        while (isLooping) {
            StartCoroutine(crossFade(s, s2, 0.33f));
            yield return new WaitForSeconds(s.clip.length - 2f);
        }
    }

    public void playMusicWithFade(string name, float transitionTime = 1.0f) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Backgroundmusic: " + name + "not found.");
            return;
        }
        StartCoroutine(updateMusicWithFade(s, transitionTime));
    }

    private IEnumerator updateMusicWithFade(Sound sound, float transitionTime) {
        if (!sound.source.isPlaying) sound.source.Play();
        float t = 0.0f;
        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime) {
            sound.volume = (1 - (t / transitionTime));
            yield return null;
        }

        sound.source.Stop();
        sound.source.Play();

        // Fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime) {
            sound.volume = (t / transitionTime);
            yield return null;
        }
    }

    private IEnumerator crossFade(Sound sound, Sound newSound, float transitionTime = 1.0f) {
        float t = 0.0f;
        newSound.source.Play();
        for (t = 0; t < transitionTime; t += Time.deltaTime) {
            sound.source.volume = (1 - (t / transitionTime));
            newSound.source.volume = (t / transitionTime);
            yield return null;
        }

        sound.source.Stop();
    }
}
