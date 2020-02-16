using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0,1)]
    public float volume = 0.7f;
    public bool loop = false;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i<sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound" + sounds[i].name);
            _go.transform.SetParent(transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i<sounds.Length;i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        //No sound with name
        Debug.LogError("SoundManager: No sound with name " + _name);
    }

    public void PauseSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        //No sound with name
        Debug.LogError("SoundManager: No sound with name " + _name);
    }
}
