
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    public GameObject player; // le joueur
    public GameObject cam; // camera
    public GameObject sman; // street manager

    void Start() {

        // on recupere le street manager
        sman = GameObject.FindWithTag("street");

        // on recupere le joueur et la camera
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera");


    }

    public void ChangeScene(string dest_scene_name){

        // on supprime la scene actuelle
        SceneManager.UnloadSceneAsync(sman.scene.name);

        // on charge la nouvelle scene
        SceneManager.LoadScene(dest_scene_name, LoadSceneMode.Additive);

        // on récupère le nouveau street manager
        sman = GameObject.FindWithTag("street");

        // on bouge le joueur dans la street
        player.GetComponent<Perso>().EnterStreet(dest_scene_name);

        // on bouge la cam dans la street
        cam.GetComponent<CameraFollow>().EnterStreet();
    }
}