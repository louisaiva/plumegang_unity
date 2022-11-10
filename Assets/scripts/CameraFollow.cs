using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{

  public GameObject player;
  public GameObject sman; // street manager
  public float timeOffset;


  private Vector3 velocity;
  public float Y_OFF;


  void Start()
  {

    // on entre dans la street
    EnterStreet();

  }

  public void EnterStreet(){

    sman = GameObject.FindWithTag("street");

  }

  void Update()
  {

    // X

    float final_x = player.transform.position.x;
    float min_x = sman.GetComponent<Street>().getBounds().x;
    float max_x = sman.GetComponent<Street>().getBounds().y;

    float x_movement = final_x - transform.position.x;

    // on vérifie si le perso est à gauche de la map
    if (GetComponent<Camera>().ViewportToWorldPoint(Vector3.zero).x + x_movement <= min_x){
      float dx = min_x - GetComponent<Camera>().ViewportToWorldPoint(Vector3.zero).x;
      final_x = transform.position.x+dx;
    }

    // on vérifie si le perso est à droite de la map
    else if (GetComponent<Camera>().ViewportToWorldPoint(Vector3.right).x + x_movement >= max_x){
      float dx = max_x - GetComponent<Camera>().ViewportToWorldPoint(Vector3.right).x;
      final_x = transform.position.x+dx;
    }

    // Y

    float final_y = player.transform.position.y + Y_OFF;
    float min_y = sman.GetComponent<Street>().getBounds().z;
    float y_movement = final_y - transform.position.y;

    // on vérifie si le perso est en bas de la map
    if (GetComponent<Camera>().ViewportToWorldPoint(Vector3.zero).y + y_movement <= min_y){
      float dy = min_y - GetComponent<Camera>().ViewportToWorldPoint(Vector3.zero).y;
      final_y = transform.position.y+dy;
    }

    // on calcule la position finale de la cam, puis on la déplace
    Vector3 final_position = new Vector3(final_x,final_y,transform.position.z);
    transform.position = Vector3.SmoothDamp(transform.position,final_position, ref velocity, timeOffset);
  }
}
