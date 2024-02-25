using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public List<Sound> sounds;
	void Awake() {
		// Retrieves the properties of the different sound clips and applies them where appropriate.
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void Play(string name) {
		Sound s = sounds.Find(sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		Debug.Log("Playing "+name+" Clip: "+s.clip);
		s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		s.source.Play();
	}

	public void StopAll() {
		foreach (Sound s in sounds) {
			s.source.Stop();
		}
	}

	public void Stop(string name) {
		Sound s = sounds.Find(sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.Stop();
	}

	public void UpdateVolume(string name, float volume) {
		Sound s = sounds.Find(sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		if(s.source.volume < 0.25f) {
			s.source.volume = 0.25f;
		} else {
			s.source.volume += volume;
		}
	}
}