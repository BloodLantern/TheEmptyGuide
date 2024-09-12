using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 100f;
    [SerializeField] Transition transition;
    [SerializeField] Transform endScreen;

    List<RectTransform> textList = new List<RectTransform>();
    bool ended = false;

    private void Start()
    {
        foreach (RectTransform text in transform.GetComponentsInChildren<RectTransform>())
        {
            textList.Add(text);
        }
    }

    private void Update()
    {
        for (int i = 0; i < textList.Count; i++)
        {
            textList[i].transform.position += Vector3.up * scrollingSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ended = true;
            TransitionManager.ChangeScene(transition,0);
        }
    }
}
