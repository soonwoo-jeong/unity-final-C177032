using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 계속 카운터 값을 표시한다
public class Forever_ShowTimer : MonoBehaviour
{

    void Update()
    {
        // 카운터 값을 표시한다 
        GetComponent<Text>().text = TimerManager.Timer;
    }
}