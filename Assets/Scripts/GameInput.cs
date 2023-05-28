using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Vector2 GetMovementVectorNormalized(){
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
         inputVector =inputVector.normalized;
         return inputVector;
    }
}
