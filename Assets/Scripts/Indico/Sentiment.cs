using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class Sentiment : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("api_key", "97fe7e59454c38d23b284a181bca567d");
        form.AddField("data", "Today is a excellente day");

        using (UnityWebRequest www = UnityWebRequest.Post("https://apiv2.indico.io/sentiment", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonStr = Encoding.UTF8.GetString(www.downloadHandler.data);
                Dictionary<string, double> dict = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonStr);
                double result;
                if (dict.TryGetValue("results", out result)) {
                    Debug.Log(result);
                } else {
                    Debug.Log("nope");
                }
            }
        }
    }
}
