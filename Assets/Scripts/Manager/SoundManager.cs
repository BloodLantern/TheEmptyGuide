//using UnityEngine;
//using FMODUnity;
//using FMOD.Studio;
//using JetBrains.Annotations;
//using Unity.VisualScripting;

//    public class SoundList

//{
//    //player

//    public const string Collectible_rare = "event:/Player/Collectible_rare";

//    public const string Dash = "event:/Player/Dash";

//    public const string Death = "event:/Player/Death";

//    public const string Footstep = "event:/Player/Footstep";

//    public const string Jump = "event:/Player/jump";

//    public const string Killzone = "event:/Player/Killzone";

//    public const string Landing = "event:/Player/Landing";

//    public const string Respawn = "event:/Player/Respawn";

//    //public const string Pickup_collectible = "event:/Character/Door Close";

//    //public const string Checpoint_Activation = "event:/Character/Door Close";

//    //public const string Checkpoint_Idle = "event:/Character/Door Close";

//    //platform

//    public const string Courant_ascendant = "event:/Character/Door Close";

//    public const string Killzone_electric = "event:/Character/Door Close";

//    public const string Platform_Destruct = "event:/Character/Door Close";

//    public const string Platform_Disappear = "event:/Character/Door Close";

//    //Musique

//    public const string Musique_lvl1_loop = "event:/Character/Door Close";

//    public const string Musique_lvl2_loop = "event:/Musique/Musique_lvl1_loop";

//    public const string Musique_menu_loop = "event:/Musique/Musique_lvl2_loop";

//    //UI

//    public const string start_game = "event:/Character/Door Close";

//    public const string UI_click = "event:/Character/Door Close";



//}



//public class SoundManager : MonoBehaviour
//{
//    [SerializeField] private StudioBankLoader _studioBankLoader;

//    private Dictionary<string, FMOD.Studio.EventInstance> _SFXDictionnary;


//    private const string PATH_BUS_MASTER = "vca:/Master";
//    private const string PATH_BUS_SFX = "vca:/SFX";
//    private const string PATH_BUS_MUS = "vca:/Music";

//    private float _masterVolume = 1f;
//    private float _musicVolume = 1f;
//    private float _SFXVolume = 1f;

//    public VCA _masterBus;
//    public VCA _sfxBus;
//    public VCA _musicBus;

//    private EventInstance ambienceEventInstance;
//    private EventInstance musicEventInstance;


//    private static SoundManager instance;

//    private SoundManager() { }

//    public static SoundManager GetInstance { get { return instance; } private set { instance = value; } }

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(this);
//            Debug.Log("This instance already exist, destroying the last one added");
//            return;
//        }
//        else instance = this;


//        _masterBus = RuntimeManager.GetVCA(PATH_BUS_MASTER);
//        _musicBus = RuntimeManager.GetVCA(PATH_BUS_MUS);
//        _sfxBus = RuntimeManager.GetVCA(PATH_BUS_SFX);

//        _SFXDictionnary = new Dictionary<string, FMOD.Studio.EventInstance>();
//    }

//    private void Start()
//    {
//        ConnectAllEvent();
//    }

//    private EventInstance GetEventInstance(string pEventPath)
//    {
//        EventInstance lInstance;

//        if (_SFXDictionnary.ContainsKey(pEventPath)) lInstance = _SFXDictionnary[pEventPath];

//        else
//        {
//            lInstance = RuntimeManager.CreateInstance(pEventPath);

//            _SFXDictionnary.Add(pEventPath, lInstance);

//        }

//        return lInstance;
//    }

//    public void PlaySound(string pEventPath)
//    {
//        GetEventInstance(pEventPath).start();
//    }

//    public void PlayJump()
//    {
//        PlaySound(SoundList.Jump);
//    }


//    /// <summary>
//    /// Initialize and play the music
//    /// </summary>
//    /// <param name="pMusicEventReference">Event to play as music from SoundEvent</param>
//    public void InitializeMusic(EventReference pMusicEventReference)
//    {
//        musicEventInstance = CreateInstance(pMusicEventReference);
//        musicEventInstance.start();

//    }

//    public void StopMusic()
//    {
//        musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//    }

//    public void StopAmbience()
//    {
//        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//    }

//    /// <summary>
//    /// Set parameters to music to add effects
//    /// </summary>
//    /// <param name="pParameters">Parameters name from FMOD</param>
//    /// <param name="pValue">Value to the parameters</param>
//    public void SetMusicParameters(string pParameters, float pValue)
//    {
//        musicEventInstance.setParameterByName(pParameters, pValue);
//    }

//    /// <summary>
//    /// Set parameters to music to add effects
//    /// </summary>
//    /// <param name="pParameters">Parameters name from FMOD</param>
//    /// <param name="pValue">Value to the parameters</param>
//    private void SetAmbienceParameters(string pParameters, float pValue)
//    {
//        ambienceEventInstance.setParameterByName(pParameters, pValue);
//    }

