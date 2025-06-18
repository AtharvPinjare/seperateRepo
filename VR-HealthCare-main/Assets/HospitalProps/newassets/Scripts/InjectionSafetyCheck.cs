using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using System.Collections;

public class InjectionSocketChecker : MonoBehaviour
{
    public GameObject injection; // Assign the injection object in Inspector
    public GameObject uiPanel;   // Assign the UI panel to enable/disable
    public Text messageText;     // Assign the Text component inside the panel

    private XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnInjectionInserted);
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
}
