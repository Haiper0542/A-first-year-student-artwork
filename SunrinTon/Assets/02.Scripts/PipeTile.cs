using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTile : MonoBehaviour {

    public bool[] open = new bool[4];
    public bool isWater;

    public bool isFixed = false;


    public void RotatePipe()
    {
        transform.Rotate(0, 0, -90);

        bool temp = open[3];
        open[3] = open[2];
        open[2] = open[1];
        open[1] = open[0];
        open[0] = temp;
    }

    public void WaterUpdate(int dir)
    {
        for(int i = 0; i < 4; i++)
        {
            if (i == dir) continue;

            if (open[i])
            {
                PipeTile linkedPipe;
                Collider2D hit = null;
                switch (i)
                {
                    case 0://위로
                        if (Physics2D.OverlapBox(transform.position + Vector3.up * 1.5f, new Vector2(0.5f, 0.5f), 0))
                            hit = Physics2D.OverlapBox(transform.position + Vector3.up * 1.5f, new Vector2(0.5f, 0.5f), 0);
                        if (hit != null)
                        {
                            linkedPipe = hit.GetComponent<PipeTile>();
                            if (linkedPipe.open[2])
                            {
                                linkedPipe.isWater = true;
                                linkedPipe.WaterUpdate(2);
                            }
                        }
                        break;
                    case 1://오른쪽으로
                        if (Physics2D.OverlapBox(transform.position + Vector3.right * 1.5f, new Vector2(0.5f, 0.5f), 0))
                            hit = Physics2D.OverlapBox(transform.position + Vector3.right * 1.5f, new Vector2(0.5f, 0.5f), 0);
                        if (hit != null)
                        {
                            linkedPipe = hit.GetComponent<PipeTile>();
                            if (linkedPipe.open[3])
                            {
                                linkedPipe.isWater = true;
                                linkedPipe.WaterUpdate(3);
                            }
                        }
                        break;
                    case 2://아래로
                        if (Physics2D.OverlapBox(transform.position + Vector3.down * 1.5f, new Vector2(0.5f, 0.5f), 0))
                            hit = Physics2D.OverlapBox(transform.position + Vector3.down * 1.5f, new Vector2(0.5f, 0.5f), 0);
                        if (hit != null)
                        {
                            linkedPipe = hit.GetComponent<PipeTile>();
                            if (linkedPipe.open[0])
                            {
                                linkedPipe.isWater = true;
                                linkedPipe.WaterUpdate(0);
                            }
                        }
                        break;
                    case 3://왼쪽으로
                        if (Physics2D.OverlapBox(transform.position + Vector3.left * 1.5f, new Vector2(0.5f, 0.5f), 0))
                            hit = Physics2D.OverlapBox(transform.position + Vector3.left * 1.5f, new Vector2(0.5f, 0.5f), 0);
                        if (hit != null)
                        {
                            linkedPipe = hit.GetComponent<PipeTile>();
                            if (linkedPipe.open[1])
                            {
                                linkedPipe.isWater = true;
                                linkedPipe.WaterUpdate(1);
                            }
                        }
                        break;
                }
            }
        }
    }
}
