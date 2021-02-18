using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAnimationController : MonoBehaviour
{
    public enum direction { left, right, up, down}

    public string leftAnimationName;
    public string rightAnimationName;
    public string upAnimationName;
    public string downAnimationName;
    public string idleLeftAnimationName;
    public string idleRightAnimationName;
    public string idleUpAnimationName;
    public string idleDownAnimationName;

    public Animator animator;
    public MoveAlongPath map;

    private Vector3 previousPosition;
    private direction previousDirection;
    private bool transitionToBlocked;

    // Start is called before the first frame update
    void Start()
    {
        previousDirection = direction.down;
        previousPosition = transform.position;
        animator = gameObject.GetComponent<Animator>();
        map = gameObject.GetComponent<MoveAlongPath>();
    }

    // Update is called once per frame
    void Update()
    {
        direction deltaDirection = determineDirectionOfMotion();

        if(map.isBlocked() && !transitionToBlocked){
            switch(previousDirection){
                case direction.left:
                    animator.Play(idleLeftAnimationName);
                    break;
                case direction.right:
                    animator.Play(idleRightAnimationName);
                    break;
                case direction.up:
                    animator.Play(idleUpAnimationName);
                    break;
                case direction.down:
                    animator.Play(idleDownAnimationName);
                    break;
            }
            transitionToBlocked = true;
        } else if(deltaDirection == previousDirection){
            return;
        } else {
            Debug.Log("Direction changed! - "+deltaDirection);
            switch(deltaDirection){
                case direction.left:
                    animator.Play(leftAnimationName);
                    break;
                case direction.right:
                    animator.Play(rightAnimationName);
                    break;
                case direction.up:
                    animator.Play(upAnimationName);
                    break;
                case direction.down:
                    animator.Play(downAnimationName);
                    break;
            }
            previousDirection = deltaDirection;
        }
        
    }

    public void unBlock(){
        transitionToBlocked = false;
    }

    private direction determineDirectionOfMotion(){

        Vector3 positionDelta = transform.position - previousPosition;
        direction deltaDirection = direction.down;
        if(Math.Abs(positionDelta.x) > Math.Abs(positionDelta.y)){
            //the x axis has a greater magnitute, we are moving primarily left or right
            if(positionDelta.x >= 0){
                deltaDirection = direction.right;
            } else {
                deltaDirection = direction.left;
            }
        } else {
            //the y axis has a greater magnitude, we are moving primarily up or down
            if(positionDelta.y >= 0){
                deltaDirection = direction.up;
            } else {
                deltaDirection = direction.down;
            }
        }
        previousPosition = transform.position;
        return deltaDirection;
    }
}
