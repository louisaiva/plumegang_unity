using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_BASE : MonoBehaviour
{

    [ReadOnly] public int build_ref = -1; // ref du build -> définit quel build est affiché, sa taille, sa position, etc.

    [ReadOnly] public float w; // largeur du build
    [ReadOnly] public float h; // hauteur du build
    
    [ReadOnly, SerializeField] private Object[] sprites;


    public void LoadBuild(int new_ref){

        build_ref = new_ref;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // on charge les sprites
        sprites = Resources.LoadAll("imports/tilemaps/builds");

        // on applique la texture du sprite
        renderer.sprite = (Sprite) sprites[build_ref+1]; //+1 car le sprite 0 est le sprite non slicé

        // on sauvegarde les dimensions du build
        w = renderer.sprite.bounds.size.x;
        h = renderer.sprite.bounds.size.y;
    }
}
