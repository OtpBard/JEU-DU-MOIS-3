using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    public static LoadingScreen Instance;
    public RawImage sprite;
    private bool IsLoading = false;
    public float Timer;
    private float TotalTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLoading)
        {
            if (Timer <= 0.0f)
            {
                IsLoading = false;
                gameObject.SetActive(false);
            }

            float x = TotalTime - Timer;
            x = x - TotalTime / 2.0f;
            float y = Mathf.Abs(x) - TotalTime / 2.0f;
            Color newColor = sprite.color;
            newColor.a = Mathf.Abs(y) / TotalTime * 2.0f;
            sprite.color = newColor;

            Timer -= Time.deltaTime;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        gameObject.SetActive(false);
    }

    public void ShowLoadingScreen(float Time)
    {
        gameObject.SetActive(true);
        Timer = Time;
        IsLoading = true;
        TotalTime = Time;
    }
}
