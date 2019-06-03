using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    // Is this a mine?
    public bool mine;

    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    // Start is called before the first frame update
    void Start() {
         //Randomly decide if it's a mine or not
        mine = Random.value < 0.15;

        // Register in Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Grid.elements[x, y] = this;
    }

    // Load another texture
    public void loadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }

    // Is it still covered?
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";

    }

    void OnMouseUpAsButton() {
        // It's a mine
        if (mine)
        {
            // uncover all mines
            Grid.uncoverMines();

            // game over
            print("you lose");
        }
        // It's not a mine
        else
        {
            // show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Grid.adjacentMines(x, y));

            // uncover area without mines
            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

            // find out if the game was won now
            if (Grid.isFinished())
                print("you win");
        }

    }
}


