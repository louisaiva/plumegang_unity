
using UnityEngine;

public class Street : MonoBehaviour
{

  public string street_name;
  public bool is_active;

  public GameObject bg = null;
  public GameObject fr = null;

  // children
  public GameObject wall_ground;
  public GameObject wall_air;
  public GameObject wall_L;
  public GameObject wall_R;
  public GameObject ground;

  public float max_width;
  public float min_y;

  public float street_height;
  public float feet_perso_height = 0.06f;


  void Start()
  {

    // on vérifie si on a un bg et un fr
    if (GameObject.FindWithTag("street_skin_bg") != null){
      bg = GameObject.FindWithTag("street_skin_bg");
    }
    if (GameObject.FindWithTag("street_skin_fr") != null){
      fr = GameObject.FindWithTag("street_skin_fr");
    }

    if (bg){

      // on donne la bonne taille et position aux bounds

      float w = bg.GetComponent<Renderer>().bounds.size.x;
      float h = bg.GetComponent<Renderer>().bounds.size.y;

      float min_x = bg.GetComponent<Renderer>().bounds.min.x;
      wall_L.transform.localScale = new Vector3(1,h+10,0);
      wall_L.transform.position = new Vector3(min_x - wall_L.transform.localScale.x/2,0,0);

      float max_x = bg.GetComponent<Renderer>().bounds.max.x;
      wall_R.transform.localScale = new Vector3(1,h+10,0);
      wall_R.transform.position = new Vector3(max_x + wall_R.transform.localScale.x/2,0,0);

      float min_y = bg.GetComponent<Renderer>().bounds.min.y;
      wall_ground.transform.localScale = new Vector3(w+10,1,0);
      wall_ground.transform.position = new Vector3(0,min_y - wall_ground.transform.localScale.y/2,0);

      float max_y = min_y + street_height + feet_perso_height;
      wall_air.transform.localScale = new Vector3(w+10,1,0);
      wall_air.transform.position = new Vector3(0,max_y + wall_air.transform.localScale.y/2,0);

      // on donne la bonne taille et position au sol
      ground.transform.localScale = new Vector3(w,street_height + feet_perso_height,0);
      ground.transform.position = new Vector3(0,min_y + ground.transform.localScale.y/2,0);
    }
    else{
      max_width = 300f;

      // on donne la bonne taille et position aux bounds

      float w = max_width;
      float h = street_height + feet_perso_height;

      float min_x = -w/2;
      wall_L.transform.localScale = new Vector3(1,h+10,0);
      wall_L.transform.position = new Vector3(min_x - wall_L.transform.localScale.x/2,0,0);

      float max_x = w/2;
      wall_R.transform.localScale = new Vector3(1,h+10,0);
      wall_R.transform.position = new Vector3(max_x + wall_R.transform.localScale.x/2,0,0);

      float min_y = -2;
      wall_ground.transform.localScale = new Vector3(w+10,1,0);
      wall_ground.transform.position = new Vector3(0,min_y - wall_ground.transform.localScale.y/2,0);

      float max_y = min_y + street_height + feet_perso_height;
      wall_air.transform.localScale = new Vector3(w+10,1,0);
      wall_air.transform.position = new Vector3(0,max_y + wall_air.transform.localScale.y/2,0);

      // on donne la bonne taille et position au sol
      ground.transform.localScale = new Vector3(w,street_height + feet_perso_height,0);
      ground.transform.position = new Vector3(0,min_y + ground.transform.localScale.y/2,0);
    }

    Debug.Log("Street "+street_name+" loaded");
  }


  void Update()
  {

  }


  // FONCTIONS SECONDAIRES

  public float getWidth(){
    if (bg){
      return bg.GetComponent<Renderer>().bounds.size.x;
    }
    else{
      return max_width;
    }
  }

  public Vector3 getBounds(){
    if (bg){
      float min_x = bg.GetComponent<Renderer>().bounds.min.x;
      float max_x = bg.GetComponent<Renderer>().bounds.max.x;
      min_y = bg.GetComponent<Renderer>().bounds.min.y;
      float max_y = min_y + street_height + feet_perso_height;
      return new Vector3(min_x,max_x,min_y);
    }
    else{
      return new Vector3(-max_width/2,max_width/2,-2);
    }
  }

}
