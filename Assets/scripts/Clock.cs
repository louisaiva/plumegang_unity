using UnityEngine;
using TMPro;
using System;


public class Clock : MonoBehaviour
{

  public GameObject days_lab,hm_lab;

  public float tpd; // tick per day
  private int days,ticks,hours,minutes,seconds;

  void Start()
  {

  }

  void FixedUpdate()
  {
    ticks++;

    // update all variables
    float current_time = ticks/tpd;
    days = (int) current_time;

    hours = (int) (current_time*24f - ((float) 24*days));
    //Debug.Log(hours);
    minutes = (int) (current_time*24f*60f)%60;
    seconds = (int) (current_time*24f*60f*60f)%60;

    // update clock labels
    days_lab.GetComponent<TextMeshProUGUI>().text = "Day "+days.ToString();
    hm_lab.GetComponent<TextMeshProUGUI>().text = hm();
  }

  string hm(){

    string h = hours.ToString();
    string m = minutes.ToString();

    if (h.Length < 2){
      h = "0" + h;
    }

    if (m.Length < 2){
      m = "0" + m;
    }
    return h + " : " + m;
  }

}
