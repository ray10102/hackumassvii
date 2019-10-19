using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System;

public class Indico : MonoBehaviour
{
    void Start()
    {
        GetEmotion("Hello world, it's a beautiful day to be a liberal anime girl!", (Emotion p) => Debug.Log(p.surprise));
    }

    public Sentiment GetSentiment(string data, Action<Sentiment> onComplete) {
        Sentiment sentiment = new Sentiment(data, onComplete);
        StartCoroutine(sentiment.Upload());
        return sentiment;
    }

    public PoliticalAnalysis GetPoliticalAnalysis(string data, Action<PoliticalAnalysis> onComplete) {
        PoliticalAnalysis political = new PoliticalAnalysis(data, onComplete);
        StartCoroutine(political.Upload());
        return political;
    }

    public Emotion GetEmotion(string data, Action<Emotion> onComplete) {
        Emotion emotion = new Emotion(data, onComplete);
        StartCoroutine(emotion.Upload());
        return emotion;   
    }

    public abstract class BaseCall {
        private string url;
        
        protected BaseCall(string url) {
            this.url = url;
        }
        protected virtual WWWForm GetForm() {
            WWWForm form = new WWWForm();
            form.AddField("api_key", "97fe7e59454c38d23b284a181bca567d");
            return form;
        }

        protected abstract void ProcessData(byte[] byteResponse);

        public IEnumerator Upload()
        {
            WWWForm form = GetForm();

            using (UnityWebRequest www = UnityWebRequest.Post(this.url, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    ProcessData(www.downloadHandler.data);
                }
            }
        }
    }

    public class Sentiment : BaseCall {
        private Action<Sentiment> onComplete;
        public double result;
        private string data;

        public Sentiment(string data, Action<Sentiment> onComplete) : base("https://apiv2.indico.io/sentiment") {
            this.data = data;
            this.onComplete = onComplete;
        }
        
        protected override WWWForm GetForm() {
            WWWForm form = base.GetForm();
            form.AddField("data", this.data);
            return form;
        }

        protected override void ProcessData(byte[] byteResponse)
        {
            string jsonStr = Encoding.UTF8.GetString(byteResponse);
            Dictionary<string, double> dict = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonStr);
            if (dict.TryGetValue("results", out this.result)) {
                onComplete?.Invoke(this);
            }
            else
            {
                Debug.Log("Failed to get results from Sentiment Analysis");
            }
        }

    }

    public class PoliticalAnalysis : BaseCall {
        public double liberal;
        public double conservative;
        public double libertarian;
        public double green;

        private string data;
        private Action<PoliticalAnalysis> onComplete;
        public PoliticalAnalysis(string data, Action<PoliticalAnalysis> onComplete) : base("https://apiv2.indico.io/political?version=2") {
            this.data = data;
            this.onComplete = onComplete;
        }

        protected override WWWForm GetForm() {
            WWWForm form = base.GetForm();
            form.AddField("data", this.data);
            return form;
        }
        
        protected override void ProcessData(byte[] byteResponse)
        {
            string jsonStr = Encoding.UTF8.GetString(byteResponse);
            Dictionary<string, Dictionary<string, double>> dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(jsonStr);
            Dictionary<string, double> result;
            if (dict.TryGetValue("results", out result))
            {
                if (result.TryGetValue("Liberal", out this.liberal)) {}
                if (result.TryGetValue("Conservative", out this.conservative)) {}
                if (result.TryGetValue("Libertarian", out this.libertarian)) {}
                if (result.TryGetValue("Green", out this.green)) {}
                onComplete?.Invoke(this);
            }
            else
            {
                Debug.LogError("Failed to get results from Political Analysis");
            }
        }
    }

    public class Emotion : BaseCall {
        public double anger;
        public double fear;
        public double sadness;
        public double joy;
        public double surprise;


        private string data;
        private Action<Emotion> onComplete;
        public Emotion(string data, Action<Emotion> onComplete) : base("https://apiv2.indico.io/emotion") {
            this.data = data;
            this.onComplete = onComplete;
        }

        protected override WWWForm GetForm() {
            WWWForm form = base.GetForm();
            form.AddField("data", this.data);
            return form;
        }
        
        protected override void ProcessData(byte[] byteResponse)
        {
            string jsonStr = Encoding.UTF8.GetString(byteResponse);
            Dictionary<string, Dictionary<string, double>> dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(jsonStr);
            Dictionary<string, double> result;
            if (dict.TryGetValue("results", out result))
            {
                if (result.TryGetValue("anger", out this.anger)) {}
                if (result.TryGetValue("fear", out this.fear)) {}
                if (result.TryGetValue("sadness", out this.sadness)) {}
                if (result.TryGetValue("joy", out this.joy)) {}
                if (result.TryGetValue("surprise", out this.surprise)) {}
                onComplete?.Invoke(this);
            }
            else
            {
                Debug.LogError("Failed to get results from Emotion Analysis");
            }
        }
    }
}
