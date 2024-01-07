using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PowerTypeEnum
{

}
public class GenericPool : MonoBehaviour
{
    [SerializeField] BaseBower[] _baseBowers;
    [SerializeField] int _count;
    Dictionary<PowerTypeEnum, Queue<BaseBower>> keyValuePairs = new Dictionary<PowerTypeEnum, Queue<BaseBower>>();
}
