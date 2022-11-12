
using UnityEngine;
using UnityEngine.Tilemaps;

public class Street : Level
{

  protected float no_bg_width = 50f;
  public int[] builds_ref;

  public GameObject builds_root; // root des builds
  public GameObject build_prefab;

  //public Vector2 wh_px;// dimensions en pixels d'un build (en comptant le w_bg)
  public float w_bg_px;// largeur du petit bg de build qui sera caché par les builds
  //public Vector2 wh; // dimensions en unités d'un build

  
  void Start()
  {

    if (level_name == null){
      level_name = "street";
    }

    // on récupère le root des builds
    builds_root = GameObject.Find("decor/builds");

    /* c fé dynamiquement maintenant
    // on pose les dimensions d'un build
    wh_px = new Vector2(160,130);
    wh = new Vector2((wh_px.x-w_bg_px)/32,wh_px.y/32);
    */

    // on initialise des trucs importants
    w_bg_px = 10;
    min_x = 0; // on débute à 0
    min_y = -2;
    h = level_height + feet_perso_height;

    // on créé le tableau de builds
    builds_ref = new int[] {0,2,3,4,5,6,2,4,3,4,1};

    // on créé les builds
    createBuilds();


    getBounds();

    // on donne la bonne taille et position aux bounds
    wall_L.transform.localScale = new Vector3(1,h+10,0);
    wall_L.transform.position = new Vector3(min_x - wall_L.transform.localScale.x/2,min_y + h/2,0);

    wall_R.transform.localScale = new Vector3(1,h+10,0);
    wall_R.transform.position = new Vector3(max_x + wall_R.transform.localScale.x/2,min_y + h/2,0);

    wall_ground.transform.localScale = new Vector3(w+10,1,0);
    wall_ground.transform.position = new Vector3(min_x + w/2,min_y - wall_ground.transform.localScale.y/2,0);

    float max_y = min_y + level_height + feet_perso_height;
    wall_air.transform.localScale = new Vector3(w+10,1,0);
    wall_air.transform.position = new Vector3(min_x + w/2,max_y + wall_air.transform.localScale.y/2,0);

    // on donne la bonne taille et position au sol
    ground.transform.localScale = new Vector3(w,level_height + feet_perso_height,0);
    ground.transform.position = new Vector3(min_x + w/2,min_y + ground.transform.localScale.y/2,0);

    createRoad();

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
    for (int x = road_area.xMin-1; x < road_area.xMax+1; x++)
    {
        tm.SetTile(new Vector3Int(x ,(int) min_y, 0), road_tile);
    }
    //tm.RefreshAllTiles();

    Debug.Log("road tilemap loaded");

  }

  void createBuilds(){

    w = 0;
    float x = min_x;
    int i=0;
    foreach (int bref in builds_ref)
    {
      
      // on créé le build
      GameObject build_go = Instantiate(build_prefab,builds_root.transform) as GameObject;
      build_go.name = "build_" + i.ToString();

      // on récupère le script du build
      Build_BASE build = build_go.GetComponent<Build_BASE>();

      // on charge le build
      build.LoadBuild(bref);

      // on règle la position du build
      build_go.transform.position = new Vector3(x,min_y + 1,0);

      // on pose le prochain x
      x += build.w - w_bg_px/32;
      w += build.w - w_bg_px/32;
      
      i++;
    }
    
    Debug.Log("builds tilemap loaded");
  }


  // FONCTIONS SECONDAIRES

  public override Vector2 getSize(){
    return new Vector2(w,h);
  }

  public override Bounds getBounds(){
    getSize();
    min_x = 0;
    max_x = w;
    min_y = -2;
    return new Bounds(new Vector3(min_x+w/2,min_y+h/2,0),new Vector3(w,h,0));
  }

}
