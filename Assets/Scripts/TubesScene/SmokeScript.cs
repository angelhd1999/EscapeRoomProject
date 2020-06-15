using UnityEngine;

/// <summary>
/// Script to get the color information of the smoke.
/// </summary>
public class SmokeScript : MonoBehaviour
{

    //Function to get the current color of the smoke.
    public int[] getCurrentColor()
    {

        Color currentColor = this.GetComponent<Renderer>().material.GetColor("_Color");

        int[] smokeColors = new int[3];

        smokeColors[0] = (int)(currentColor.r * 255);
        smokeColors[1] = (int)(currentColor.g * 255);
        smokeColors[2] = (int)(currentColor.b * 255);

        return smokeColors;
    }

    //Set color in RGB values from 0 to 255.
    void setColor(int red, int green, int blue)
    {
        this.GetComponent<Renderer>().material.color = new Color(coloNumberConversion(red), coloNumberConversion(green), coloNumberConversion(blue));
    }

    //Increase some percentatge of each color.
    public void increaseColor(float red, float green, float blue)
    {
        Color currentColor = this.GetComponent<Renderer>().material.GetColor("_Color");


        float newRed = currentColor.r + red;
        float newGreen = currentColor.g + green;
        float newBlue = currentColor.b + blue;

        //If it is possible increase color.
        if (newRed >= 0f && newRed <= 1.0f)
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", new Color(newRed, currentColor.g, currentColor.b));
        }

        currentColor = this.GetComponent<Renderer>().material.GetColor("_Color");

        if (newGreen >= 0f && newGreen <= 1.0f)
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", new Color(currentColor.r, newGreen, currentColor.b));
        }

        currentColor = this.GetComponent<Renderer>().material.GetColor("_Color");

        if (newBlue >= 0f && newBlue <= 1.0f)
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", new Color(currentColor.r, currentColor.g, newBlue));
        }

    }

    //Color is expressed in % (from 0 to 1) so this function converts 255 to a value from 0 to 1.
    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }
}
