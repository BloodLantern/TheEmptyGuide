using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [field: SerializeField] public EventReference _sfxSucces { get; private set; }
    [field: SerializeField] public EventReference _sfxDeath { get; private set; }
    [field: SerializeField] public EventReference _sfxDamagePlayer { get; private set; }
    [field: SerializeField] public EventReference _sfxDamageFoe { get; private set; }
    [field: SerializeField] public EventReference _sfxWin { get; private set; }
    [field: SerializeField] public EventReference _sfxKeyboard { get; private set; }
    [field: SerializeField] public EventReference _sfxValidateWord { get; private set; }
    [field: SerializeField] public EventReference _sfxJumpNSlide { get; private set; }

    private static SoundManager _instance;

    //Sound volume management
    private const string PATH_BUS_MASTER = "vca:/Master";
    private const string PATH_BUS_SFX = "vca:/SFX";
    private const string PATH_BUS_MUS = "vca:/Music";

    private float _masterVolume = 1f;
    private float _musicVolume = 1f;
    private float _SFXVolume = 1f;

    public VCA _masterBus;
    public VCA _sfxBus;
    public VCA _musicBus;

    private SoundManager() { }

    public static SoundManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            Debug.Log("This instance of" + GetType().Name + " already exist, delete the last one added.");
            return;
        }
        else _instance = this;

        DontDestroyOnLoad(gameObject);

        //Get VCAs
        _masterBus = RuntimeManager.GetVCA(PATH_BUS_MASTER);
        _musicBus = RuntimeManager.GetVCA(PATH_BUS_MUS);
        _sfxBus = RuntimeManager.GetVCA(PATH_BUS_SFX);

    }

    // Start is called before the first frame update
    void Start()
    {
        PlaySucces(transform.position);
    }

    public void PlaySFX(EventReference pSound, Vector3 pPosition)
    {
        RuntimeManager.PlayOneShot(pSound, pPosition);
    }

    public void PlaySucces(Vector3 pPosition)
    {
        PlaySFX(_sfxSucces, transform.position);
    }

}