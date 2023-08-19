using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI text;
    private void Start()
    {
        text.text = this.gameObject.name;
    }
    private void Update()
    {
        if(IsPlayerInsideOfRadius())
        {
            OpenCanvas();
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
        return false;
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
