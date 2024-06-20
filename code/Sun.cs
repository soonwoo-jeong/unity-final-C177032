using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    [SerializeField]
    private float secondPerTealTimeSecond;

    private bool isNight = false;
    void Start()
    {
        
    }

 
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerTealTimeSecond * Time.deltaTime);
    }
}
