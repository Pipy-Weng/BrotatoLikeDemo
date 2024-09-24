using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab;

    Transform player;
    List<Vector2> gunPositions = new List<Vector2>();

    int spawnedGuns = 0;

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        gunPositions.Add(new Vector2(-0.7f, -0.25f));
        gunPositions.Add(new Vector2(0.7f, -0.25f));

        gunPositions.Add(new Vector2(-1.4f, -0.2f));
        gunPositions.Add(new Vector2(1.4f, -0.2f));

        gunPositions.Add(new Vector2(-1f, -0.5f));
        gunPositions.Add(new Vector2(1f, -0.5f));

    }

    private void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.G)) {
            AddGun();
        }
    }

    void AddGun()
    {
        var pos = gunPositions[spawnedGuns];

        var newGun = Instantiate(gunPrefab, pos, Quaternion.identity);

        newGun.GetComponent<Gun>().SetOffset(pos);
        spawnedGuns++;
    }

}
