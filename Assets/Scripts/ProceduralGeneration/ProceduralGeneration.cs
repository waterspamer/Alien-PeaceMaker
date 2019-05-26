using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{

    [Header ("Generation Params")]

    private float GenerationSeed = .435f;
    [Range(16, 40)] public int Length = 20;
    [Range(16, 40)] public int Width = 20;

    

    public static int[,] data
    {
        get; private set;
    }

    public bool showDebug;

    void Awake()
    {
        data = GenerateLevelBlueprint(Length, Width);
    }

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        int[,] lvl = data;
        int rMax = lvl.GetUpperBound(0);
        int cMax = lvl.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (lvl[i, j] == 0)
                {
                    msg += "....";
                }
                if (lvl[i,j] == 1)
                {
                    msg += "ST";
                }
                if (lvl[i, j] == 2)
                {
                    msg += "EN";
                }
                if (lvl[i, j] == 3)
                {
                    msg += "FF";
                }
                if (lvl[i, j] == 4)
                {
                    msg += "W|";
                }
            }
            msg += "\n";
        }
        msg += "\n";
        
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    private int[] GetEnemySpawnerCoord()
    {
        string seeded = ((int)(GenerationSeed * 1000)).ToString();

        return new int[] {seeded[1] - seeded[2] > 0 ? Random.Range(0, (int)((float)Length / seeded[0] * 10)) + 2 : Random.Range(Length / 2, (int)((float)Length / seeded[0] * 10)) +2 ,
            seeded[1] - seeded[2] > 0 ? Random.Range(0, (int)((float)Width / seeded[0] * 10)) : Random.Range(Width / 2, (int)((float)Width / seeded[0] * 10)) };
    }

    private int[,] ConnectWithWays(int[,] lvl, int[] startPoint,int [] endPoint)
    {

        var turnsCount = Random.Range(3, (int)(3 + (GenerationSeed * 1000).ToString()[Random.Range(0, 3)] /2));

        var direction = 2;
        var l = Random.Range(3, 4);
        var currentPoint = new int[] { startPoint[0], startPoint[1] };
        for (int i = 1; i <= l; i++)
        {
            lvl[startPoint[0]+i, startPoint[1]] = 4;
            currentPoint[0]++;
        }     
        
        while (turnsCount > 0)
        {
            l = Random.Range(3, 5);

            var rightTurn = Random.Range(0, 2) == 1 ? true : false;

            direction = rightTurn? direction+1 : direction-1;

            direction = direction == 4 || direction == 0 ? 2 : direction; 

            for (int i = 0; i < l; i++)
            {

                if (currentPoint[0] == endPoint[0] - 3)
                {
                    turnsCount = 0;
                    lvl[currentPoint[0] + 1, currentPoint[1]] = 4;
                    currentPoint[0]++;
                    break;
                }

                if (direction == 3 && currentPoint[1] == Width-1) direction--;

                if (direction == 1 && currentPoint[1] == 0) direction++;

                if (direction == 1)
                {
                    lvl[currentPoint[0], currentPoint[1] - 1 ] = 4;
                    currentPoint[1]--;
                }
                if (direction == 2)
                {
                    lvl[currentPoint[0] + 1, currentPoint[1]] = 4;
                    currentPoint[0]++;
                }
                if (direction == 3)
                {
                    lvl[currentPoint[0], currentPoint[1] + 1] = 4;
                    currentPoint[1]++;
                }      
            }
            turnsCount--;
            
        }

        var fac = (currentPoint[1] > endPoint[1]) ? -1 : (currentPoint[1] == endPoint[1]) ? 0 : 1;

        while (currentPoint[1] != endPoint[1])
        {        
            lvl[currentPoint[0], currentPoint[1] + fac] = 4;
            currentPoint[1] += fac;
        }
        lvl[currentPoint[0] + 1, currentPoint[1]] = 4;

        return lvl;
    }

    public int[,] GenerateLevelBlueprint(int length, int width)
    {
        
        var LevelBP = new int[length, width];

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                LevelBP[i, j] = 0;
            }
        }

        //Start loc chunk
        var startValue = Random.Range(0, width);

        LevelBP[0, startValue] = 1;


        //End loc chunk
        var endValue = Random.Range(0, width);

        LevelBP[length-1, endValue] = 3;

        //Enemy Spawn
        var spn = GetEnemySpawnerCoord();

        LevelBP[spn[0], spn[1]] = 2;

        LevelBP = ConnectWithWays(LevelBP, new int[] { 0, startValue }, new int[] { spn[0], spn[1] });

        LevelBP = ConnectWithWays(LevelBP, new int[] { spn[0], spn[1] }, new int[] { length - 1, endValue });

        return LevelBP;
    }

}
