using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI mainText; // Reference to the UI Text element
    private int timer = 0; // Timer variable to track the score
    private float elapsedTime = 0; // Accumulated time for updating the timer

    // Start is called before the first frame update
    void Start()
    {
        if (mainText != null)
        {
            mainText.text = "Timer: " + timer;
        }
        else
        {
            Debug.LogError("Score Text is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Update the timer every second
        if (elapsedTime >= 1f)
        {
            timer++;
            elapsedTime = 0f;

            if (mainText != null)
            {
                mainText.text = "Timer: " + timer;
            }

        }
    }
    
}