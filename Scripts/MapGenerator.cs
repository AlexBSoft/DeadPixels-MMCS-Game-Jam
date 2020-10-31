using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public TileBase tileBase1;
    public TileBase tileBase2;
    public TileBase tileWall;
    public float mod;
    public float mod2;
    public TileBase tileBaseTree;

    public GameObject tileobj2;

    private float value;
    private float value2;
    // Start is called before the first frame update
    void Start()
    {
        mod = Random.Range(-1.0f, 1.0f);
        mod2  = Random.Range(-1.0f, 1.0f);
        int yy =0;
        for (int y = -20; y< 21; y++){
            int xx = 0;
            yy++;
            for (int x = -20; x<21; x++){
                
                value = Mathf.PerlinNoise(xx * mod, yy *mod);
                value2 = Mathf.PerlinNoise(xx * mod2, yy *mod2);
                xx++;
                //Debug.Log(value);
                if(y==-20 || y==20 ||x==-20 || x==20 )
                    tileobj2.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tileWall );
                else{
                    if(value < 0.6f)
                        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tileBase1 );
                    else
                        gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tileBase2 );
                }
                /*
                //Drawing wood
                if(value > 0.7f && value2 > 0.4f){
                    GameObject.Find("ObjectsTilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tileBaseTree );
                }
                */
                    
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
