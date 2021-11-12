using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using System;
using TMPro;
public class MyDefaultEventTrackable : DefaultObserverEventHandler {

    [Serializable]
    public class Temperature
    {
        public int id;
        public string temperature;
    }

    TMP_Text output;

    protected override void Start()
    {   
        base.Start();
        output = GameObject.Find("Output").GetComponent<TMP_Text>();
    }

    protected override void OnTrackingFound()
    {    
        base.OnTrackingFound ();
        GetData();
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost ();
    }

    void GetData() => StartCoroutine(GetTemperature());

    IEnumerator GetTemperature()
    {
        output.text = "Loading...";
        string uri = "https://618c145cded7fb0017bb93e6.mockapi.io/api/ar/temperatures";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError){
                output.text = request.error;
            }
            else
            {
                var x = JsonUtility.FromJson<Temperature>(request.downloadHandler.text);
                output.text = x.temperature;
            }
        }
    }
    //TODO: GetLight -> light/{id}

    //Ação Botoes
    //TODO: PutLightState -> ligth/{id} body --> lightOn: true or false
}

