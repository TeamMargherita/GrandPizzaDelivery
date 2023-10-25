using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class StreetLamp : MonoBehaviour
{
    [SerializeField] private Light2D pointLight2D;

    private static Color lightOnColor = new Color(255 / 255f, 177 / 255f, 0 / 255f);
    private static Color lightOffColor = Color.black;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeLight2D(bool bo)
    {
        if (bo)
        {
            pointLight2D.color = lightOnColor;
        }
        else
        {
            pointLight2D.color = lightOffColor;
        }
    }
}
