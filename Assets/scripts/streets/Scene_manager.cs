
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scene_manager : MonoBehaviour
{
    public GameObject player; // le joueur
    public GameObject cam; // camera
    public GameObject sman; // street manager
    public GameObject uiman; // ui manager

    private IEnumerator changing_routine;

    void Start() {

        // on recupere le street manager
        sman = GameObject.FindWithTag("level");

        // on recupere le joueur et la camera
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera");

        // on recupere le ui manager
        uiman = GameObject.FindWithTag("ui_manager");


    }

    public void ChangeScene(string dest_scene_name){
        changing_routine = ChangeSceneIE(dest_scene_name);
        StartCoroutine(changing_routine);
    }

    IEnumerator ChangeSceneIE(string dest_scene_name){

        //string old_scene_name = sman.scene.name;

        // on supprime la scene actuelle
        SceneManager.UnloadSceneAsync(sman.scene.name);

        // on charge la nouvelle scene
        SceneManager.LoadScene(dest_scene_name, LoadSceneMode.Additive);
        
        yield return new WaitForFixedUpdate();


        // on récupère le nouveau street manager
        sman = GameObject.FindWithTag("level");

        // on bouge le joueur dans la street
        player.GetComponent<Perso>().EnterStreet(dest_scene_name);

        // on bouge la cam dans la street
        cam.GetComponent<CameraFollow>().EnterStreet();

        // on met à jour l'affichage du nom de la rue
        uiman.GetComponent<UI_Manager>().EnterStreet(dest_scene_name);

        //Debug.Log( old_scene_name+" => "+dest_scene_name);
    }
}