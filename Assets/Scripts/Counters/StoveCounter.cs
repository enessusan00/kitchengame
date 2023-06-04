using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter
{
    // Durum değiştiğinde tetiklenecek olay
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    // Olası durumlar
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray; // Kızartma tariflerinin bulunduğu dizi
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray; // Yanma tariflerinin bulunduğu dizi

    private State state; // Mevcut durum
    private FryingRecipeSO fryingRecipeSO; // Kızartma tarifi
    private BurningRecipeSO burningRecipeSO; // Yanma tarifi
    private float fryingTimer; // Kızartma süresi
    private float burningTimer; // Yanma süresi

    private void Start()
    {
        state = State.Idle; // Başlangıçta durum Idle olarak ayarlanır
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeWithSOInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimecMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Masa üzerinde nesne yok

            if (player.HasKitchenObject())
            {
                // Oyuncunun elinde nesne var

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Oyuncunun elinde pişirilebilir nesne var

                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeWithSOInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                }
            }
            else
            {
                // Oyuncunun elinde nesne yok
            }
        }
        else
        {
            // Masa üzerinde nesne var

            if (player.HasKitchenObject())
            {
                // Oyuncunun elinde de nesne var
            }
            else
            {
                // Oyuncunun elinde nesne yok

                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Verilen giriş nesnesi ile kızartma tarifleri arasında eşleşme kontrolü yapar
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeWithSOInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Verilen giriş nesnesine karşılık gelen çıkış nesnesini döndürür
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeWithSOInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeWithSOInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Verilen giriş nesnesine göre kızartma tarifini döndürür
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeWithSOInput(KitchenObjectSO inputKitchenObjectSO)
    {
        // Verilen giriş nesnesine göre yanma tarifini döndürür
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
