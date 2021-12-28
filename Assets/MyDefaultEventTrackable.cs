using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;
public class MyDefaultEventTrackable : DefaultObserverEventHandler {

[Serializable]
public class Lampada
    {
        public int id;
        public object bulb_id;
        public string ip;
        public string name;
        public bool state;
        public string mac_address;
        public object creared_at;
        public object updated_at;
    }

[Serializable]
    public class Data
    {
        public Lampada data;
    }

    TMP_Text outputNome1, outputEstado1, outputIP1, outputNome2, outputEstado2, outputIP2;

    protected override void Start()
    {   
        base.Start();

        //LAMP1
        outputNome1 = GameObject.Find("OutputNome1").GetComponent<TMP_Text>();
        outputEstado1 = GameObject.Find("OutputEstado1").GetComponent<TMP_Text>();
        outputIP1 = GameObject.Find("OutputIP1").GetComponent<TMP_Text>();

        //LAMP2
        outputNome2 = GameObject.Find("OutputNome2").GetComponent<TMP_Text>();
        outputEstado2 = GameObject.Find("OutputEstado2").GetComponent<TMP_Text>();
        outputIP2 = GameObject.Find("OutputIP2").GetComponent<TMP_Text>();
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

    void GetData() => StartCoroutine(GetLampada());

    IEnumerator GetLampada()
    {
        
        //LAMP1
        outputNome1.text = "Loading...";
        outputEstado1.text = "Loading...";
        outputIP1.text = "Loading...";

        //LAMP2
        outputNome2.text = "Loading...";
        outputEstado2.text = "Loading...";
        outputIP2.text = "Loading...";

        string uri1 = "http://127.0.0.1:8080/api/lamp/1";
        string uri2 = "http://127.0.0.1:8080/api/lamp/2";

        using (UnityWebRequest request = UnityWebRequest.Get(uri1))
        {
            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError){
                outputNome1.text = request.error;
                outputEstado1.text = request.error;
                outputIP1.text = request.error;
            }
            else
            {
                Data lampada1 = JsonUtility.FromJson<Data>(request.downloadHandler.text);
                outputNome1.text = lampada1.data.name;
                outputEstado1.text = lampada1.data.state.Equals(true) ? "Ligada" : "Desligada";
                outputIP1.text = lampada1.data.ip;
            }
        }

        using (UnityWebRequest request = UnityWebRequest.Get(uri2))
        {
            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError){
                outputNome2.text = request.error;
                outputEstado2.text = request.error;
                outputIP2.text = request.error;
            }
            else
            {
                Data lampada2 = JsonUtility.FromJson<Data>(request.downloadHandler.text);
                outputNome2.text = lampada2.data.name;
                outputEstado2.text = lampada2.data.state.Equals(true) ? "Ligada" : "Desligada";
                outputIP2.text = lampada2.data.ip;
            }
        }
    }
}

