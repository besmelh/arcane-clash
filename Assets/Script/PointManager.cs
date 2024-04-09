using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class PointManager : MonoBehaviour
{
    public int hp;
    public TMP_Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        hpText.text = "HP: " + hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int points)
    { 
        hp += points;
        hpText.text = "HP: " + hp;
    }
}
