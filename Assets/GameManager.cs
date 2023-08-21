using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int scoreDepth = 0;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
    }

    public void SetDepth(int depth)
    {
        scoreDepth = depth;
    }
}
