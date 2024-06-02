using UnityEngine;
using System;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assets.Scripts
{
    public class TextWriter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _pathText;

        private void Awake()
        {
            StartCoroutine(RequestText());
        }

        public IEnumerator RequestText()
        {
            string url = Application.dataPath + "/test.php";

            Dictionary<string, string> body = new Dictionary<string, string>
            {
                { "state", "opened" },
                { "time", $"{DateTime.UtcNow}" }
            };

            string postData = JsonConvert.SerializeObject(body);
                
            UnityWebRequest uwr = UnityWebRequest.Post(url, postData, "application/json");

            _pathText.text = $"Url: {url}";

            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                _pathText.text += $"\n\n Exception: {uwr.error}";
            }
            else
            {
                _pathText.text += $"\nSaved Data";
            }
        }
    }
}