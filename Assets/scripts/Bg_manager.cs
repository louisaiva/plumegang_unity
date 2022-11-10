using UnityEngine;
using System.Collections;

public class Bg_manager : MonoBehaviour
{


  public GameObject cam; // the Camera

  // children
  public GameObject bg_builds;
  public GameObject bg_builds2;
  public GameObject fr_builds;
  public GameObject fr_builds2;


  private float width_bg_builds;
  private float width_fr_builds;
  private float X_BG_OFT,X_FR_OFT;

  private float starting_x;

  void Start(){
    starting_x = transform.position.x;

    width_bg_builds = bg_builds.GetComponent<Renderer>().bounds.size.x;
    width_fr_builds = fr_builds.GetComponent<Renderer>().bounds.size.x;
    X_BG_OFT = 0;
    X_FR_OFT = 0;


  }

  // Update is called once per frame
  void Update(){

    // on bouge la position globale (et donc de tous les enfants) pour suivre la cam
    transform.position = new Vector3(starting_x + cam.transform.position.x,transform.position.y,0);

    // on bouge bien les batiments en bg et on applique le paralaxe
    bg_builds.transform.position = new Vector3(starting_x + X_BG_OFT*width_bg_builds + cam.transform.position.x*0.5f,bg_builds.transform.position.y,0);
    fr_builds.transform.position = new Vector3(starting_x + X_FR_OFT*width_fr_builds + cam.transform.position.x*0.25f,bg_builds.transform.position.y,0);

    // de même avec le 2e sprite qui va se repositioner pour simuler un monde infini
    bg_builds2.transform.position = new Vector3(starting_x + (X_BG_OFT+1)*width_bg_builds + cam.transform.position.x*0.5f,bg_builds.transform.position.y,0);
    fr_builds2.transform.position = new Vector3(starting_x + (X_FR_OFT+1)*width_fr_builds + cam.transform.position.x*0.25f,bg_builds.transform.position.y,0);

    // on vérifie si les buildings du bg ne sortent pas de l'écran
    if( cam.GetComponent<Camera>().WorldToViewportPoint(bg_builds.GetComponent<Renderer>().bounds.min).x > 0f){
      X_BG_OFT -= 1;
    }
    else if ( cam.GetComponent<Camera>().WorldToViewportPoint(bg_builds2.GetComponent<Renderer>().bounds.max).x <= 1f){
      X_BG_OFT += 1;
    }
    //Debug.Log(cam.GetComponent<Camera>().WorldToViewportPoint(bg_builds2.GetComponent<Renderer>().bounds.max).x);

    // de meme avec les buildings un peu plus en front
    if( cam.GetComponent<Camera>().WorldToViewportPoint(fr_builds.GetComponent<Renderer>().bounds.min).x > 0f){
      X_FR_OFT -= 1;
    }
    else if ( cam.GetComponent<Camera>().WorldToViewportPoint(fr_builds2.GetComponent<Renderer>().bounds.max).x <= 1f){
      X_FR_OFT += 1;
    }

  }
}
