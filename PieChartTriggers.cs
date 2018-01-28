using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieChartTriggers : MonoBehaviour
{
    /*
    Color[] Data;
    Image image;

    public int Width { get { return image.sprite.texture.width; } }
    public int Height
    {
        get { return image.sprite.texture.height; }
    }

void Awake()
    {
        image = GetComponent<Image>();
        Data = image.sprite.texture.GetPixels();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPos = new Vector2(screenPos.x, screenPos.y);

            RaycastHit2D[] ray = Physics2D.RaycastAll(screenPos, Vector2.zero, 0.01f);
            for (int i = 0; i < ray.Length; i++)
            {
               if (ray[i].collider.tag == "Circle")
                {
                    screenPos -= ray[i].collider.gameObject.transform.position;
                    int x = (int)(screenPos.x * Width);
                    int y = (int)(screenPos.y * Height) + Height;

                   if (x > 0 && x < Width && y > 0 && y < Height)
                    {
                        Color color = Data[y * Width + x];
                        Debug.Log("clicked " + color);
                    }
                    break;
                }
            }
        }
    }
    */
}
