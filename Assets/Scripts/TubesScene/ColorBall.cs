using UnityEngine;


/// <summary>
/// Script to set the ball color.
/// </summary>
public class ColorBall : MonoBehaviour
{
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    //Set color in RGB values from 0 to 255.
    public void setColor(int red, int green, int blue)
    {
        rend.material.color = new Color(coloNumberConversion(red), coloNumberConversion(green), coloNumberConversion(blue));
    }

    private float coloNumberConversion(float num)
    {
        return (num / 255.0f);
    }

}
