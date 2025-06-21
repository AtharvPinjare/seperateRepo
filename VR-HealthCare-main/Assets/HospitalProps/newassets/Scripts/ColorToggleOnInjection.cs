using UnityEngine;

public class ColorToggleOnInjection : MonoBehaviour
{
    public GameObject bottle1;
    public GameObject bottle2;
    public GameObject bottle3;
    public GameObject injection;

    void Start()
    {
        // Set initial colors
        SetColor(bottle1, Color.yellow);
        SetColor(bottle2, Color.green);
        SetColor(bottle3, Color.blue);
        SetColor(injection, Color.white);
    }

    void SetColor(GameObject obj, Color color)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = color;
        }
    }

    void SwapColors(GameObject bottle)
    {
        Renderer bottleRenderer = bottle.GetComponent<Renderer>();
        Renderer injectionRenderer = injection.GetComponent<Renderer>();

        if (bottleRenderer != null && injectionRenderer != null)
        {
            //Color tempColor = bottleRenderer.material.color;
            //bottleRenderer.material.color = injectionRenderer.material.color;
            injectionRenderer.material.color = bottleRenderer.material.color ;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other == bottle1 || other == bottle2 || other == bottle3)
        {
            SwapColors(other);
        }
    }
}
