using UnityEngine;

public class Music : MonoBehaviour
{
   	private static Music instance;  // Ensures only one instance

    private AudioSource audioSource;  // Reference to the AudioSource

    void Awake()
    {
        // Ensure only one instance of the music player exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource is set to loop
        if (audioSource != null)
        {
            audioSource.loop = true;  // Set the music to loop
            audioSource.Play();  // Start playing the music
        }
        else
        {
            Debug.LogError("AudioSource not found on this GameObject.");
        }
    }
}
