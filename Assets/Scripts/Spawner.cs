using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int unitsToSpawn = 0;
    public float spawnDelay = 0f;
    public Transform prefab;
    public List<Transform> pathToFollow;
    private int unitsSpawned;
    private float nextSpawnTime;

    void Start(){
        nextSpawnTime = 0.0f;
        unitsSpawned = 0;
    }

    void FixedUpdate(){
        if(Time.time > nextSpawnTime && unitsSpawned < unitsToSpawn){
            Transform created = Instantiate(prefab, transform.position, transform.rotation);
            created.name = transform.name + " - " + unitsSpawned;
            MoveAlongPath map = created.GetComponent<MoveAlongPath>();
            map.setWaypoints(pathToFollow);
            unitsSpawned = unitsSpawned + 1;
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
