using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, UnitController
{
    EnemyHealthController healthController;
    MoveAlongPath moveAlongPath;
    void Start()
    {
        healthController = gameObject.GetComponent<EnemyHealthController>();
        moveAlongPath = gameObject.GetComponent<MoveAlongPath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void die(){
        Destroy(gameObject);
    }
}
