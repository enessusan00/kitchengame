using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {//obje yok 
            if (player.HasKitchenObject())
            {
                // playerda obje var
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    // playerda obje var ama tarifi yok 

                player.GetKitchenObject().SetKitchenObjectParent(this);

                }
            }
            else
            {
                // player obje yok
            }

        }
        else
        {
            // masada obje var
            if (player.HasKitchenObject())
            {
                //playerda da obje var
            }
            else
            {
                //player obje yok
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        //masada obje var ve obje kesilebilir bu tekrar kesilmesini engeller
        
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO otuputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(otuputKitchenObjectSO, this);
        }

    }
        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
         foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true ;
            }
        }
            return false;

    }
    // obje dönüştürme
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
           
        }
         return null;
    }
}
