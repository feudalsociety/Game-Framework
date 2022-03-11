using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMangaer 
{
    // AudioSource�� Unity Component�̹Ƿ� new�� �ϸ� �ȵǰ� ingame object ������ compnent�� �ٿ�����
    // ������ 2���� �����
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    // Caching, Scene�� �ٲ� ������ �� �������� �߰��� ��, Clear �Լ� �ʿ�
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // MP3 Player --> Audio Source
    // MP3 ���� --> Audio Clip
    // Listener --> Audio Listener (1 scene�� �ϳ��� ������ ��)

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root); // Scene�� �̵��� ���� ��� ���´�.

            // reflection ��� �̿�
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject(soundNames[i]);
                _audioSources[i] = go.AddComponent<AudioSource>();
                // UI���� SetParent�� RectTransform�̱� ������
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    // Cache ������, Scene�� �̵��� �� Clear ������, Sound���� �ٸ��� Clear�� �� �����Ƿ�
    // Manager���� Clear�� ����ϵ��� �Ѵ�.
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    // �ϳ��� �������� �ٸ� ������ ���, �����ϸ� �ڵ������ ���� ���� ���̴�.
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    // AudioCilp�� ���� �ִ� ����
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null) return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect�� ��� ���� load�ϸ� ���ϰ� �ɸ��Ƿ� caching
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else // Effect�� ��� ���� load�ϸ� ���ϰ� �ɸ��Ƿ� caching
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }
}
