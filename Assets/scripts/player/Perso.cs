
using UnityEngine;
using UnityEngine.Tilemaps;

public class Perso : MonoBehaviour
{

  // init des variables d'instance

  public float speed;
  public GameObject player;

  public Rigidbody2D rb;
  public Animator animator;
  public SpriteRenderer sr;

  public GameObject ground;

  private Vector3 velocity = Vector3.zero;
  private float ground_force = 2f;

  // I - FONCTIONS PRIMORDIALES (1.0)

  // init
  void Start()
  {
    //
  }

  // update de d'habitude
  void FixedUpdate()
  {

    // on récup la vitesse puis on bouge
    float y_speed = (1/2)*speed;
    float x_mov = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    float y_mov = Input.GetAxis("Vertical") * speed/4 * Time.deltaTime;
    //Debug.Log(y_speed);
    Move_player(x_mov,y_mov);

    //Debug.Log(isGrounded());

    // on met à jour les animations
    if (rb.velocity.x > 0.1f){
      sr.flipX = false;
    }else if (rb.velocity.x < -0.1f){
      sr.flipX = true;
    }

    animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
  }


  // II - FONCTIONS SECONDAIRES (2.0)

  void Move_player(float _x_mov,float _y_mov){
    // fonction pour bouger le joueur
    float total_y_mov;

    // on vérifie si le joueur est dans l'air, si oui on applique la gravité
    if (isGrounded()){
      rb.gravityScale = 0;
      total_y_mov = _y_mov;
    }
    else{
      rb.gravityScale = 1;
      total_y_mov = rb.velocity.y;
    }

    // on calcule le vecteur mouvement et on bouge
    Vector3 target_velocity = new Vector2(_x_mov,total_y_mov);
    rb.velocity = Vector3.SmoothDamp(rb.velocity,target_velocity, ref velocity, .05f);
  }

  bool isGrounded(){
    // vérifie si le perso est sur le sol ou dans l'air

    //float ground_max_y = ground.transform.position.y + (ground.GetComponent<Renderer> ().bounds.size.y)/2; // on récupère le dessus du sol
    //float player_min_y = player.transform.position.y - (player.GetComponent<Renderer> ().bounds.size.y)/2; // on récupère le dessous du joueur

    if (rb.IsTouching(ground.GetComponent<TilemapCollider2D>())){
      return true;
    }
    return false;
  }

}
