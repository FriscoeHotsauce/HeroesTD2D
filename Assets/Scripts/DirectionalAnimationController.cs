using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAnimationController : MonoBehaviour
{
    public enum direction { left, right, up, down}
    public enum state { moving, idle, dying }

    public string moveLeftAnimationName;
    public string moveRightAnimationName;
    public string moveUpAnimationName;
    public string moveDownAnimationName;
    public string idleLeftAnimationName;
    public string idleRightAnimationName;
    public string idleUpAnimationName;
    public string idleDownAnimationName;
    public string attackLeftAnimationName;
    public string attackRightAnimationName;
    public string attackUpAnimationName;
    public string attackDownAnimationName;
    public string dieLeftAnimationName;
    public string dieRightAnimationName;
    public string dieUpAnimationName;
    public string dieDownAnimationName;


    public Animator animator;
    public UnitController unitController;

    private Vector3 previousPosition;
    public direction previousDirection;
    private state previousState;

    // Start is called before the first frame update
    void Start()
    {
        previousDirection = direction.down;
        previousPosition = transform.position;
        previousState = state.idle;
        animator = gameObject.GetComponent<Animator>();
        unitController = gameObject.GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction deltaDirection = determineDirectionOfMotion();
        state currentState = determineCurrentState();
        bool stateChanged = currentState != previousState;
        bool directionChanged = previousDirection != deltaDirection;

        if(stateChanged || directionChanged){
            // Debug.Log("StateChange: "+ stateChanged +" | "+"DirectionChange: "+directionChanged);
            if(unitController.isMoving()){
                // Debug.Log("Moving "+ deltaDirection);
                switch(deltaDirection){
                    case direction.up:
                        animator.Play(moveUpAnimationName);
                        break;
                    case direction.down:
                        animator.Play(moveDownAnimationName);
                        break;
                    case direction.left:
                        animator.Play(moveLeftAnimationName);
                        break;
                    case direction.right:
                        animator.Play(moveRightAnimationName);
                        break;
                }
            } else {
                // Debug.Log("Idling "+previousDirection);
                switch(previousDirection){
                    case direction.up:
                        animator.Play(idleUpAnimationName);
                        break;
                    case direction.down:
                        animator.Play(idleDownAnimationName);
                        break;
                    case direction.left:
                        animator.Play(idleLeftAnimationName);
                        break;
                    case direction.right:
                        animator.Play(idleRightAnimationName);
                        break;
                }
            }
        }
        previousDirection = deltaDirection;
        previousState = currentState;
    }

    private state determineCurrentState(){
        if(gameObject.activeSelf && unitController.isMoving()){
            return state.moving;
        } else {
            return state.idle;
        }
    }

    public void attack(){
        switch(previousDirection){
            case direction.left:
                animator.Play(attackLeftAnimationName);
                break;
            case direction.right:
                animator.Play(attackRightAnimationName);
                break;
            case direction.up:
                animator.Play(attackUpAnimationName);
                break;
            case direction.down:
                animator.Play(attackDownAnimationName);
                break;

        }
    }

    public void die(){
        switch(previousDirection){
            case direction.left:
                animator.Play(dieLeftAnimationName);
                break;
            case direction.right:
                animator.Play(dieRightAnimationName);
                break;
            case direction.up:
                animator.Play(dieUpAnimationName);
                break;
            case direction.down:
                animator.Play(dieDownAnimationName);
                break;
        }
    }

    private direction determineDirectionOfMotion(){

        Vector3 positionDelta = transform.position - previousPosition;
        direction deltaDirection = direction.down;
        if(Math.Abs(positionDelta.x) > Math.Abs(positionDelta.y)){
            //the x axis has a greater magnitute, we are moving primarily left or right
            if(positionDelta.x > 0){
                deltaDirection = direction.right;
            } else {
                deltaDirection = direction.left;
            }
        } else {
            //the y axis has a greater magnitude, we are moving primarily up or down
            if(positionDelta.y > 0){
                deltaDirection = direction.up;
            } else {
                deltaDirection = direction.down;
            }
        }
        previousPosition = transform.position;
        return deltaDirection;
    }
}
