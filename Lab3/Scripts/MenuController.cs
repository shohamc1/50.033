using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == "resetButton")
            {   
                eachChild.gameObject.SetActive(false);
            }
        }
    }

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void Reset()
    {
        // reset positions
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;

        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
