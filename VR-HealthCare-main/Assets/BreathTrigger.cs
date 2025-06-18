using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreathTrigger : MonoBehaviour
{
    [SerializeField] UpdateVitalsData updateVitalsData;
    [SerializeField] string BreathingMaskTag = "Breathing Mask";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(BreathingMaskTag))
        {
            updateVitalsData.breathingMask = true;
            other.transform.position = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(BreathingMaskTag))
        {
            updateVitalsData.breathingMask = false;
        }
    }
}
