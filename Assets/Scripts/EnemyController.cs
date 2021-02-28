using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, UnitController
{
  HealthController healthController;
  MoveAlongPath moveAlongPath;
  DirectionalAnimationController dac;

  void Start(){
    healthController = gameObject.GetComponent<HealthController>();
    moveAlongPath = gameObject.GetComponent<MoveAlongPath>();
    dac = gameObject.GetComponent<DirectionalAnimationController>();
  }

  // Update is called once per frame
  void Update(){

  }

  public void die(){
    dac.die();
    Destroy(gameObject, 0.25f);
  }

  public bool isMoving()
  {
    return !moveAlongPath.isBlocked();
  }
}
