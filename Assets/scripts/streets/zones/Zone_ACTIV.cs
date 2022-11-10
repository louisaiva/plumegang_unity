using UnityEngine;
using TMPro;

public class Zone_ACTIV : Zone_HOOV
{

  private float delay_label = 0.4f;
  public string activating_position = "back";

  private bool is_active = false;

  //// la activating_position régule la position de la zone PAR RAPPORT au perso afin de l'activer correctement
  //   3 positions : 'back','front','mid' :
  // si 'back', le perso doit monter (Z) pour activer la zone
  // si 'front', le perso doit descendre (S) pour activer la zone
  // si 'mid', inactivable (prendre avec E)

  public virtual void activate(){
    if (!is_active){
      Debug.Log(zone_name + " activated");

      is_active = true;
      // colore le label pour 5sec
      label.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 25, 255);
      Invoke("desactivate",delay_label);
      }
    }

  private void desactivate(){
    label.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255, 255);
    is_active = false;
    }

}
