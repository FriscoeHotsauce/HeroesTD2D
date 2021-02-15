using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string ENEMY_TAG = "Enemy";

    /**
    *   Get game objects tagged with 'Enemy' within a range of a unit
    */
    public static List<GameObject> getEnemiesInRange(Transform unit, float range){
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        List<GameObject> enemiesInRange = new List<GameObject>();
        foreach(GameObject enemy in allEnemies){
            if(Vector2.Distance(unit.position, enemy.transform.position) <= range){
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }
}