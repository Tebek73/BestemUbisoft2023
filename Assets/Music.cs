using UnityEngine;

public class Music: MonoBehaviour
{
    public AudioClip backgroundSong; // Assign your background music in the Inspector

    private AudioSource audioSource;

    void Start()
    {
        // Fetch the AudioSource component from the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if not present
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio clip as background music
        audioSource.clip = backgroundSong;

        // Set the audio to loop
        audioSource.loop = true;

        // Play the background music
        audioSource.Play();
    }
}
