using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosisMangerScript : MonoBehaviour
{
    public GameObject patient;
    public GameObject antiVenom;
    public GameObject oxygenMask;
    public GameObject bagValveMask;
    public GameObject gameOverScreen;
    public GameObject biteInspectionText;
    public GameObject diagnosisConfirmationUI;
    public Animator patientAnimator;
    public TMPro.TextMeshProUGUI timerText;
    public GameObject mainCamera;
    public Transform biteArea;

    private float timeSinceBite = 0f;
    private bool isAntivenomGiven = false;
    private bool isBreathingSupported = false;
    private bool patientDead = false;
    private bool manualBreathingStarted = false;
    private bool biteInspected = false;
    private bool diagnosisConfirmed = false;

    void Update()
    {
        //if (patientDead) return;
        //timeSinceBite += Time.deltaTime;

        //// Update Timer UI
        //timerText.text = "Time Since Bite: " + Mathf.Floor(timeSinceBite).ToString() + " sec";

        //// Condition deteriorates after 30 minutes (simulated as 30 seconds here)
        //if (timeSinceBite > 30f && !isAntivenomGiven)
        //{
        //    TriggerRespiratoryArrest();
        //}

        //// Game over if no oxygen support or manual breathing
        //if (timeSinceBite > 60f && !isBreathingSupported && !manualBreathingStarted)
        //{
        //    GameOver();
        //}
    }

    public void AdministerAntivenom()
    {
        if (antiVenom.activeInHierarchy && diagnosisConfirmed)
        {
            isAntivenomGiven = true;
            Debug.Log("Antivenom Administered. Patient Stabilizing.");
            patientAnimator.SetBool("Recovering", true);
        }
        else if (!diagnosisConfirmed)
        {
            Debug.Log("You must confirm diagnosis first.");
        }
    }

    public void ProvideOxygen()
    {
        if (oxygenMask.activeInHierarchy)
        {
            isBreathingSupported = true;
            Debug.Log("Oxygen Provided. Preventing respiratory arrest.");
        }
    }

    public void StartManualBreathing()
    {
        if (bagValveMask.activeInHierarchy)
        {
            manualBreathingStarted = true;
            Debug.Log("Manual Breathing Started. Patient Stabilizing.");
        }
    }

    public void InspectBite()
    {
        if (!biteInspected)
        {
            Debug.Log("Inspecting Bite Mark...");
            biteInspectionText.SetActive(true);
            // biteInspectionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Fang marks detected. Slight swelling. Suggestive of Cobra bite.";
            CameraZoomToBite();
            Invoke("ShowDiagnosisConfirmation", 2f);
            biteInspected = true;
        }
    }

    void CameraZoomToBite()
    {
        Debug.Log("Camera Shifted");
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, biteArea.position, 0.5f);
        mainCamera.transform.LookAt(biteArea);
    }

    void ShowDiagnosisConfirmation()
    {
        Debug.Log("Confirmed Diagnosis");
        diagnosisConfirmationUI.SetActive(true);
    }

    public void ConfirmDiagnosis()
    {
        Debug.Log("Diagnosis Confirmed: Cobra Bite");
        diagnosisConfirmed = true;
        diagnosisConfirmationUI.SetActive(false);
    }

    void TriggerRespiratoryArrest()
    {
        Debug.Log("Patient is now experiencing Respiratory Arrest.");
        patientAnimator.SetBool("RespiratoryArrest", true);
    }

    void GameOver()
    {
        Debug.Log("Patient Died. Game Over.");
        gameOverScreen.SetActive(true);
        patientDead = true;
        patientAnimator.SetBool("Dead", true);
    }
}
