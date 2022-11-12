
using UnityEngine;
using UnityEngine.Tilemaps;

public class Street : Level
{

  protected float no_bg_width = 50f;

  public GameObject road_tm; // tilemap de la route
  public TileBase tile;
  public BoundsInt area;


  void Start()
  {

    if (level_name == null){
      level_name = "street";
    }

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

    createTilemap();

  }

  void Update()
  {

  }

  // FONCTIONS ONCE

  void createTilemap(){

    // on récupère la tilemap de la route
    road_tm = GameObject.Find("tilemaps").transform.Find("road").gameObject;

    // on place les tiles de la route à la bonne position
    Tilemap tm = road_tm.GetComponent<Tilemap>();

    // on clean tout
    tm.ClearAllTiles();

    // on définit la bonne area pour la route
    area = new BoundsInt();
    area.xMin = (int)min_x;
    area.xMax = (int)max_x;
    area.yMin = (int)min_y;
    area.yMax = (int)min_y + 1;

    // on récupère la tile de tiles/road
    tile = Resources.Load("tiles/road") as TileBase;

    // on place les tiles au bon endroit
    for (int x = area.xMin; x < area.xMax; x++)
    {
        tm.SetTile(new Vector3Int(x ,(int) min_y, 0), tile);
    }
    //tm.RefreshAllTiles();

    Debug.Log("road tilemap loaded");

  }

  // FONCTIONS SECONDAIRES

  public override Vector2 getSize(){
    w = no_bg_width;
    h = level_height + feet_perso_height;
    return new Vector2(w,h);
  }

  public override Bounds getBounds(){
    getSize();
    min_x = -no_bg_width/2;
    max_x = no_bg_width/2;
    min_y = -2;
    return new Bounds(new Vector3(min_x+w/2,min_y+h/2,0),new Vector3(w,h,0));
  }

}
