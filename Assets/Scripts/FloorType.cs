using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorType : MonoBehaviour
{
    public static FloorType Instance { get; private set; }
    [SerializeField] private Transform glassFloor;
    [SerializeField] private Transform metalFrameFloor;
    [SerializeField] private LayerMask targetLayer;

    // Start is called before the first frame update
    private Vector2 glassFloorAreaSize = new Vector2(6f, 7f);
    private Vector2 metalFloorAreaSize = new Vector2(12f, 10f);

    private enum FloorState {
        Glass,
        Metal,
        Default
    }

    private FloorState floorState;

    private void Awake() {
        Instance = this;
        floorState = FloorState.Default;
    }

    // Update is called once per frame
    private void Update()
    {
        floorState = FloorState.Default;
        var playerWalkOnMetalFrame = Physics2D.OverlapBox(metalFrameFloor.position, metalFloorAreaSize, 0f, targetLayer);
        if (playerWalkOnMetalFrame != null) {
            floorState = FloorState.Metal;
        }
        var playerWalkOnGlassFloor = Physics2D.OverlapBox(glassFloor.position, glassFloorAreaSize, 0f, targetLayer);
        if (playerWalkOnGlassFloor != null) {
            floorState = FloorState.Glass;
        }

        

        
    }

    public bool GlassFloor() {
        return floorState == FloorState.Glass;
    }

    public bool MetalFloor() {
        return floorState == FloorState.Metal;
    }

    public bool DefaultFloor() {
        return floorState == FloorState.Default;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(glassFloor.position, glassFloorAreaSize);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(metalFrameFloor.position, metalFloorAreaSize);
    }
}
