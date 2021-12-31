using UnityEngine;
using Vuforia;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System;

public class BotaoLamp2 : MonoBehaviour
{
  
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
    public VirtualButtonBehaviour VirtualButtonLamp2;

    public bool state = true;
    TMP_Text outputEstado2, outputNome2, outputIP2;
    void Start()
    {
        VirtualButtonLamp2.RegisterOnButtonReleased(Toggle);
        outputEstado2 = GameObject.Find("OutputEstado2").GetComponent<TMP_Text>();
        
    }
    IEnumerator Request(){

        string uri1 = "http://127.0.0.1:8080/api/lamp/2/toggle";
        string uri2 = "http://127.0.0.1:8080/api/lamp/2";

        using (UnityWebRequest request = UnityWebRequest.Post(uri1,""))
        {
            request.SetRequestHeader("Content-Lenght", "0");
            request.SetRequestHeader("Content-Type", "application/json");
            request.method = "POST";
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Done!");
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
                outputEstado2.text = lampada2.data.state.Equals(true) ? "Ligada" : "Desligada";
            }
        }
        
    }
 
    public void Toggle(VirtualButtonBehaviour vb)
    {
        StartCoroutine(Request());
    }
}
