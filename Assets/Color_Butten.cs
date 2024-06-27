using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Butten : MonoBehaviour
{
    public string ColorName;
    public Game_Controoler Game_Controoler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void This_Is_The_Color()
    {
        Game_Controoler.CheckColorChoice(ColorName);
    }


}
