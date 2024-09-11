using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingMenu : MonoBehaviour
{
    [SerializeField] Button backBtn;

    void Awake()
    {
        backBtn.onClick.AddListener(CloseScreen);
    }

    void CloseScreen() {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        backBtn.onClick.RemoveAllListeners();
    }

}
