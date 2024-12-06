using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCollider : MonoBehaviour
{
    private GameManager controller;
    public string levelName;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        controller.gameOver = true;
        SceneManager.LoadScene(levelName);

    }
}
