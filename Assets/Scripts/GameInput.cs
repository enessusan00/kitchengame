using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    
    private void Awake() {
     playerInputActions= new PlayerInputActions();
     playerInputActions.Player.Enable();
}
    public Vector2 GetMovementVectorNormalized(){
         Vector2  inputVector  =playerInputActions.Player.Move.ReadValue<Vector2>();// klavyede sadece iki boyut vardır bu yüzden başta 2 olarak tanımlanır

         inputVector =inputVector.normalized;
         return inputVector;
    }
}
