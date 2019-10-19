using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twity.DataModels.Responses;

public class TwitterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Twity.Oauth.consumerKey = "QWfElQsOE4s7YpcT1q5OS0Dvi";
        Twity.Oauth.consumerSecret = "oerO9Mfr1Verbd64TydNi3LZnIE8PbWXm0iIfPJjgnKlw0HTM5";
        Twity.Oauth.accessToken = "1145564096-gcgjSgGQCDlltojG3cN2l1x4AfnkOubnzzxLjS5";
        Twity.Oauth.accessTokenSecret = "uWrEvMKGsoSVnd5GgAz2ZlkciFShVERjrCbysA5FPMjaC";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["id"] = "1185314716610002944";
        StartCoroutine(Twity.Client.Get("statuses/show", parameters, Callback));
    }

    void Callback(bool success, string response)
    {
        if (success)
        {
            SearchTweetsResponse Response = JsonUtility.FromJson<SearchTweetsResponse>(response);
            Debug.Log(response);
        }
        else
        {
            Debug.Log(response);
        }
    }
}
