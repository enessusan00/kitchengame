 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        // player obje taşımıyorsa
        if (!player.HasKitchenObject())
        {
             KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

        }

    }


}
