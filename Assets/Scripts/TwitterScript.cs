using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twity;
using Twity.DataModels.Core;
using Twity.DataModels.Trends;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.Networking;

public class TwitterScript : MonoBehaviour
{
    private Stream stream;
    private Indico indico;
    public AviManager aviManager;
    public SpriteRenderer sprite;
    private WaveSources waveSources;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        indico = GetComponent<Indico>();
        waveSources = GetComponent<WaveSources>();

        Twity.Oauth.consumerKey = "QWfElQsOE4s7YpcT1q5OS0Dvi";
        Twity.Oauth.consumerSecret = "oerO9Mfr1Verbd64TydNi3LZnIE8PbWXm0iIfPJjgnKlw0HTM5";
        Twity.Oauth.accessToken = "1145564096-gcgjSgGQCDlltojG3cN2l1x4AfnkOubnzzxLjS5";
        Twity.Oauth.accessTokenSecret = "uWrEvMKGsoSVnd5GgAz2ZlkciFShVERjrCbysA5FPMjaC";
    
        stream = new Stream(StreamType.PublicFilter);
        // StartTrendingTopics();
        List<string> tracks = new List<string>(){"happy", "blessed", "excited"};
        StartStream(tracks);
        List<string> newTracks = new List<string>(){"dumb", "horrible", "idiot"};
        UpdateStream(newTracks);
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

    private void UpdateStream(List<string> tracks)
    {
        stream.Off();
        StartStream(tracks);
    }

    private string sent2Spec(double sentiment)
    // What is the species that corresponds to this sentiment value?
    {
        string[] orderedSpecies = {
            "little-egret",  // grandma from hell
            "jocotoco-antpitta",  // goofy owl boi
            "eurasian-nuthatch",  // chill but more of a wail / cry
            "common-blackbird", // pretty and loud
            "red-crossbill", // sterotypical tweeter
            "eurasian-wren"  // carefree, long call
        };
        return orderedSpecies[(int) (sentiment * orderedSpecies.Length)];
    }

    private float followers2Volume(int followers_count)
    {
        return (float) Math.Log((double) followers_count, 1000);
    }

    private AudioClip randomChirp(string species)
    {
        AudioClip[] audioClips = Resources.LoadAll($"{species}/samples", typeof(AudioClip)).Cast<AudioClip>().ToArray();
        return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    }

    IEnumerator WithImage(string MediaUrl, Action<Texture2D> callback)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(MediaUrl))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                callback(texture);
            }
        }
    }

    private void OnStream(string response, StreamMessageType messageType)
    {
        try
        {
            if (messageType == StreamMessageType.Tweet)
            {
                Tweet tweet = JsonUtility.FromJson<Tweet>(response);
                indico.GetSentiment(tweet.text, (Indico.Sentiment s) => {
                    float f2v = followers2Volume(tweet.user.followers_count);
                    Vector2 xz = new Vector2(UnityEngine.Random.Range(-60f, 60f), UnityEngine.Random.Range(-60f, 60f));
                    AudioSource.PlayClipAtPoint(randomChirp(sent2Spec(s.result)), new Vector3(xz.x, 0, xz.y), f2v);
                    waveSources.CreateWave(f2v * 10f, followers2Volume(tweet.user.followers_count)* 3f, .25f, xz.x, xz.y);
                    Debug.Log($"{tweet.user.followers_count} : {tweet.text}");
                    StartCoroutine(WithImage(tweet.user.profile_image_url, texture => {
                        aviManager.Create(xz, texture, Mathf.Lerp(2.5f, 5f, f2v));
                        Rect rec = new Rect(0, 0, 48, 48);
                    }));
                });
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
