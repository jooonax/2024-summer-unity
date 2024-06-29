using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Xml.Serialization;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [field:SerializeField]
    public TMPro.TextMeshProUGUI WoodAmount { get; private set; }

    [field:SerializeField]
    public TMPro.TextMeshProUGUI StoneAmount { get; private set; }

    [field:SerializeField]
    public TMPro.TextMeshProUGUI PeopleAmount { get; private set; }

    [field:SerializeField]
    public TMPro.TextMeshProUGUI VPAmount { get; private set; }

}
