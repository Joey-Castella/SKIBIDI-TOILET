using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSlideshow : MonoBehaviour
{
    public GameObject[] slides; // Drag your 5 slides here
    private int currentSlideIndex = 0;

    // CHANGE: Use OnEnable instead of Start
    void OnEnable()
    {
        currentSlideIndex = 0; // Reset index
        ShowSlide(0);          // Show the first one immediately
    }

    public void NextSlide()
    {
        currentSlideIndex++;

        if (currentSlideIndex < slides.Length)
        {
            ShowSlide(currentSlideIndex);
        }
        else
        {
            Debug.Log("Slides finished. Loading Menu...");
            Time.timeScale = 1f; // IMPORTANT: Unfreeze time before leaving
            SceneManager.LoadScene("main menu"); // Make sure this matches your scene name exactly
        }
    }

    void ShowSlide(int index)
    {
        // Turn off ALL slides first
        for (int i = 0; i < slides.Length; i++)
        {
            if (slides[i] != null) 
                slides[i].SetActive(false);
        }

        // Turn on the current one
        if (index < slides.Length && slides[index] != null)
        {
            slides[index].SetActive(true);
        }
    }
}