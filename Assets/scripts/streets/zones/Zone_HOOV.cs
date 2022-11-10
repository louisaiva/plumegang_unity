using UnityEngine;
using TMPro;

public class Zone_HOOV : MonoBehaviour
{

  //public GameObject sman;
  public GameObject label;

  public string zone_name;

  protected virtual void Start()
  {
    label.GetComponent<TextMeshProUGUI>().text = zone_name;
    label.GetComponent<TextMeshProUGUI>().enabled = false;

  }

  private void OnTriggerEnter2D(Collider2D collision){
    if (collision.CompareTag("Player")){
      label.GetComponent<TextMeshProUGUI>().enabled = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision){
    if (collision.CompareTag("Player")){

      // on vérifie qu'il ne reste pas un collider du joueur dans le collider de la zone
      LayerMask mask = LayerMask.GetMask("Player");
      if (!GetComponent<Collider2D>().IsTouchingLayers(mask)){
        label.GetComponent<TextMeshProUGUI>().enabled = false;
      }
    }
  }
}
