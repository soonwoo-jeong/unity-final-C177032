using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject SpawnObject; //Prefab���� ������ ������Ʈ

    public int xPos;
    public int zPos;
    public int yPos;
    public int enemyCount;

    private BoxCollider boxcollider;


    void Start()
    {
        StartCoroutine(StartSpawing());
        boxcollider = GetComponent<BoxCollider>();
    }

    IEnumerator StartSpawing()
    {
        while (enemyCount < 1000)
        {
            xPos = Random.Range(0, 100); //Position X�� -4~ 8 ������ ���� ���� ����
            zPos = Random.Range(0, 100); // Position Z�� 6 ~ 11 ������ ���� ���� ����
            yPos = Random.Range(8, 12); // 2~12 ���� ����

            //Prefab ������Ʈ�� �ν��Ͻ�ȭ ���Ѽ� ȣ���Ѵ�. 
            Instantiate(SpawnObject, new Vector3(xPos,yPos, zPos), Quaternion.identity); //(������ ���, ��ġ, ����)
            yield return new WaitForSeconds(2);
            enemyCount += 1;
        }
    }
}