//    private void InitializeAmbience(EventReference pAmbienceEventReference)
//    {
//        ambienceEventInstance = CreateInstance(pAmbienceEventReference);
//        ambienceEventInstance.start();
//    }

//    /// <summary>
//    /// Play a SFX
//    /// </summary>
//    /// <param name="pSFX">Event to play from SoundEvents</param>
//    /// <param name="pPos">Position where the soudn should be played</param>
//    public void PlaySFX(EventReference pSFX, Vector3 pPos)
//    {
//        RuntimeManager.PlayOneShot(pSFX, pPos);
//    }



//    public EventInstance CreateInstance(EventReference pEventReference)
//    {
//        EventInstance lEventInstance = RuntimeManager.CreateInstance(pEventReference);
//        return lEventInstance;
//    }

//    #region SoundSettings

//    /// <summary>
//    /// Change the volume of a bus
//    /// </summary>
//    /// <param name="pVCA">VCA volume to change</param>
//    /// <param name="pVolume">New volume value</param>
//    /// <param name="pSettingsVolume">Volume that will be stock inside SettingsManager for the mute</param>
//    private void OnVolumeChanged(VCA pVCA, float pVolume, float pSettingsVolume)
//    {
//        pSettingsVolume = pVolume;
//        Debug.Log(pVolume + " : " + pSettingsVolume);
//        pVCA.setVolume(pSettingsVolume);
//    }

//    /// <summary>
//    /// Change the volume of Master Bus
//    /// </summary>
//    /// <param name="pVolumeChanged">New volume value for Master bus</param>
//    public void OnMasterVolumeChanged(float pVolumeChanged)
//    {
//        OnVolumeChanged(_masterBus, pVolumeChanged, _masterVolume);
//    }

//    /// <summary>
//    /// Change the volume of Music Bus
//    /// </summary>
//    /// <param name="pVolumeChanged">New volume value for Music bus</param>
//    public void OnMusicVolumeChanged(float pVolumeChanged)
//    {
//        OnVolumeChanged(_musicBus, pVolumeChanged, _musicVolume);
//    }

//    /// <summary>
//    /// Change the volume of SFX Bus
//    /// </summary>
//    /// <param name="pVolumeChanged">New volume value for SFX bus</param>
//    public void OnSFXVolumeChanged(float pVolumeChanged)
//    {
//        OnVolumeChanged(_sfxBus, pVolumeChanged, _SFXVolume);
//    }

//    private void OnMasterVolumeMuted(bool pMasterMuted)
//    {
//        SettingsManager._isMasterVolumeMuted = pMasterMuted;
//        if (SettingsManager._isMasterVolumeMuted) _masterBus.setVolume(0);
//        else
//        {
//            _masterBus.setVolume(_masterVolume);
//        }

//    }

//    private void OnMusicVolumeMuted(bool pMusicMuted)
//    {
//        SettingsManager._isMusicVolumeMuted = pMusicMuted;
//        if (SettingsManager._isMusicVolumeMuted) _musicBus.setVolume(0);
//        else
//        {
//            _musicBus.setVolume(_musicVolume);
//        }
//    }

//    private void OnSFXVolumeMuted(bool pSFXMuted)
//    {
//        SettingsManager._isSFXVolumeMuted = pSFXMuted;
//        if (SettingsManager._isSFXVolumeMuted) _sfxBus.setVolume(0);
//        else
//        {
//            _sfxBus.setVolume(_SFXVolume);
//        }
//    }
//    #endregion


//    private void ConnectAllEvent()
//    {
//        SettingsScreen.OnMasterVolumeChanged += OnMasterVolumeChanged;
//        SettingsScreen.OnMusicVolumeChanged += OnMusicVolumeChanged;
//        SettingsScreen.OnSFXVolumeChanged += OnSFXVolumeChanged;

//        SettingsScreen.OnMasterMuted += OnMasterVolumeMuted;
//        SettingsScreen.OnMusicMuted += OnMusicVolumeMuted;
//        SettingsScreen.OnSFXMuted += OnSFXVolumeMuted;
//    }

//    private void DisconnectAllEvent()
//    {
//        SettingsScreen.OnMasterVolumeChanged -= OnMasterVolumeChanged;
//        SettingsScreen.OnMusicVolumeChanged -= OnMusicVolumeChanged;
//        SettingsScreen.OnSFXVolumeChanged -= OnSFXVolumeChanged;

//        SettingsScreen.OnMasterMuted -= OnMasterVolumeMuted;
//        SettingsScreen.OnMusicMuted -= OnMusicVolumeMuted;
//        SettingsScreen.OnSFXMuted -= OnSFXVolumeMuted;
//    }


//}