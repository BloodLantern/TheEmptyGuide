using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image image;

    Color textColor;
    Color imageColor;

    bool appearing = true;
    float elapsed = 0f;

    [SerializeField] float cycleTime = 2f;
    [SerializeField] int sceneIndex = 0;

    private void Start()
    {
        textColor = text.color;
        imageColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += appearing ? Time.deltaTime : -Time.deltaTime;
        if (elapsed > cycleTime || elapsed < 0f)  {
            appearing = !appearing;
        }
        float lRatio = elapsed / cycleTime;
        text.color = new Color(textColor.r, textColor.g, textColor.b, lRatio);
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, lRatio);

        if (Input.anyKey)
        {
            TransitionManager.ChangeScene(sceneIndex);
        }
    }
}
