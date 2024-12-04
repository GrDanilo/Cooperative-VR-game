using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPos : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPos;
    [SerializeField] private GameObject[] SpawnPositions;

    private void Awake() 
    {
        int rand = Random.Range(0, 5);
        Debug.Log(rand);
        SpawnPositions[rand].SetActive(true);
        PlayerPos.transform.position = SpawnPositions[rand].transform.position;
    }
}
