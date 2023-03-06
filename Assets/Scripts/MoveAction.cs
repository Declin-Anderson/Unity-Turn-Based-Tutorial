using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    // Getting the reference of the Animator
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;
    // Target Spot for the model to move to
    private Vector3 targetPosition;
    private Unit unit;

    private void Awake() 
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }
    
    private void Update() 
    {
        float stoppingDistance = .1f;

        // Checks to see if the distance from the unit to its targets spot is greater than 0
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            // Determining the direction and the speed that the unit shall reach its end goal
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Rotation of the model when it moves to create a smooth transtation between animation states
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            // Setting the bool for the animator on the unit
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            // Setting the bool for the animator on the unit
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    // Changes the target vector for the model to move to
    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    // Gathers the valid position that the unit can move to
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // Same Position
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid Position has a unit in it
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
