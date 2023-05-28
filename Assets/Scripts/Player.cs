using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update()
    {
       Vector2 inputVector =gameInput.GetMovementVectorNormalized() ;
       
       
         Vector3 movDir =new Vector3(inputVector.x,0f,inputVector.y); // karakteri zeminde yürümesini sağlar 
       
        isWalking = movDir !=Vector3.zero;
        transform.position+=movDir*moveSpeed*Time.deltaTime;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,movDir,Time.deltaTime*rotateSpeed);
 

    }
    public bool IsWalking(){
        return isWalking;
    }
}
