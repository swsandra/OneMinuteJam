using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PumpkinSpawner : MonoBehaviour
{
    [SerializeField] GameObject greenPumpkinPrefab;
    [SerializeField] GameObject[] orangePumpkinPrefabs;
    float spawnRate;
    Pumpkin.PumpkinType[] validTypes = {Pumpkin.PumpkinType.Green, Pumpkin.PumpkinType.Uncarved, Pumpkin.PumpkinType.Unlit};
    [SerializeField] AnimatedTile[] tiles;
    [SerializeField] Transform[] spawnPositions;
    int[] beltDirection = {-1, 1};      // 1 (right), or -1 (left)

    void Start()
    {
        StartCoroutine(SpawnPumpkin());
    }

    IEnumerator SpawnPumpkin() {
        while (true) {
            spawnRate = tiles[0].m_MinSpeed/2; // Belt speed can change
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

            int randomSpawn = Random.Range(0, spawnPositions.Length);
            go = Instantiate(pumpkin, spawnPositions[randomSpawn].position, Quaternion.identity);
            go.GetComponent<Pumpkin>().ChangePumpkinType(pumpkinType, false);
            go.GetComponent<Pumpkin>().direction = beltDirection[randomSpawn];
            go.GetComponent<Pumpkin>().tile = tiles[randomSpawn];
        }
    }
}
