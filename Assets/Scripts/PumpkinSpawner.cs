using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawner : MonoBehaviour
{
    [SerializeField] GameObject greenPumpkinPrefab;
    [SerializeField] GameObject[] orangePumpkinPrefabs;
    public float spawnRate = 0.5357f;
    [SerializeField] float pumpkinSpeed = 1;
    Pumpkin.PumpkinType[] validTypes = {Pumpkin.PumpkinType.Green, Pumpkin.PumpkinType.Uncarved, Pumpkin.PumpkinType.Unlit};
    [SerializeField] Transform[] spawnPositions;
    int[] beltDirection = {-1, 1};      // 1 (right), or -1 (left)

    void Start()
    {
        StartCoroutine(SpawnPumpkin());
        StartCoroutine(DoubleRateCoroutine());
    }

    IEnumerator DoubleRateCoroutine() {
        yield return new WaitForSeconds(44.77f);
        spawnRate /= 2;
    }

    IEnumerator SpawnPumpkin() {
        yield return new WaitForSeconds(1.2f);
        while (true) {
            yield return new WaitForSeconds(spawnRate);
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
            go.GetComponent<Pumpkin>().speed = pumpkinSpeed;
        }
    }
}
