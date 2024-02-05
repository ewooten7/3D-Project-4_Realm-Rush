using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //will ALWAYS be in use in edit & play modes
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
/*L 121: Coordinate text color*/
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;



    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint; //L 121

    void Awake() {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        waypoint = GetComponentInParent<Waypoint>(); //L 121
        DisplayCoordinates(); //makes coordinates display correctly in Play Mode
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        
        SetLabelColor();
        ToggleLabels();
    }
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C)) //Press C to turn On/Off
        {
            label.enabled = !label.IsActive(); //sets current state to opposite state
        }
    }

    void SetLabelColor()
    {
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }




    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); //corrects coordinate numbers by position

        label.text = coordinates.x + "," + coordinates.y; //displays XY coordinates of grid blocks
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString(); //updates Grid Names real-time
    }
}
