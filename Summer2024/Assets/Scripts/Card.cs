using System.Linq;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool Active {get; set;}
    public bool UsedAbility {get; set;}
    [field:SerializeField]
    public CardSO CardSO {get; set;}
}