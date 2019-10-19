using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twity;
using Twity.DataModels.Core;
using Twity.DataModels.Trends;
using System.Reflection;
using System.Text.RegularExpressions;


public class TwitterScript : MonoBehaviour
{
    private Stream stream;
    private Indico indico;
    // Start is called before the first frame update
    void Start()
    {
        indico = GetComponent<Indico>();

        Twity.Oauth.consumerKey = "QWfElQsOE4s7YpcT1q5OS0Dvi";
        Twity.Oauth.consumerSecret = "oerO9Mfr1Verbd64TydNi3LZnIE8PbWXm0iIfPJjgnKlw0HTM5";
        Twity.Oauth.accessToken = "1145564096-gcgjSgGQCDlltojG3cN2l1x4AfnkOubnzzxLjS5";
        Twity.Oauth.accessTokenSecret = "uWrEvMKGsoSVnd5GgAz2ZlkciFShVERjrCbysA5FPMjaC";
    
        stream = new Stream(StreamType.PublicFilter);
        StartTrendingTopics();
        // StartStream(tracks);
    }

    private void StartTrendingTopics()
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["id"] = "2477058";
        StartCoroutine(Twity.Client.Get("trends/place", parameters, StartTrendingCallback));
    }

    void StartTrendingCallback(bool success, string response)
    {
        if (success)
        {
            Debug.Log(response);
            Regex rgx = new Regex(@"name"":""([^""]*)"",""url");
            List<string> topics = new List<string>();
            foreach (Match match in rgx.Matches(response))
            {
                topics.Add(match.Groups[1].Value);
            }
            StartStream(topics);
        }
        else
        {
            Debug.Log(response);
        }
    }

    private void StartStream(List<string> tracks)
    {
        Dictionary<string, string> streamParameters = new Dictionary<string, string>();

        Twity.FilterTrack filterTrack = new Twity.FilterTrack(tracks);
        streamParameters.Add(filterTrack.GetKey(), filterTrack.GetValue());
        StartCoroutine(stream.On(streamParameters, OnStream));
    }


    private void OnStream(string response, StreamMessageType messageType)
    {
        try
        {
            if (messageType == StreamMessageType.Tweet)
            {
                Tweet tweet = JsonUtility.FromJson<Tweet>(response);
                indico.GetSentiment(tweet.text, (Indico.Sentiment s) => Debug.Log($"{s.result} : {tweet.text}"));
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    void OnApplicationQuit()
    {
        stream.Off();
    }
}
