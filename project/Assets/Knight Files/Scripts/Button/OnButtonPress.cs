using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public static float left = 0;
    // 延迟时间  
    private float delay = 0.2f;

    // 按钮是否是按下状态  
    private bool isDown = false;

    // 按钮最后一次是被按住状态时候的时间  
    private float lastIsDownTime;

    public GameObject player;
    public PlayerControl playerControl;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update () {
        // 如果按钮是被按下状态  
        if (isDown)
        {
            // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒  
            if (Time.time - lastIsDownTime > delay)
            {
                // 触发长按方法  
                Debug.Log("长按");
                // 记录按钮最后一次被按下的时间  
                lastIsDownTime = Time.time;

            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("left");
        playerControl.h = -1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerControl.h = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerControl.h = 0;
    }
}
