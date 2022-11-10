
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

    if (bg != null){

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
      Debug.Log("Street_manager: no bg found");
    }
  }


  void Update()
  {

  }


  // FONCTIONS SECONDAIRES

  public float getWidth(){
    return bg.GetComponent<Renderer>().bounds.size.x;
  }

  public Vector3 getBounds(){
    Vector3 bounds = new Vector3(bg.GetComponent<Renderer>().bounds.min.x,bg.GetComponent<Renderer>().bounds.max.x,bg.GetComponent<Renderer>().bounds.min.y);
    return bounds;
  }

}
