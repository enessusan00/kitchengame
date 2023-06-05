using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;   
    private IHasProgress hasProgress;   
    void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress==null){
            Debug.LogError("GameObject"+hasProgressGameObject +"IHasProgress de tanımlı componenti yok");
        }
        hasProgress.OnProgressChanged+= CuttingCounter_OnProgressChanged;
        barImage.fillAmount=0f;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount=e.progressNormalized;
        if(e.progressNormalized==0f||e.progressNormalized==1){
            Hide();
        }else{
            Show();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}
