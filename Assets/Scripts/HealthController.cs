using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
  public int currentHealth;
  public int maxHealth = 100;
  public bool showHealth;
  public UnitController unitController;
  void Start()
  {
    currentHealth = maxHealth;
    showHealth = false;
    unitController = gameObject.GetComponent<UnitController>();
  }

  // Update is called once per frame
  void Update(){
    if (currentHealth != maxHealth){
      showHealthBar();
    }
  }

  public void showHealthBar(){

  }

  //if damage is fatal, return true; otherwise false
  public bool dealDamage(int damage){
    currentHealth = currentHealth - damage;
    if (currentHealth <= 0){
      unitController.die();
      return true;
    }
    return false;
  }

  public int getCurrentHealth(){
    return currentHealth;
  }
}
