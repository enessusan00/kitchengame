using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
 [SerializeField] private StoveCounter stoveCounter;
 [SerializeField] private GameObject stoneOnGameObject;
 [SerializeField] private GameObject particlesGameObject;

void Start()
{
    stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
}

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
       bool showVisual = e.state == StoveCounter.State.Frying||e.state == StoveCounter.State.Fried;
       stoneOnGameObject.SetActive(showVisual);
       particlesGameObject.SetActive(showVisual);
    }
}
