using Vuforia;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

namespace Vuforia.Assets.Prefabs{
public class GetTemp : MonoBehaviour
{
    [Serializable]
    public class Temp
    {
        public int id;
        public string temperature;
    }

    public TMP_Text output;

    public void Start()
    {
        output = GameObject.Find("Output").GetComponent<TMP_Text>();
        GetData();
    }

    public void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
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
                var x = JsonUtility.FromJson<Temp>(request.downloadHandler.text);
                output.text = x.temperature;
            }
        }
    }
}
}