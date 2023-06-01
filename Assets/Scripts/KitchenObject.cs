using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void SetClearCounter(ClearCounter clearCounter)
    {   //parent değiştiğinde mevcudu temizler
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        // bu olay gerçekleşmez ama aksi durumda kontrol edilmeli
        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter Zaten KitchenObject'e sahip");
        }
        clearCounter.SetKitchenObject(this);    
        // obje parenti değiştiğinde konumu günceller
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}

