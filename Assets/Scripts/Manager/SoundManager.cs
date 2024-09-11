//using FMODUnity;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SoundManager : MonoBehaviour
//{
//    [field: SerializeField] private EventReference _sfxSucces; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxDeath; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxDamagePlayer; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxDamageFoe; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxWin; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxKeyboard; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxValidateWord; //{ get; private set; }
//    [field: SerializeField] private EventReference _sfxJumpNSlide; //{ get; private set; }

//    private static SoundManager instance;

//    private SoundManager() { }

//    public static SoundManager GetInstance
//    {
//        get { return instance; }
//        private set { instance = value; }
//    }
//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(this);
//            Debug.Log("This instance of" + GetType().Name + " already exist, delete the last one added.");
//            return;
//        }
//        else instance = this;

//        DontDestroyOnLoad(gameObject);
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        PlaySucces(transform.position);
//    }

//    private void PlaySFX(EventReference pSound, Vector3 pPosition)
//    {
//        RuntimeManager.PlayOneShot(pSound, pPosition);
//    }

//    public void PlaySucces(Vector3 pPosition)
//    {
//        PlaySFX(_sfxSucces, transform.position);
//    }
//}