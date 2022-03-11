using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMangaer 
{
    // AudioSource도 Unity Component이므로 new를 하면 안되고 ingame object 생성후 compnent를 붙여야함
    // 지금은 2개만 사용함
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    // Caching, Scene이 바뀔 때마다 안 없어지고 추가만 됨, Clear 함수 필요
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // MP3 Player --> Audio Source
    // MP3 음원 --> Audio Clip
    // Listener --> Audio Listener (1 scene에 하나만 있으면 됨)

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root); // Scene을 이동할 때도 살아 남는다.

            // reflection 기능 이용
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject(soundNames[i]);
                _audioSources[i] = go.AddComponent<AudioSource>();
                // UI에서 SetParent는 RectTransform이기 때문에
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    // Cache 날리기, Scene이 이동할 때 Clear 하지만, Sound말고도 다른걸 Clear할 수 있으므로
    // Manager에서 Clear를 담당하도록 한다.
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    // 하나의 버전에서 다른 버전을 사용, 복붙하면 코드관리가 쉽지 않을 것이다.
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    // AudioCilp을 직접 넣는 버전
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
        else // Effect의 경우 자주 load하면 부하가 걸리므로 caching
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
        else // Effect의 경우 자주 load하면 부하가 걸리므로 caching
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
