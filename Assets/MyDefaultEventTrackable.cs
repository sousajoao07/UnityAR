using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;
public class MyDefaultEventTrackable : DefaultObserverEventHandler {

[Serializable]

public class TotalTimeLamps
    {
        public string totalTime;
    }

[Serializable]

public class GeneralInfo
    {
        public string totalTime;
        public double priceInEuros;
    }

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

    TMP_Text outputNome1, outputEstado1, outputIP1, outputNome2, outputEstado2, outputIP2,
     outputGasto, outputGastoMensal, outputHorasUso, outputLamp1Tempo, outputLamp2Tempo;

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

        //GERAL

        outputGasto = GameObject.Find("OutputGasto").GetComponent<TMP_Text>();
        outputGastoMensal = GameObject.Find("OutputGastoMensal").GetComponent<TMP_Text>();
        outputHorasUso = GameObject.Find("OutputHorasUso").GetComponent<TMP_Text>();
        outputLamp1Tempo = GameObject.Find("OutputLamp1Tempo").GetComponent<TMP_Text>();
        outputLamp2Tempo = GameObject.Find("OutputLamp2Tempo").GetComponent<TMP_Text>();

    }

    protected override void OnTrackingFound()
    {    
        base.OnTrackingFound ();
        GetDataLampada();
        GetDataGeral();
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost ();
    }

    void GetDataLampada() => StartCoroutine(GetLampada());
    void GetDataGeral() => StartCoroutine(GetDadosGerais());

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
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError){
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
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError){
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

    IEnumerator GetDadosGerais()
    {
        outputGasto.text = "Loading...";
        outputGastoMensal.text = "Loading...";
        outputHorasUso.text = "Loading...";
        outputLamp1Tempo.text = "Loading...";
        outputLamp2Tempo.text = "Loading...";

        string uriUptimes = "http://127.0.0.1:8080/api/uptimes/lastDay";
        string uriTotalUptime1= "http://127.0.0.1:8080/api/uptimes/totalUptime/1";
        string uriTotalUptime2= "http://127.0.0.1:8080/api/uptimes/totalUptime/2";

        using (UnityWebRequest request = UnityWebRequest.Get(uriUptimes))
        {
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError){
                // outputNome1.text = request.error;
                // outputEstado1.text = request.error;
                // outputIP1.text = request.error;
            }
            else
            {
                GeneralInfo uptime = JsonUtility.FromJson<GeneralInfo>(request.downloadHandler.text);
                outputGasto.text = uptime.priceInEuros.ToString()+"€";
                outputGastoMensal.text = (uptime.priceInEuros * 30).ToString()+"€";
                outputHorasUso.text = uptime.totalTime;
            }
        }

        using (UnityWebRequest request = UnityWebRequest.Get(uriTotalUptime1))
        {
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError){
                // outputNome2.text = request.error;
                // outputEstado2.text = request.error;
                // outputIP2.text = request.error;
            }
            else
            {
                TotalTimeLamps totalTimeLamp1 = JsonUtility.FromJson<TotalTimeLamps>(request.downloadHandler.text);
                outputLamp1Tempo.text = totalTimeLamp1.totalTime.ToString();
            }
        }

        using (UnityWebRequest request = UnityWebRequest.Get(uriTotalUptime2))
        {
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError){
                // outputNome1.text = request.error;
                // outputEstado1.text = request.error;
                // outputIP1.text = request.error;
            }
            else
            {
                TotalTimeLamps totalTimeLamp2 = JsonUtility.FromJson<TotalTimeLamps>(request.downloadHandler.text);
                outputLamp2Tempo.text = totalTimeLamp2.totalTime.ToString();
            }
        }
    }
}

