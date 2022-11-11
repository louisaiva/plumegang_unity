
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    public void EnterStreet(string dest_scene_name)
    {
        // on met à jour l'affichage du nom de la rue
        GameObject.Find("street_name").GetComponent<TextMeshProUGUI>().text = dest_scene_name;

    }
}
