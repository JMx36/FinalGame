using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    GameObject life_1;

    [SerializeField]
    GameObject life_2;

    [SerializeField]
    GameObject life_3;

    // Start is called before the first frame update
    void Start()
    {
       /* GameStateManager.OnLoseLife += OnLoseLife;
        GameStateManager.OnGameOver += OnGameOver;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLoseLife()
    {
        Debug.Log("Lost a life");
    }

    private void OnGameOver()
    {
        Debug.Log("Game over");
    }
}
