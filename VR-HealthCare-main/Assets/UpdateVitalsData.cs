using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateVitalsData : MonoBehaviour
{
    public TextMeshPro heartRate;
    public TextMeshPro bloodO2;
    public bool breathingMask = false;
    private int heartRateValue;

    public void UpdateVitalData()
    {
        StartCoroutine(DataUpdater());
    }
    public void StopDataUpdation()
    {
        StopCoroutine(DataUpdater());
    }
    public IEnumerator DataUpdater()
    {
        while (true)
        {
            // Heart rate and blood oxygen level values are random for demonstration purposes :)
            if(breathingMask)
            {
                heartRateValue = Random.Range(60, 100);
                heartRate.text = heartRateValue.ToString();
                bloodO2.text = Random.Range(90, 100).ToString();
            }
            else
            {
                heartRateValue = Random.Range(100, 150);
                heartRate.text = heartRateValue.ToString();
                bloodO2.text = Random.Range(70, 90).ToString();
            }
            yield return new WaitForSeconds(1.2f);
        }
    }
}
