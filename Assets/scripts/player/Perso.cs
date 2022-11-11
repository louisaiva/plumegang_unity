
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.SceneManagement;


public class Perso : MonoBehaviour
{

  // init des variables d'instance

  public float speed;
  public GameObject player;
  public GameObject sman; // street manager

  public Rigidbody2D rb;
  public Animator animator;
  public SpriteRenderer sr;

  public GameObject ground;
  private float y_velocity;
  private float x_velocity;
  //public float rbvelocity_y;

  private Vector3 velocity = Vector3.zero;
  private Vector2 inputs;
  
  public Scene scene;


  public int activating_thg = 0; // 1 si activating on Z, -1 on S, 0 sinon
  private IEnumerator activating_routine;


  // I - FONCTIONS PRIMORDIALES (1.0)

  // init
  void Start(){
    //feet_perso_height = sman.GetComponent<Street_manager>().feet_perso_height;
    rb.gravityScale = 0;

    // on entre dans la street
    EnterStreet("home");

    }

  public void EnterStreet(string dest_scene_name){

    // on récupère les nouveaux composants
    GameObject[] gameObjects = SceneManager.GetSceneByName(dest_scene_name).GetRootGameObjects();
    
    foreach (GameObject go in gameObjects)
    {
      if (go.tag == "level"){
        sman = go;
      }
    }
    if (sman == null){
      Debug.Log("pas de sman pour le perso :/");
    }

    ground = sman.transform.GetChild(0).GetChild(0).gameObject;
    //Debug.Log("on entre dans "+dest_scene_name);

  }

  void Events(){

    // Z,S,Q,D
    inputs = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

    // Z,S activation zone
    if (Input.GetAxis("Vertical") != 0 && activating_thg == 0){
      //Debug.Log("oula");

      // on vérifie si on touche une zone et qu'en même temps on est en buttée en haut ou en bas et qu'on est pas deja en train d'activer un bail
      LayerMask mask = LayerMask.GetMask("Zone_ACTIV");
      if (rb.IsTouchingLayers(mask) && isOnBounds().y != 0){
        //Debug.Log("oula2");

        // on crée notre filtre pour récupérer seulement la 1e zone active
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(mask);
        filter.useTriggers = true;
        Collider2D[] results = {new Collider2D()};

        // on récup la zone
        rb.OverlapCollider(filter,results);
        Zone_ACTIV zone = results[0].transform.gameObject.GetComponent<Zone_ACTIV>();

        // on vérifie qu'on essaie de l'activer du bon coté
        if ((zone.activating_position == "back" && isOnBounds().y == 1) ||
            (zone.activating_position == "front" && isOnBounds().y == -1)){

              // on met à actif
              activating_thg = (int) isOnBounds().y;

              // on joue l'animation d'activation
              animator.Play("rapper_activate");

              // on met a jour la routine et on la lance
              activating_routine = Activate_thg(zone);
              StartCoroutine(activating_routine);
            }

        }
      }

    // Z,S desactivation zone
    else if (Input.GetAxis("Vertical") == 0 && activating_thg != 0){
      // on regarde si on est en train d'actver qqch
      if (activating_thg == isOnBounds().y){

        // on met à inactif
        activating_thg = 0;
        // on arrete l'animation d'activation
        animator.Play("rapper_idle");
        // on stop la routine
        StopCoroutine(activating_routine);
      }
      }
    }

// update de d'habitude
  void Update(){

    // events
    Events();


    // on met à jour les animations
    if (rb.velocity.x > 0.1f){
      sr.flipX = false;
    }else if (rb.velocity.x < -0.1f){
      sr.flipX = true;
    }
    animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));


    // veloc
    //rbvelocity_y = rb.velocity.y;
  }

  void FixedUpdate(){

    // on récup la vitesse puis on bouge
    float y_speed = .125f*speed;
    x_velocity = inputs.x * speed * Time.deltaTime;
    y_velocity = inputs.y * y_speed * Time.deltaTime;

    //Debug.Log(y_velocity);
    Move_player(x_velocity,y_velocity);
    }


  // II - FONCTIONS SECONDAIRES (2.0)

  void Move_player(float _x_velocity,float _y_velocity){
    rb.velocity = new Vector3(_x_velocity,_y_velocity,0);
    }

  IEnumerator Activate_thg(Zone_ACTIV thg){

    yield return new WaitForFixedUpdate();
    yield return new WaitWhile(() => animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "rapper_activate");

    thg.activate();
    activating_thg = 0;

    }

  // III - GETTERS

  bool isGrounded(){
    // vérifie si le perso est sur le sol ou dans l'air

    if (rb.IsTouching(ground.GetComponent<Collider2D>())){
      return true;
    }
    return false;
    }

  Vector2 isOnBounds(){

    // /!\ attention petite latence de 1 tour de boucle -> Rigidbody2D met à jour sa vitesse 1 tour après
    // chaque float vaut -1 si le perso cogne contre le min (gauche ou bas), 1 si contre le max (droite,haut), 0 sinon

    float x,y;

    // x
    if (x_velocity != 0 && rb.velocity.x == 0){
      if (x_velocity > 0) {x = 1;}
      else {x=-1;}
    }
    else{
      x = 0;
    }


    // y
    if (y_velocity != 0 && rb.velocity.y == 0){
      if (y_velocity > 0) {y = 1;}
      else {y=-1;}
    }
    else{
      y = 0;
    }

    return new Vector2(x,y);
    }

  // end
}