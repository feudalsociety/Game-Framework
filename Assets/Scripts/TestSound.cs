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

    // ���߿��� drag & drop���� data sheet�� ��η� �� ���� ���̴�.
    public AudioClip audioClip;
    public AudioClip audioClip2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        // audio.PlayClipAtPoint(); // clip�� ������ ��ġ���� Ʋ���ش�.

        //audio.PlayOneShot(audioClip); 
        //audio.PlayOneShot(audioClip2); // ��ġ�� ���ÿ� �÷����Ѵ�.

        //float lifeTime = Mathf.Max(audioClip.length, audioClip.length);

        //// lifetime�� ª�ٸ� audioclip�� ���󰡼� audio�� ����Ǵ� ���߿� �����ȴ�.
        //GameObject.Destroy(gameObject, lifeTime); 
        
        i++;
        if(i % 2 == 0)
        // ��θ� �޴´�.
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
        else
        Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}
