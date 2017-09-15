using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Element : MonoBehaviour
{
    public bool mine;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
	void Start ()
    {
        mine = Random.value < 0.15;

        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Grid.elements[x, y] = this;
        
	}
	public void loadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;

        else 
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }
    public void OnMouseUpAsButton()
    {
        if (mine)
        {
            Grid.uncoverMines();
            StartCoroutine("Toasty");
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Grid.adjacentMines(x, y));

            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);
        }
        if (Grid.isFinished())
            print("you win!");
    }
    IEnumerator Toasty()
    {
        print("toastinprogress");
        yield return new WaitForSeconds(10);
    }
}
