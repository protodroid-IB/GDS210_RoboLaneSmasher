using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private BaseUnit unit;

    [SerializeField]
    private RectTransform healthBar;

	// Use this for initialization
	void Start ()
    {
        unit = GetComponent<BaseUnit>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newScale = new Vector3(unit.GetHealthRatio(), 1f, 1f);

        healthBar.localScale = newScale;
	}
}
