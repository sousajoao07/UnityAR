using UnityEngine;
using Vuforia;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class Botoes : MonoBehaviour
{
    public VirtualButtonBehaviour VirtualButtonLigar;

    public bool state = true;
    TMP_Text outputEstado3;
    void Start()
    {
        VirtualButtonLigar.RegisterOnButtonReleased(Toggle);
        outputEstado3 = GameObject.Find("OutputEstado3").GetComponent<TMP_Text>();
        
    }
    IEnumerator Request(){

        string uri = "http://127.0.0.1:8080/api/lamp/toggleAll";

        using (UnityWebRequest request = UnityWebRequest.Post(uri,""))
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
        
    }
 
    public void Toggle(VirtualButtonBehaviour vb)
    {
        StartCoroutine(Request());
    }
}
