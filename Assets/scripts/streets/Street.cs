
using UnityEngine;
using UnityEngine.Tilemaps;

public class Street : Level
{

  protected float no_bg_width = 50f;
  public int[] builds;
  public Vector2 wh_px;// dimensions en pixels d'un build
  public Vector2 wh; // dimensions en unités d'un build

  
  void Start()
  {

    if (level_name == null){
      level_name = "street";
    }

    // on pose les dimensions d'un build
    wh_px = new Vector2(160,130);
    wh = new Vector2(wh_px.x/32,wh_px.y/32);

    // on créé le tableau de builds
    builds = new int[] {2,3,4,5,6,4,3,2,4,3,2,4,2,4,3,4};

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

    createRoad();

    createBuilds();

  }

  // FONCTIONS ONCE

  void createRoad(){

    // on récupère la tilemap de la route
    GameObject road_tm = GameObject.Find("tilemaps").transform.Find("road").gameObject;
    Tilemap tm = road_tm.GetComponent<Tilemap>();

    // on clean tout
    tm.ClearAllTiles();

    // on définit la bonne area pour la route
    BoundsInt road_area = new BoundsInt();
    road_area.xMin = (int)min_x;
    road_area.xMax = (int)max_x;
    road_area.yMin = (int)min_y;
    road_area.yMax = (int)min_y + 1;

    // on récupère la tile de tiles/road
    TileBase road_tile = Resources.Load("tiles/road") as TileBase;

    // on place les tiles au bon endroit
    for (int x = road_area.xMin; x < road_area.xMax; x++)
    {
        tm.SetTile(new Vector3Int(x ,(int) min_y, 0), road_tile);
    }
    //tm.RefreshAllTiles();

    Debug.Log("road tilemap loaded");

  }

  void createBuilds(){

    // on récupère le component tilemap
    GameObject builds_tm = GameObject.Find("tilemaps").transform.Find("builds").gameObject;
    Tilemap tm = builds_tm.GetComponent<Tilemap>();

    // on règle les anchors de la tilemap
    tm.tileAnchor = new Vector3(wh.x,wh.y/2,0);

    // on clean tout
    tm.ClearAllTiles();

    // on définit la bonne area pour placer les builds
    BoundsInt area = new BoundsInt();
    area.xMin = (int)min_x;
    area.xMax = (int)max_x;
    area.yMin = (int)min_y + 1;


    int i=0;
    // on place les tiles
    for (float x = area.xMin; x < area.xMax; x+=wh.x)
    {
      // on récupère la tile de tiles/builds
      TileBase tile = Resources.Load("tiles/builds_"+builds[i].ToString()) as TileBase;

      tm.SetTile(new Vector3Int((int) x ,area.yMin, 0), tile);
      i++;
    }

    Debug.Log("builds tilemap loaded");
  }


  // FONCTIONS SECONDAIRES

  public override Vector2 getSize(){
    if (builds != null){
      w = builds.Length * wh.x;
      h = level_height + feet_perso_height;
    }
    else{
      w = no_bg_width;
      h = level_height + feet_perso_height;
    }
    return new Vector2(w,h);
  }

  public override Bounds getBounds(){
    getSize();
    min_x = -w/2;
    max_x = w/2;
    min_y = -2;
    return new Bounds(new Vector3(min_x+w/2,min_y+h/2,0),new Vector3(w,h,0));
  }

}
