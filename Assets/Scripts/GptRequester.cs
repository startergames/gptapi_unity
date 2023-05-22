using System.Collections;
using System.Collections.Generic;
using unity_gpt_api.Runtime;
using UnityEngine;

public class GptRequester : MonoBehaviour
{
    private GptApi api;

    // Start is called before the first frame update
    void Start()
    {
        api = new GptApi();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExecuteGptQuery("Translate the following English text to French: 'Hello, how are you?'"));
        }
    }

    private IEnumerator ExecuteGptQuery(string prompt)
    {
        var task = api.GetCompletionAsync(prompt);
        yield return new WaitUntil(() => task.IsCompleted);
        var result = task.Result;
        Debug.Log(result);
    }
}
