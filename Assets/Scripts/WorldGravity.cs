using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGravity : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] m_Rigidbodies;
    [SerializeField] private float m_Multiplier = 20.0f;

    IEnumerator Routine()
    {
        while (true)
        {
            m_Rigidbodies = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
            for (int x = 0; x < m_Rigidbodies.Length; x++)
            {
                yield return new WaitForSeconds(0);
                for (int y = 0; y < m_Rigidbodies.Length; y++)
                {
                    if (x != y)
                    {
                        m_Rigidbodies[x].AddForce((m_Rigidbodies[y].gameObject.transform.position - m_Rigidbodies[x].transform.position) * (m_Rigidbodies[y].mass / Vector3.Distance(m_Rigidbodies[x].transform.position, m_Rigidbodies[y].transform.position) * m_Multiplier));
                        yield return new WaitForSeconds(0);
                    }
                }
            }
            yield return new WaitForSeconds(0);
        }
    }
    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(Routine());
    }
}
