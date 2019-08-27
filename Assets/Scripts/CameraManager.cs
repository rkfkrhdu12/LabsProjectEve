using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform player;

    [SerializeField] float moveSpeed = 7;

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    void FixedUpdate()
    {
        Transform parentTrn = transform.parent;

        parentTrn.position = Vector3.Lerp(parentTrn.position, player.position, moveSpeed * 2 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log(other.name);
            Material mat = other.GetComponent<MeshRenderer>().material;

            mat.SetFloat("_Mode", 3);
            mat.color = Color.clear;
            //mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            //mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log(other.name);
            Material mat = other.GetComponent<MeshRenderer>().material;

            mat.SetFloat("_Mode", 0);
            mat.color = Color.white;
            //mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            //mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.renderQueue = 3000;
        }
    }
}