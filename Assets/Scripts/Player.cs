using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    
    private Vector3 lastInteractDir;
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y); // karakteri zeminde yürümesini sağlar 

        isWalking = movDir != Vector3.zero;
        float playerRadius = .7f;
        float movDistance = moveSpeed * Time.deltaTime;
        float playerHeight = .7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, movDistance);
        if (!canMove)
        {
            // eğer hareket yönünün tersine gidemiyorsak
            //sadece x yönüne izin ver
            Vector3 moveDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, movDistance);
            if (canMove)
            {
                //eğer sadece x de hareket edebiliyorsak
                movDir = moveDirX;
            }
            else
            {
                // eğer sadece x yönünde hareket edemiyorsak  z ye izin ver
                Vector3 moveDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, movDistance);
                if (canMove)
                {
                    //z yönüne izin ver
                    movDir = moveDirZ;
                }
                else
                {
                    //hiçbir yönde hareket edemiyor
                }
            }
            transform.position += movDir * movDistance;
        }
        if (canMove)
        {
            transform.position += movDir * movDistance;
        }
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    private void HandleInteractions()
    {
        Vector2 inputVector= gameInput.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(inputVector.x,0f,inputVector.y);
        
        if(movDir!= Vector3.zero){
            lastInteractDir= movDir;
        }
        float interactDistance =2f;
        if(Physics.Raycast(transform.position,movDir,out RaycastHit raycastHit,interactDistance,counterLayerMask)){
                if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
                    clearCounter.Interact();
                }
        }
        
    }
}
