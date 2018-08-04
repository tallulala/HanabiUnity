using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;
using System.IO;

public class GazeTrackingController : MonoBehaviour
{
    public ButtonController ButtonCont;
    public Camera cam;
    public Text txt;
    public float minx;
    public float maxx;
    public float miny;
    public float maxy;
    public RawImage img;
    public Texture2D texture;
    public float hue = 0;
    public float saturation = 1;
    public float value = 1;
    public int ImageCount = 0;

    public void Start()
    {
        for (int i = 0; i < texture.width; ++i)
        {
            for (int j = 0; j < texture.height; ++j)
            {
                texture.SetPixel(i, j, Color.HSVToRGB(0.75f, saturation, value));
            }
        }
    }

    public IEnumerator MakeMap()
    {
        int n = 0;

        while (!ButtonCont.isGameOver)
        {
            yield return new WaitForSeconds(0.1f);

            Vector2 GazeInput = TobiiAPI.GetGazePoint().Viewport;
            Vector3 GazeDotPos = new Vector3(GazeInput.x, GazeInput.y, 0);
            Vector3 transformed = cam.ViewportToScreenPoint(GazeDotPos);

            if (!float.IsNaN(GazeInput.x) && !float.IsNaN(GazeInput.y))
            {
                txt.text = "Gaze detected    " + string.Format("{0:0.##}", GazeInput.x) + "/" + string.Format("{0:0.##}", GazeInput.y) +
                    "      " + string.Format("{0:0.##}", transformed.x) + "/" + string.Format("{0:0.##}", transformed.y);

                gameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(minx * (1 - GazeDotPos.x) + maxx * (GazeDotPos.x), miny * (1 - GazeDotPos.y) + maxy * (GazeDotPos.y));

                int x = (int)(texture.width * (GazeDotPos.x));
                int y = (int)(texture.height * (GazeDotPos.y));

                ColorRadius(1, 0.1f, x, y);
                ColorRadius(3, 0.05f, x, y);
                ColorRadius(5, 0.02f, x, y);

                n++;
                if (n % 25 == 0)
                {
                    texture.Apply();
                }
            }
        }
    }

    public void ColorRadius(int radius, float deltaHue, int x, int y)
    {
        Color color;

        for (int i = -radius; i < radius; i++)
        {
            for (int j = -radius; j < radius; j++)
            {
                color = texture.GetPixel(x + i, y + j);
                Color.RGBToHSV(color, out hue, out saturation, out value);
                hue = Limit(hue - deltaHue, 0, 0.75f);
                texture.SetPixel(x + i, y + j, Color.HSVToRGB(hue, saturation, value));
            }
        }
    }

    public float Limit(float val, float min, float max)
    {
        if (val < min)
        {
            return min;
        }
        if (val > max)
        {
            return max;
        }
        return val;
    }

    public void SaveTextureAsPNG()
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../HeatMaps/HeatMap" + ButtonCont.playerName + ImageCount + ".png", bytes);
        ImageCount++;

        for (int i = 0; i < texture.width; ++i)
        {
            for (int j = 0; j < texture.height; ++j)
            {
                texture.SetPixel(i, j, Color.white);
            }
        }
    }
}