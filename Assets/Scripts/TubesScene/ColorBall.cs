using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set color in RGB values from 0 to 255.
    public void setColor(int red, int green, int blue)
    {
        rend.material.color = new Color(coloNumberConversion(red), coloNumberConversion(green), coloNumberConversion(blue));
    }

    Color getCurrentColor(float red, float green, float blue)
    {
        Color currentColor = rend.material.GetColor("_Color");
        return currentColor;
    }

    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }

}
