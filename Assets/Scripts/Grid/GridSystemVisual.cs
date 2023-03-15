using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Generates Visuals for the tiles that the unit can move to
public class GridSystemVisual : MonoBehaviour
{

    public static GridSystemVisual Instance { get; private set; }
    // The prefab for holding the highlight visual
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    // Array of the highlighted tiles
    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //* Start is called before the first frame update
    private void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
        ];

        // Creates the outlines for the tiles that are legal move option
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform gridSystemVisualSingleTransform =
                    Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    //* Update is called once per frame
    private void Update()
    {
        UpdateGridVisual();
    }

    //* Hides all the outlines for the movement range of characters
    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    //* Shows all the outlines for valid movement options of the character
    // @param gridPositionsList the list of positions that the grid has
    public void ShowGridPostionList(List<GridPosition> gridPositionsList)
    {
        foreach (GridPosition gridPosition in gridPositionsList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    //* Updates to show the valid movement options of a unit after it has been selected or moved
    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        ShowGridPostionList(
            selectedAction.GetValidActionGridPositionList());
    }
}
