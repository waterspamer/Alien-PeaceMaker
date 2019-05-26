using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelBuilder : MonoBehaviour
{

    [Header("Bioms Settings")]

    
    public static List<GameObject> Bioms;

    [SerializeField] private GameObject StartLocation;

    [SerializeField] private GameObject SingleCorridor;

    [SerializeField] private GameObject EnemySpawnLocation;

    [SerializeField] private GameObject TurningCorridor;

    [SerializeField] private GameObject EndLocation;

    private float _timeGen;

    private static int[,] lvlInstance;

    void OnGUI()
    {
        string msg = "";
        msg += "Location(" + lvlInstance.GetUpperBound(0)+1 + " by " + lvlInstance.GetUpperBound(1)+1+ "...................Generated in " + _timeGen.ToString() + " ms.........";

        GUI.Label(new Rect(20, 0, 500, 500), msg);
    }

    void Awake()
    {
        var t1 = System.DateTime.Now.Millisecond;
        lvlInstance = ProceduralGeneration.data;
        Bioms = new List<GameObject>();
        Bioms.Add(StartLocation);
        Bioms.Add(SingleCorridor);
        Bioms.Add(EnemySpawnLocation);
        Bioms.Add(TurningCorridor);
        Bioms.Add(EndLocation);
        ConstructLocation(lvlInstance);
        var floor = new GameObject();
        var tmp = floor.AddComponent<BoxCollider>();
        tmp.size = new Vector3(6 * (lvlInstance.GetLength(0)+3), .2f, 6 * (lvlInstance.GetLength(1)+3));
        floor.transform.position += new Vector3(-tmp.size.x / 2 +20, 0, tmp.size.z / 2 - 10);
        floor.layer = 12;
        gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        _timeGen = System.DateTime.Now.Millisecond - t1;
    }

    class BiomToBuild
    {
        Biom b;
        Quaternion rotation;

        public BiomToBuild (Biom b, Quaternion rotation)
        {
            this.b = b;
            this.rotation = rotation;
        }
        public GameObject GetGameObject()
        {
            if (b == Biom.emptySpace) return null;
            return Bioms[(int)b];
        }
        public Quaternion GetRotation()
        {
            return rotation;
        }
    }

    enum Biom
    {
        startSpot,
        singleCorridor,
        enemySpot,
        turningCorrdor,
        finishSpot,
        emptySpace
    }

    private void ConstructLocation (int[,] locationData)
    {
        for (int i = 0; i <= lvlInstance.GetUpperBound(0) ; i++)
        {
            for (int j = 0; j <= lvlInstance.GetUpperBound(1); j++)
            {
                BuildBiom(GetBiom(i, j), new Vector3(-6.2f * i , 0, 6.2f * j ));
            }
        }
    }

    private void BuildBiom(BiomToBuild b, Vector3 position)
    {
        if (b.GetGameObject()!= null)
        Instantiate(b.GetGameObject(), position, b.GetRotation());
    }

    private BiomToBuild GetBiom(int i, int j)
    {

        switch (lvlInstance[i, j])
        {
            case 1:
                {
                    return new BiomToBuild(Biom.startSpot, Quaternion.Euler(0, 90, 0));
                }
            case 2:
                {
                    return new BiomToBuild(Biom.enemySpot, Quaternion.identity);
                }
            case 3:
                {
                    return new BiomToBuild(Biom.finishSpot, Quaternion.Euler(0, 180, 0));
                }
            case 4:
                {
                            {
                                if (j == 0)
                                {
                                    return (lvlInstance[i - 1, j] == 0 ? new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 180, 0)) :
                                        lvlInstance[i + 1, j] == 0 ? new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, -90, 0)) :
                                        new BiomToBuild(Biom.singleCorridor, Quaternion.Euler(0, 0, 0)));



                                    if (lvlInstance[i - 1, j] == 0 && lvlInstance[i + 1, j] == 4) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 180, 0));
                                    if (lvlInstance[i - 1, j] != 0 && lvlInstance[i + 1, j] != 0) return new BiomToBuild(Biom.singleCorridor, Quaternion.Euler(0, 0, 0));
                                    if (lvlInstance[i - 1, j] != 0 && lvlInstance[i + 1, j] == 0 && lvlInstance[i, j + 1] != 0) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, -90, 0));
                                }
                            }

                            {
                                if (j == lvlInstance.GetUpperBound(1))
                                {

                                    return lvlInstance[i - 1, j] != 0 ? lvlInstance[i, j - 1] != 0 ? new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 0, 0)) :
                                        new BiomToBuild(Biom.singleCorridor, Quaternion.Euler(0, 0, 0)) :
                                        new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 90, 0));


                                    if (lvlInstance[i - 1, j] != 0 && lvlInstance[i, j - 1] != 0) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 0, 0));
                                    if (lvlInstance[i, j - 1] != 0 && lvlInstance[i + 1, j] != 0) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 90, 0));
                                    if (lvlInstance[i - 1, j] != 0 && lvlInstance[i + 1, j] != 0) return new BiomToBuild(Biom.singleCorridor, Quaternion.Euler(0, 0, 0));
                                }

                                    if (lvlInstance[i, j - 1] == 4 && lvlInstance[i, j + 1] == 0 && lvlInstance[i + 1, j] != 0) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 90, 0));
                                    if (lvlInstance[i, j + 1] == 4 && lvlInstance[i + 1, j] == 4 && lvlInstance[i, j - 1] == 0) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 180, 0));
                                    if (lvlInstance[i + 1, j] == 0 && lvlInstance[i, j + 1] == 4 && lvlInstance[i - 1, j] == 4) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, -90, 0));
                                    if (lvlInstance[i + 1, j] == 0 && lvlInstance[i, j - 1] == 4 && lvlInstance[i - 1, j] == 4) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 0, 0));
                                    if (j != 0 && j != lvlInstance.GetUpperBound(0))
                                   if (lvlInstance[i + 1, j] == 4 && lvlInstance[i, j - 1] == 0 && lvlInstance[i - 1, j] == 0 && lvlInstance[i, j + 1] == 4) return new BiomToBuild(Biom.turningCorrdor, Quaternion.Euler(0, 180, 0));
                                    if (lvlInstance[i + 1, j] != 0) return new BiomToBuild(Biom.singleCorridor, Quaternion.Euler(0, 0, 0));

                                    if (lvlInstance[i + 1, j] == 0 && lvlInstance[i - 1, j] == 0) return new BiomToBuild(Biom.singleCorridor, Quaternion.Euler(0, 90, 0));
                               

                            }
                            
                    }

                break;
                    
                }       

        return new BiomToBuild(Biom.emptySpace, Quaternion.identity);

    }
}
