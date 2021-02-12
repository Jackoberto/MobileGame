using System;
using System.Threading.Tasks;
using UnityEngine;

public class Mic : MonoBehaviour
{
    public AudioSource audioSourceSettings;
    public int time = 3;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Assign(time);
        }
    }

    private async void Assign(float duration)
    {
        Debug.Log("Recording");
        var audioClip = Microphone.Start(Microphone.devices[0], false, time, 48000);
        await Task.Delay(TimeSpan.FromSeconds(duration + 1));
        Debug.Log("Playing");
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = audioSourceSettings.playOnAwake;
        audioSource.loop = audioSourceSettings.loop;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}