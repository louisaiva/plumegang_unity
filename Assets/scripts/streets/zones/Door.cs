using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : Zone_ACTIV
{

  public GameObject scman; // scene manager
  public string dest_scene_name;

  protected override void Start() {
    scman = GameObject.Find("scene_manager");

    // on change le nom
    zone_name = dest_scene_name;

    base.Start();
  }

  public override void activate(){

    base.activate();

    // changement de scene
    scman.GetComponent<Scene_manager>().ChangeScene(dest_scene_name);


  }

}
