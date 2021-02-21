using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{

    public List<Transform> waypoints;
    public float moveSpeed = 2f;
    private float distanceToNextWaypoint = 0f;
    private int nextWaypointIndex = 0;
    private bool blocked;
    private GameObject blockingUnit;
    private DirectionalAnimationController dac;
    
    
    void Start()
    {
        //teleport to the first waypoint on start (or don't I don't really like that)
        // transform.position = waypoints[0].transform.position;
        dac = gameObject.GetComponent<DirectionalAnimationController>();
    }

    void Update()
    {
        if(!blocked){
            checkAndUpdateWaypoints();
            moveToNextWaypoint();
        }
    }

    private void checkAndUpdateWaypoints(){
        distanceToNextWaypoint = Vector2.Distance(transform.position, 
            waypoints[nextWaypointIndex].transform.position);
        
        //use a number close to but not zero because float math is bad and inconsistent
        if(distanceToNextWaypoint < 0.01f){
            if(nextWaypointIndex < waypoints.Count -1){
                nextWaypointIndex = nextWaypointIndex + 1;
            } else {
                despawnAndDealDamage();
            }
        }
    }

    public void block(GameObject blocker){
        blocked = true;
        blockingUnit = blocker;
    }

    public void unBlock(){
        blocked = false;
        blockingUnit = null;
    }

    public bool isBlocked(){
        return blocked;
    }

    public GameObject getBlockingUnit(){
        return blockingUnit;
    }

    private void moveToNextWaypoint(){
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[nextWaypointIndex].transform.position, moveSpeed * Time.deltaTime);
    }

    private void despawnAndDealDamage(){
        Destroy(gameObject);
        //todo ian.harris 2/14/2021 do some damage when there's something to do damage to
    }
}
