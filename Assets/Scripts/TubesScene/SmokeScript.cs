using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
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
    void setColor(int red, int green, int blue)
    {
        rend.material.color = new Color(coloNumberConversion(red), coloNumberConversion(green), coloNumberConversion(blue));
    }
    
    //RGB mode.
    void increaseColor(int red, int green, int blue)
    {
        Color currentColor = rend.material.GetColor("_Color");

        int newRed = (int)currentColor.a*255;
        int newGreen = (int)currentColor.b * 255;
        int newBlue = (int)currentColor.g * 255;
        newRed += red;
        newRed += green;
        newBlue += blue;

        //If it is possible increase color.
        if (newRed >= 0 && newRed <= 255)
        {
            rend.material.color = new Color(coloNumberConversion(newRed), currentColor.b, currentColor.g);
        }

        if (newGreen >= 0 && newGreen <= 255)
        {
            rend.material.color = new Color(currentColor.a, coloNumberConversion(newGreen), currentColor.g);
        }

        if (newBlue >= 0 && newBlue <= 255)
        {
            rend.material.color = new Color(currentColor.a, currentColor.b, coloNumberConversion(newBlue));
        }

    }

    //Percentatge mode.
    public void increaseColor(float red, float green, float blue)
    {
        Color currentColor = rend.material.GetColor("_Color");

        //Debug.Log(currentColor);

        float newRed = currentColor.r + red;
        float newGreen = currentColor.g + green;
        float newBlue = currentColor.b + blue;
        //Debug.Log(newGreen);
        //If it is possible increase color.
        if (newRed >= 0f && newRed <= 1.0f)
        {
            rend.material.SetColor("_Color", new Color(newRed, currentColor.g, currentColor.b));
        }

        currentColor = rend.material.GetColor("_Color");

        if (newGreen >= 0f && newGreen <= 1.0f)
        {
            rend.material.SetColor("_Color", new Color(currentColor.r, newGreen, currentColor.b));
        }

        currentColor = rend.material.GetColor("_Color");
        //Debug.Log("After set"+currentColor);

        if (newBlue >= 0f && newBlue <= 1.0f)
        {
            rend.material.SetColor("_Color", new Color(currentColor.r, currentColor.g, newBlue));
        }

    }

    public Dictionary<string, int> getCurrentColor()
    {
        Dictionary<string, int> smokeColors = new Dictionary<string, int>();

        Color currentColor = rend.material.GetColor("_Color");

        smokeColors.Add("red", (int)(currentColor.r * 255));
        smokeColors.Add("green", (int)(currentColor.g * 255));
        smokeColors.Add("blue", (int)(currentColor.b * 255));

        return smokeColors;
    }

        //Color is expressed in % (from 0 to 1) so this function converts 255 to a value from 0 to 1.
        private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }
}
