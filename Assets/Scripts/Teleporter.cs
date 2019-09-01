using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : DataChip
{
    public Transform arrivalPoint;
    public GameObject cameraObj;

    protected override void Teleport()
    {
        Transform playerTrn = GameManager.Instance.player.transform;
        playerTrn.position = arrivalPoint.position;
        playerTrn.rotation = arrivalPoint.rotation;

        cameraObj.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
