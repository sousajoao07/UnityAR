using UnityEngine;
using Vuforia;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System;

public class BotaoLamp1 : MonoBehaviour
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
    public VirtualButtonBehaviour VirtualButtonLamp1;

    public bool state = true;
    TMP_Text outputEstado1, outputNome1, outputIP1;
    void Start()
    {
        VirtualButtonLamp1.RegisterOnButtonReleased(Toggle);
        outputEstado1 = GameObject.Find("OutputEstado1").GetComponent<TMP_Text>();
        
    }
    IEnumerator Request(){

        string uri1 = "http://127.0.0.1:8080/api/lamp/1/toggle";
        string uri2 = "http://127.0.0.1:8080/api/lamp/1";

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
                outputNome1.text = request.error;
                outputEstado1.text = request.error;
                outputIP1.text = request.error;
            }
            else
            {
                Data lampada1 = JsonUtility.FromJson<Data>(request.downloadHandler.text);
                outputEstado1.text = lampada1.data.state.Equals(true) ? "Ligada" : "Desligada";
            }
        }
        
    }
 
    public void Toggle(VirtualButtonBehaviour vb)
    {
        StartCoroutine(Request());
    }
}
