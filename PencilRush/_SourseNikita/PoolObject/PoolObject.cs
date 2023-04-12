using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private bool _isTakeRandom = true;
#nullable enable
    public Pencil? TrygetPencil()
#nullable disable
    {
        int countPencils = gameObject.transform.childCount;

        if (countPencils > 0)
        {
            if (_isTakeRandom == true)
            {
                transform.GetChild(Random.Range(0, countPencils)).TryGetComponent<Pencil>(out Pencil pencil);
                return pencil;
            }
            else
            {
                transform.GetChild(0).TryGetComponent<Pencil>(out Pencil pencil);
                return pencil;
            }
        }
        else
        {
            return null;
        }
    }
}
