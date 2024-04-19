        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    private AudioSource audioSource; // 音频源

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取音频源组件
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 播放/暂停按钮点击事件
    public void PlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause(); // 如果音乐正在播放，那么暂停
        }
        else
        {
            audioSource.Play(); // 如果音乐已暂停，那么播放
        }
    }
}
