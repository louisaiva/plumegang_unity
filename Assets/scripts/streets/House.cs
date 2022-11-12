
using UnityEngine;

public class House : Level
{

  protected GameObject bg;
  protected GameObject fr;


  void Start()
  {

    if (level_name == null){
      level_name = "house";
    }

    level_height = 0.785f;

    // on récupère les composants
    bg = GameObject.FindWithTag("street_skin_bg");
    fr = GameObject.FindWithTag("street_skin_fr");

    getBounds();

    // on donne la bonne taille et position aux bounds
    wall_L.transform.localScale = new Vector3(1,h+10,0);
    wall_L.transform.position = new Vector3(min_x - wall_L.transform.localScale.x/2,0,0);

    wall_R.transform.localScale = new Vector3(1,h+10,0);
    wall_R.transform.position = new Vector3(max_x + wall_R.transform.localScale.x/2,0,0);

    wall_ground.transform.localScale = new Vector3(w+10,1,0);
    wall_ground.transform.position = new Vector3(0,min_y - wall_ground.transform.localScale.y/2,0);

    float max_y = min_y + level_height + feet_perso_height;
    wall_air.transform.localScale = new Vector3(w+10,1,0);
    wall_air.transform.position = new Vector3(0,max_y + wall_air.transform.localScale.y/2,0);

    // on donne la bonne taille et position au sol
    ground.transform.localScale = new Vector3(w,level_height + feet_perso_height,0);
    ground.transform.position = new Vector3(0,min_y + ground.transform.localScale.y/2,0);
  }

  // FONCTIONS SECONDAIRES

  public override Vector2 getSize(){
    w = bg.GetComponent<Renderer>().bounds.size.x;
    h = bg.GetComponent<Renderer>().bounds.size.y;
    return new Vector2(w,h);
  }

  public override Bounds getBounds(){
    getSize();
    min_x = bg.GetComponent<Renderer>().bounds.min.x;
    max_x = bg.GetComponent<Renderer>().bounds.max.x;
    min_y = bg.GetComponent<Renderer>().bounds.min.y;
    return new Bounds(new Vector3(min_x+w/2,min_y+h/2,0),new Vector3(w,h,0));
  }
}
