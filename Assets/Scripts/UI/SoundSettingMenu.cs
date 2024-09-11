using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingMenu : MonoBehaviour
{
    [SerializeField] Button backBtn;

    void Awake()
    {
        
    }

    void CloseScreen() {
        Destroy(gameObject);
    }

}
