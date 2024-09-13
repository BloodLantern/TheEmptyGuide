using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [field: SerializeField] public EventReference noDrop { get; private set; }
    [SerializeField] public EventReference _backBtn;
    public EventReference yesDrop;

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


    public enum volType
    {
        master,
        music,
        sfx
    }

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
    }

    public float GetVolume(volType type)
    {
        switch (type)
        {
            case volType.master:
                _masterBus.getVolume(out _masterVolume);
                return _masterVolume;
            case volType.music:
                _musicBus.getVolume(out _musicVolume);
                return _musicVolume;
            case volType.sfx:
                _sfxBus.getVolume(out _SFXVolume);
                return _SFXVolume;
            default:
                return 0f;
        }
    }

    public void SetVolume(volType type,float pValue)
    {
        switch (type)
        {
            case volType.master:
                _masterBus.setVolume(pValue);
                break;
            case volType.music:
                _musicBus.setVolume(pValue);
                break;
            case volType.sfx:
                _sfxBus.setVolume(pValue);
                break;
            default:
                break;
        }
    }


    public void PlaySFX(EventReference pSound, Vector3 pPosition)
    {
        RuntimeManager.PlayOneShot(pSound, pPosition);
    }

    public void PlayBackButton(Vector3 pPos)
    {
        PlaySFX(_backBtn, pPos);
    }

}