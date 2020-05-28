using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackMusic : MonoBehaviour
{
    public AudioClip music;
    private AudioSource back;
    public Slider slider;
    void Start()
    {
        back = this.GetComponent<AudioSource>();
        back.loop = true; //设置循环播放  
        back.volume = 0.5f;//设置音量最大，区间在0-1之间
        back.clip = music;
        back.Play(); //播放背景音乐，
    }
    void Update()
    {
        back.volume = slider.value;
    }
   public void SliderClick(float value)
    {
        print ("当前值为"+value);

    }
}
