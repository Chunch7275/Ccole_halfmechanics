using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    private GameManager controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        controller.gameOver = true;
    }
}
