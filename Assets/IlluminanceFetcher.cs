using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class IlluminanceFetcher : MonoBehaviour
{
    [System.Serializable]
    public class Result
    {
        public string timeStamp;
        public string resourceId;
        public string value;
        public string subjectId;
    }

    [System.Serializable]
    public class IlluminanceResponse
    {
        public int code;
        public string message;
        public string msgDetails;
        public string requestId;
        public List<Result> result;
    }

    private string url = "http://localhost:8080";
    public float timeInterval = 5f; // Interval to fetch the data (in seconds)
    public float illuminance; // to store illuminance value
    public TextMeshProUGUI illuminanceText;
    private bool isTextVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("StartFetch", 0f, timeInterval);
        illuminanceText.enabled = isTextVisible;
    }

    private void Update()
    {
        Vector3 myPos = transform.position;
        
    }
    public void ToggleIlluminanceText()
    {
        isTextVisible = !isTextVisible;
        illuminanceText.enabled = isTextVisible; // change the visibility of the text

        if (isTextVisible)
        {
            // If the text is visible, fetch the data
            StartFetch();
        }
    }

    public void StartFetch()
    {
        StartCoroutine(GetIlluminanceData());
    }

    IEnumerator GetIlluminanceData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                IlluminanceResponse response = JsonUtility.FromJson<IlluminanceResponse>(request.downloadHandler.text);
                if (response.result.Count > 0)
                {
                    illuminance = float.Parse(response.result[0].value);

                    // Only change the text if it's visible
                    if (isTextVisible)
                    {
                        illuminanceText.text = "Illuminance: " + illuminance;
                    }
                }
            }
        }
    }
}
