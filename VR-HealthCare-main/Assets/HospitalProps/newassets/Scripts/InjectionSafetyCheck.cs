using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using System.Collections;

public class InjectionSafetyCheck : MonoBehaviour
{
    public GameObject injection; // Assign the injection object in Inspector
    public GameObject uiPanel;   // Assign the UI panel to enable/disable
    public Text messageText;     // Assign the Text component inside the panel

    private XRSocketInteractor socket;
    public Renderer targetRenderer;     // Assign the object's renderer in Inspector
    private Material targetMaterial;
    public bool isSafe = false; // Track if the injection is safe
    private Color startColor = Color.white;
    private Color targetColor = new Color(0.5f, 0f, 1f); // Violet
    private Color targetColor1 = new Color(0.8f,0.6f,1f); // Safe color for the injection
    private float duration = 600f; // 2 minutes
    private float elapsedTime1 = 0f;
    private float elapsedTime2 = 0f;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnInjectionInserted);
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        // Use a copy of the material (so we don't affect shared material)
        targetMaterial = targetRenderer.material;

        // Optional: Set start color to white explicitly
        targetMaterial.SetColor("_BaseColor", startColor);
    }

    void OnInjectionInserted(SelectEnterEventArgs args)
    {
        GameObject insertedObject = args.interactableObject.transform.gameObject;

        if (insertedObject == injection)
        {
            Renderer renderer = injection.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;

                if (color == Color.yellow)
                {
                    Debug.Log("Injection is safe, proceed with treatment");
                    if (uiPanel != null && messageText != null)
                    {
                        messageText.text = "I am feeling better, thank you doctor";
                        uiPanel.SetActive(true);
                        isSafe = true; // Mark as safe

                    }
                }
                else if (color == Color.white)
                {
                    Debug.Log("bring medicine quickly");
                    if (uiPanel != null && messageText != null)
                    {
                        messageText.text = "What Are  you waiting for? Bring the medicine quickly!";
                        uiPanel.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("not safe");
                    if (uiPanel != null && messageText != null)
                    {
                        messageText.text = "I am feeling worse, please check the medicine";
                        uiPanel.SetActive(true);
                    }
                }

                StartCoroutine(ResetColorAfterDelay(renderer));
            }
        }
    }

    IEnumerator ResetColorAfterDelay(Renderer renderer)
    {
        yield return new WaitForSeconds(1f);
        renderer.material.color = Color.white;
    }

    void OnDestroy()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnInjectionInserted);
        }
    }

    void Update()
    {
        if ((elapsedTime1 < duration) && (isSafe == false))
        {
            elapsedTime1 += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime1 / duration);

            Color currentColor = Color.Lerp(startColor, targetColor, t);
            targetMaterial.SetColor("_BaseColor", currentColor);
        }
        else if (elapsedTime2 < 5f && isSafe == true)
        {
            elapsedTime2 += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime2 / 5f);

            Color currentColor = Color.Lerp(targetColor1, startColor, t);
            targetMaterial.SetColor("_BaseColor", currentColor);
        }
    }
}

