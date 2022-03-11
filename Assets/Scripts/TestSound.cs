using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 나중에는 drag & drop보다 data sheet에 경로로 들어가 있을 것이다.
    public AudioClip audioClip;
    public AudioClip audioClip2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        // audio.PlayClipAtPoint(); // clip을 지정한 위치에서 틀어준다.

        //audio.PlayOneShot(audioClip); 
        //audio.PlayOneShot(audioClip2); // 겹치면 동시에 플레이한다.

        //float lifeTime = Mathf.Max(audioClip.length, audioClip.length);

        //// lifetime이 짧다면 audioclip이 날라가서 audio가 실행되는 도중에 중지된다.
        //GameObject.Destroy(gameObject, lifeTime); 
        
        i++;
        if(i % 2 == 0)
        // 경로를 받는다.
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
        else
        Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}
