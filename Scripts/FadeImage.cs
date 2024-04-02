using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Damageable))]

public class Fadeimage : MonoBehaviour
{
    public Image image;

    //visible or invisible
    public float target = 0.0f;

    public float second = 2.0f;


 

    private void Start()
    {
        StartCoroutine(FadeImage((getImageDone) =>
        {
            if (getImageDone)
            {
                //code after Fade visible 
            }
        }));
    }

    private IEnumerator FadeImage(Action<bool> action)
    {
        var alpha = image.color.a;
        

            //if ()

            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / second)
            {
                //change color as you want
                var newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(alpha, target, t));
                image.color = newColor;
                yield return null;
                action(image.color.a < 0.05f);
            }
        }
    }

