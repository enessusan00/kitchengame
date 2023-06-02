using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {//obje yok 
        if(player.HasKitchenObject()){
            // playerda obje var
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }else{
            // player obje yok
        }

        }else{
            // masada obje var
            if(player.HasKitchenObject()){
                //playerda da obje var
            }else{
                //player obje yok
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
