using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI text;
    bool isPlayerClose;
    private void Start()
    {
        this.name = GetComponent<GroundItem>().item.name;
        text.text = GetComponent<GroundItem>().item.name;
    }
    private void Update()
    {
        if(IsPlayerInsideOfRadius())
        {
            OpenCanvas();
            if(Input.GetKey(KeyCode.E))
            {
                var item = gameObject.GetComponent<GroundItem>();
                PlayerMovement.instance.inventory.AddItem(new Item(item.item), 1);
                Destroy(this.gameObject);
            }
        }
        else
        {
            CloseCanvas();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private bool IsPlayerInsideOfRadius()
    {
        Collider [] colliders= Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false ;
    }
    private void OpenCanvas()
    {
        canvas.gameObject.SetActive(true);
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position);
    }
    private void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}
