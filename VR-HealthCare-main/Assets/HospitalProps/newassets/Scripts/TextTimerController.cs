using UnityEngine;
using TMPro;

public class TextTimerController : MonoBehaviour
{
    public TextMeshPro timerText;  // Assign your TextMeshProUGUI in Inspector
    public AudioSource beepAudio;      // Assign an AudioSource with a beep clip
    private float timer = 300f;        // Start from 5 minutes (300 seconds)
    private bool isBeeping = false;
    private float nextBeepTime = 0f;
    public InjectionSafetyCheck injectionSafetyCheck; // Reference to the InjectionSafetyCheck script

    void Update()
    {
        if (injectionSafetyCheck != null && !injectionSafetyCheck.isSafe)
        {
        

            timer += Time.deltaTime;

            // Show timer in mm:ss format until "Death"
            if (timer < 720f)
            {
                int minutes = Mathf.FloorToInt(timer / 60f);
                int seconds = Mathf.FloorToInt(timer % 60f);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }

            // Color transition: Green (5 min) to Red (10 min)
            if (timer <= 600f)
            {
                float t = Mathf.InverseLerp(300f, 600f, timer); // from 0 to 1
                timerText.color = Color.Lerp(Color.green, Color.red, t);
            }

            // Beep between 10 to 12 minutes
            if (timer >= 600f && timer < 720f)
            {
                if (!isBeeping)
                {
                    isBeeping = true;
                    nextBeepTime = Time.time;
                }

                if (Time.time >= nextBeepTime)
                {
                    if (beepAudio != null)
                        beepAudio.Play();

                    nextBeepTime = Time.time + 1f;
                }
            }

            // After 12 minutes: show "Death" and stop beeping
            if (timer >= 720f && isBeeping)
            {
                isBeeping = false;
                timerText.text = "Death";
                timerText.color = Color.red;
                Debug.Log("Death");
            }
        }
        else
        {
            timerText.text = "Healing";
            timerText.color = Color.green; // Optional: change color to indicate healing
        }
    }
}

