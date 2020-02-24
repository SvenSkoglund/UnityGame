using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{


    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Clicked");

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, clickPosition);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.name == "CreateGameButton")
                {
                    Debug.Log("Hit Create Game button");
                    SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
                }
            }
        }
    }


}