using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool _isPressed = false;
    public float releaseTime = 0.15f;
    public Rigidbody2D Hook;
    public float maxDragDistance = 2f;
    public GameObject nextBall;

    private void Update()
    {
        if (_isPressed == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, Hook.position) > maxDragDistance)
            {
                rb.position = Hook.position + ( mousePos - Hook.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        _isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
        this.enabled = false;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;

        yield return new WaitForSeconds(2f);

        if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Enemy.EnemiesAlive = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
