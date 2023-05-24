using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed=7f;
    private bool isWalking;
    private void Update()
    {
        
        Vector2  inputVector  = new Vector2(0,0);// klavyede sadece iki boyut vardır bu yüzden başta 2 olarak tanımlanır
        if(Input.GetKey(KeyCode.W)){
            inputVector.y +=1;
        }else if(Input.GetKey(KeyCode.A)){
            inputVector.x -=1;
        }else if(Input.GetKey(KeyCode.S)){
            inputVector.y -=1;
        }else if(Input.GetKey(KeyCode.D)){
            inputVector.x +=1;
        }
         Vector3 movDir =new Vector3(inputVector.x,0f,inputVector.y); // karakteri zeminde yürümesini sağlar 
        inputVector =inputVector.normalized;
        isWalking = movDir !=Vector3.zero;
        transform.position+=movDir*moveSpeed*Time.deltaTime;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,movDir,Time.deltaTime*rotateSpeed);
 

    }
    public bool IsWalking(){
        return isWalking;
    }
}
