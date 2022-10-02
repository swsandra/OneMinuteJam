using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PumpkinSpawner : MonoBehaviour
{
    [SerializeField] GameObject greenPumpkinPrefab;
    [SerializeField] GameObject[] orangePumpkinPrefabs;
    [SerializeField] int beltDirection; // Either 1 (right), or -1 (left)
    [SerializeField] AnimatedTile tile;
    float spawnRate;
    Pumpkin.PumpkinType[] validTypes = {Pumpkin.PumpkinType.Green, Pumpkin.PumpkinType.Uncarved, Pumpkin.PumpkinType.Unlit};

    void Start()
    {
        StartCoroutine(SpawnPumpkin());
    }

    IEnumerator SpawnPumpkin(){
        while (true){
            spawnRate = tile.m_MinSpeed/2; // Belt speed can change
            yield return new WaitForSeconds(1/spawnRate);
            Pumpkin.PumpkinType pumpkinType = validTypes[Random.Range(0, validTypes.Length)];
            GameObject go;
            GameObject pumpkin;
            if (pumpkinType == Pumpkin.PumpkinType.Green){
                pumpkin = greenPumpkinPrefab;
            }else { // Uncarved or unlit
                pumpkin = orangePumpkinPrefabs[Random.Range(0, orangePumpkinPrefabs.Length)];
            }
            // print("spawn type "+pumpkinType);
            go = Instantiate(pumpkin, transform.position, Quaternion.identity);
            go.GetComponent<Pumpkin>().ChangePumpkinType(pumpkinType, false);
            go.GetComponent<Pumpkin>().direction = beltDirection;
            go.GetComponent<Pumpkin>().tile = tile;
        }
    }
}
