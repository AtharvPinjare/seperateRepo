using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // ← Required for TextMeshProUGUI

public class InjectionSocketHandler : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public TextMeshPro reportText; // ← TextMeshPro Text

    public Renderer injectionRenderer; // Reference to the injection's renderer
    private Material targetMaterial; // Material to change color
    public GameObject injection; // Reference to the injection object
    void Start()
    {
        reportText.text = ""; // Initialize the report text
        socket1 = socket1.GetComponent<XRSocketInteractor>();
        socket1.selectEntered.AddListener(OnInjectionInserted1);
        socket2 = socket2.GetComponent<XRSocketInteractor>();
        socket2.selectEntered.AddListener(OnInjectionInserted2);
        if (injectionRenderer == null)
            injectionRenderer = GetComponent<Renderer>();

        // Use a copy of the material (so we don't affect shared material)
        targetMaterial = injectionRenderer.material;

        // Optional: Set start color to white explicitly
        targetMaterial.SetColor("_BaseColor", Color.white);
    }
    void OnInjectionInserted1(SelectEnterEventArgs args)
    {
        GameObject insertedObject = args.interactableObject.transform.gameObject;

        if (insertedObject == injection)
        {
            Renderer renderer = injection.GetComponent<Renderer>();
            renderer.material.color = new Color(1f, 0, 0);
            Debug.Log("Injection inserted into Socket 1");
        }
    }
    void OnInjectionInserted2(SelectEnterEventArgs args)
    {
        GameObject insertedObject = args.interactableObject.transform.gameObject;

        if (insertedObject == injection)
        {
            Renderer renderer = injection.GetComponent<Renderer>();
            if (renderer.material.color == new Color(1f,0,0))
            {
                Debug.Log("Injection inserted into Socket 2");
                reportText.text += "Clotting Profile (PT/INR, aPTT): Prolonged\n Fibrinogen: Low\n D-dimer: Elevated\n Complete Blood Count: " +
                    "Leukocytocytosis(WBC), mild thrombocytopenia\n";
            }
        }
    }
    void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (socket1 != null)
            socket1.selectEntered.RemoveListener(OnInjectionInserted1);
        if (socket2 != null)
            socket2.selectEntered.RemoveListener(OnInjectionInserted2);
    }
}
