using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {
    
    // Time since last gravity tick
    float lastFall = 0;

    bool isValidGridPos()
    {
       
        foreach (Transform child in transform)
        {
            Vector3 v = Grid.roundVec3(child.position);

            // Not inside Border?
            if (!Grid.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?

            if (Grid.grid[(int)v.x, (int)v.y, (int)v.z] != null &&
                Grid.grid[(int)v.x, (int)v.y, (int)v.z].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                for (int z = 0; z < Grid.d; ++z)
                    if (Grid.grid[x, y, z] != null)
                        if (Grid.grid[x, y, z].parent == transform)
                            Grid.grid[x, y, z] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector3 v = Grid.roundVec3(child.position);
            Grid.grid[(int)v.x, (int)v.y, (int)v.z] = child;
        }
    }

    // Use this for initialization
    void Start () {
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
        FallPoint.setFallPoint(transform);
    }

    //前に進めるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveFront()
    {
        // Modify position
        transform.position += new Vector3(0, 0, 1);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(0, 0, -1);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //後ろに進めるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveBack()
    {
        // Modify position
        transform.position += new Vector3(0, 0, -1);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(0, 0, 1);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //左に動けるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveLeft()
    {
        // Modify position
        transform.position += new Vector3(-1, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(1, 0, 0);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //右に動けるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveRight()
    {
        // Modify position
        transform.position += new Vector3(1, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(-1, 0, 0);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //x軸回転できるかチェック
    void checkRotateXaxis()
    {
        transform.Rotate(-90, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.Rotate(90, 0, 0);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //y軸回転できるかチェック
    void checkRotateYaxis()
    {
        transform.Rotate(0, -90, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.Rotate(0, 90, 0);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //z軸回転できるかチェック
    void checkRotateZaxis()
    {
        transform.Rotate(0, 0, -90);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.Rotate(0, 0, 90);

        //影を設置
        FallPoint.setFallPoint(transform);
    }


    // Update is called once per frame
    void Update () {

        switch (CameraMove.getPositionNumber())
        {
            case 0:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveFront();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveBack();
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveLeft();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveRight();
                }
                break;

            case 1:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveRight();                  
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveLeft();
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveFront();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveBack();
                }
                break;

            case 2:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveBack();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveFront();                    
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveRight();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {           
                    checkMoveLeft();
                }
                break;

            case 3:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveLeft();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveRight();                    
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveBack();                  
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveFront();                   
                }
                break;
        }

        // Rotate
        if (Input.GetKeyDown(KeyCode.A))
        {
            checkRotateXaxis();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            checkRotateYaxis();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            checkRotateZaxis();
        }





        //時間経過で落ちる
        if (Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                //影を消す
                FallPoint.resetFallPoint();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;

        }

        //一気に落ちる
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            while (true)
            {

                transform.position += new Vector3(0, -1, 0);

                if (!isValidGridPos())
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);
                    break;
                }

            }
            updateGrid();

            // Clear filled horizontal lines
            Grid.deleteFullRows();

            // Spawn next Group
            FindObjectOfType<Spawner>().spawnNext();

            //影を消す
            FallPoint.resetFallPoint();

            // Disable script
            enabled = false;
        }
    }
}
