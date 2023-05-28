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
        float playerRadius=.7f;
        float movDistance= moveSpeed*Time.deltaTime;
        float playerHeight = .7f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight ,playerRadius,movDir,movDistance);
        if(!canMove){
            // eğer hareket yönünün tersine gidemiyorsak
            //sadece x yönüne izin ver
            Vector3 moveDirX= new Vector3(movDir.x,0,0).normalized;
            canMove =!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight ,playerRadius,moveDirX,movDistance);
            if(canMove){
                //eğer sadece x de hareket edebiliyorsak
                movDir=moveDirX;
            }else {
                // eğer sadece x yönünde hareket edemiyorsak  z ye izin ver
                Vector3 moveDirZ = new Vector3(0,0,movDir.z).normalized;
                canMove =!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight ,playerRadius,moveDirZ,movDistance);
                if(canMove){
                    //z yönüne izin ver
                    movDir=moveDirZ;
                }
                else{
                    //hiçbir yönde hareket edemiyor
                }
            }
        transform.position+=movDir*movDistance;
        }
        if(canMove){
            transform.position+=movDir*movDistance;
        }
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,movDir,Time.deltaTime*rotateSpeed);
 

    }
    public bool IsWalking(){
        return isWalking;
    }
}
