
using UnityEngine;

public class Level : MonoBehaviour
{

  public string level_name;
  //public bool is_active;

  // children
  public GameObject wall_ground;
  public GameObject wall_air;
  public GameObject wall_L;
  public GameObject wall_R;
  public GameObject ground;

  // dimensions et positions
  protected float min_x;
  protected float max_x;
  protected float min_y;
  protected float w;
  protected float h;

  // dimensions du ground
  protected float level_height = 0.94f;
  protected float feet_perso_height = 0.06f;


  void Start()
  {

    // on pose les dimensions et les positions des murs
    w = 300f;
    h = level_height + feet_perso_height;

    min_x = -w/2;
    max_x = w/2;
    min_y = -2;

    Debug.Log("Street "+level_name+" loaded");
  }

  // FONCTIONS SECONDAIRES

  public virtual Vector2 getSize(){
    return new Vector2(w,h);
  }

  public virtual Bounds getBounds(){
    return new Bounds(new Vector3(min_x+w/2,min_y+h/2,0),new Vector3(w,h,0));
  }

}
