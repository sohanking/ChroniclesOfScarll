using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class mineController : MonoBehaviour {

    public GameObject prefabMine;

    public int SeriesLength;
    private int gridX;
    private int gridY;
    public float spacing;
   public List<Transform> paths;
    public Text[] mineTextArr;
    
  public  GameObject[] mineParticles;
    void Awake()
    {
        gridX = gridY = SeriesLength;
        int count = 0;
        paths = new List<Transform>();
       
           
        mineTextArr = new Text[gridX * gridY];
        mineParticles = new GameObject[gridX * gridY];
        for (int y = 0; y < gridY; y++)
        {
         
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, 0.2f, y) * spacing;
                GameObject mine = Instantiate(prefabMine, pos, Quaternion.identity) as GameObject;
                mine.name = "mine" + count;
                paths.Add(mine.transform);
                mineTextArr[count] = mine.GetComponentInChildren<Text>();
              
                mineParticles[count]=mine.GetComponentInChildren<ParticleSystem>(true).transform.parent.gameObject;
                mine.transform.parent = this.gameObject.transform;
               
                count++;

            }
        }

    }
}
