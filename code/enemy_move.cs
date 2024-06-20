using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Script : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField]
    private float speed;

    [SerializeField]
    GameObject gameobejct;

    [SerializeField]
    private GameObject Target;
    Rigidbody rigid;

    private BoxCollider boxcollider;
    bool isPlayerDied=false;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Target.transform.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
    public void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDied = true;
            SceneManager.LoadScene("exit");
        }
    }


}
