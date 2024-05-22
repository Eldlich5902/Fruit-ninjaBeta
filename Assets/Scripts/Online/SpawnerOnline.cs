﻿using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnerOnline : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] onlineFruitPrefabs;
    public GameObject bombPrefab;
    [Range(0f, 1f)] public float bombChance = 0.10f;/*Bomb spawn rate*/
    /*Độ trễ spawn*/
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    /*Biến số cho góc spawn*/
    public float minAngle = -15f;
    public float maxAngle = 15f;
    /*Biến số cho lực phóng vật thể*/
    public float minForce = 18f;
    public float maxForce = 22f;
    /*Time tồn tại của vật thể*/
    public float maxLifetime = 5f;

    private void Awake()/*gán spawnArea theo collider*/
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()/*Bật spawn*/
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()/*Tắt spawn*/
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()/*Quy trình spawn*/
    {
        yield return new WaitForSeconds(2f);/*Độ trễ ban đầu*/

        while (enabled)
        {
            GameObject prefab = onlineFruitPrefabs[Random.Range(0, onlineFruitPrefabs.Length)];
            /*Bomb spawn*/
            if (Random.value < bombChance) {
                prefab = bombPrefab;
            }
            /*Vị trí spawn 3D*/
            Vector3 position = new Vector3
            {
                x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            };
            /*Độ nghiêng khi spawn*/
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0);//Random.Range(minAngle, maxAngle));
            /*Spawn vật thể*/
            //GameObject fruit = Instantiate(prefab, position, rotation);
            GameObject onlineFruit = PhotonNetwork.Instantiate(prefab.name, position, rotation);
            Destroy(onlineFruit, maxLifetime);
            /*Tạo lực phóng*/
            float force = 20;//Random.Range(minForce, maxForce);
            onlineFruit.GetComponent<Rigidbody>().AddForce(onlineFruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(1);//(Random.Range(minSpawnDelay, maxSpawnDelay));/*Đợi trong 1 khoảng time ngẫu nhiên rồi lặp lại*/
        }
    }

}
