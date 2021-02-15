using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour, UnitController
{
    public List<GameObject> blockedUnits = new List<GameObject>();
    public int blockableUnits = 1;
    public int numberOfAttacks = 1;
    public int damage = 2;
    public float range = 0.2f;
    public float attackDelay = 20f;
    public float attackAvailableAt = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        findUnitsToBlock();
        if(attackAvailableAt <= Time.time && blockedUnits.Count > 0){
            Debug.Log("Yeet");
            attackEnemy();
            attackAvailableAt = Time.time + attackDelay;
        }
    }

    private void findUnitsToBlock(){
        if(blockedUnits.Count < blockableUnits){
           List<GameObject> enemiesInRange = Utils.getEnemiesInRange(transform, range);
        //    Debug.Log("Enemies in range: "+ enemiesInRange.Count);
           if(enemiesInRange.Count > 0){
                GameObject nearestEnemy = null;
                float nearestDistance = 10000f;
                foreach(GameObject enemy in enemiesInRange){
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    //only update the nearest enemy if they're not currently being blocked by another unit
                    if(distance < nearestDistance && !enemy.GetComponent<MoveAlongPath>().isBlocked()){
                        nearestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
                if(nearestEnemy != null){
                    blockUnit(nearestEnemy);
                }
           }
        }    
    }

    private void blockUnit(GameObject nearestEnemy){
        blockedUnits.Add(nearestEnemy);
        MoveAlongPath map = nearestEnemy.GetComponent<MoveAlongPath>();
        map.block(gameObject);
    }

    private void attackEnemy(){
        int attacksMade = 0;
        List<GameObject> survivingEnemies = new List<GameObject>();
        foreach(GameObject enemy in blockedUnits){
            if(attacksMade <= numberOfAttacks){
                attacksMade = attacksMade + 1;
                EnemyHealthController enemyHealthController = enemy.GetComponent<EnemyHealthController>();
                //if the enemy is not killed, add them back to the list
                if(!enemyHealthController.dealDamage(damage)){
                    survivingEnemies.Add(enemy);
                }
            } else {
                survivingEnemies.Add(enemy);
            }

        }
        blockedUnits = survivingEnemies;
    }

    public void die(){
        foreach(GameObject unit in blockedUnits){
            unit.GetComponent<MoveAlongPath>().unBlock();
        }
        Destroy(gameObject);
    }
    
}
