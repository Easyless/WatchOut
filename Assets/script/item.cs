using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject clock; //Prefab�� ���� public ���� �Դϴ�.
    void SpawnEnemy()
    {
        float randomX = Random.Range(-3.5f, 3.5f); //���� ��Ÿ�� X��ǥ�� �������� ������ �ݴϴ�
        float randomZ = Random.Range(-60f, 130f); //���� ��Ÿ�� X��ǥ�� �������� ������ �ݴϴ�.
        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(clock, new Vector3(randomX, 80f, randomZ), Quaternion.identity); //������ ��ġ��, ȭ�� ���� ������ Enemy�� �ϳ� �������ݴϴ�.
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 10, 3); //3���� ����, SpawnEnemy�Լ��� 1�ʸ��� �ݺ��ؼ� ���� ��ŵ�ϴ�.

    }
    void Update()
    {

    }
}