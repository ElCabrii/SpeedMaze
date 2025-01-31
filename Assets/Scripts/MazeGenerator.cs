using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private BlockCell _blockCellPrefab;
    [SerializeField] private GameObject _doorPrefab;
    [SerializeField] private GameObject _exitPrefab;

    [SerializeField] private BlockCell[,] _blockGrid;
    [SerializeField] private int blockSize = 7;

    private List<GameObject> generatedBlocks = new List<GameObject>(); // List to keep track of generated blocks
    

    void Start()
    {
        int blocksPerMaze = PlayerPrefs.GetInt("Difficulty") * 5;
        GenerateMaze(blocksPerMaze);
    }

    void GenerateMaze(int blocksPerMaze)
    {
        Vector3 currentPosition = Vector3.zero; // Start position for the first block
        int entrance = Random.Range(0, blockSize); // Random entrance position
        int exit = Random.Range(0, blockSize); // Random exit position

        for (int i = 0; i < blocksPerMaze; i++)
        {
            GameObject block = new GameObject($"Block_{i}");

            _blockGrid = new BlockCell[blockSize, blockSize];
            for (int x = 0; x < blockSize; x++)
            {
                for (int z = 0; z < blockSize; z++)
                {
                    _blockGrid[x, z] = Instantiate(_blockCellPrefab, new Vector3(x, 0, z), Quaternion.identity, block.transform);
                }
            }
            if (i != 0)
            {
                _blockGrid[0, entrance].ClearLeftWall();
            }
            _blockGrid[blockSize - 1, exit].ClearRightWall();
            GameObject door = Instantiate(_doorPrefab, new Vector3(blockSize, 0, exit), Quaternion.identity, block.transform);
            door.transform.position += new Vector3(-0.5f, 0, 0);
            door.transform.rotation = Quaternion.Euler(-90, 0, 0);
            if (i == blocksPerMaze - 1)
            {
                GameObject exitBlock = Instantiate(_exitPrefab, new Vector3(blockSize, 0, exit), Quaternion.identity, block.transform);
                exitBlock.transform.position += new Vector3(1, -1, 0);
            }
            entrance = exit;
            exit = Random.Range(0, blockSize);
            GenerateBlock(null, _blockGrid[0, 0]);
            block.transform.localScale = new Vector3(3, 3, 3);
            generatedBlocks.Add(block);
            block.transform.position = currentPosition;
            currentPosition += new Vector3(blockSize * 3, 0, 0);
        }
    }
    private void GenerateBlock(BlockCell previousCell, BlockCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        BlockCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);
            if (nextCell != null)
            {
                GenerateBlock(currentCell, nextCell);
            }
        } while (nextCell != null);
    }
    private BlockCell GetNextUnvisitedCell(BlockCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<BlockCell> GetUnvisitedCells(BlockCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < blockSize)
        {
            var cellToRight = _blockGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }
        if (x - 1 >= 0)
        {
            var cellToLeft = _blockGrid[x - 1, z];
            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < blockSize)
        {
            var cellToFront = _blockGrid[x, z + 1];
            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _blockGrid[x, z - 1];
            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(BlockCell previousCell, BlockCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }
        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        else if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        else if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        else if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
