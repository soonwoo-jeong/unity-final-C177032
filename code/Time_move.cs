using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��� ī���� ���� ǥ���Ѵ�
public class Forever_ShowTimer : MonoBehaviour
{

    void Update()
    {
        // ī���� ���� ǥ���Ѵ� 
        GetComponent<Text>().text = TimerManager.Timer;
    }
}