
using UnityEngine;

public class Street : Level
{

  protected float no_bg_width = 50f;

  void Start()
  {

    if (level_name == null){
      level_name = "street";
    }

    getSize();
    getBounds();

    // on donne la bonne taille et position aux bounds
    float w = no_bg_width;
    float h = level_height + feet_perso_height;

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
    w = no_bg_width;
    h = level_height + feet_perso_height;
    return new Vector2(w,h);
  }

  public override Vector3 getBounds(){
    min_x = -no_bg_width/2;
    max_x = no_bg_width/2;
    min_y = -2;
    return new Vector3(min_x,max_x,min_y);
  }

}
