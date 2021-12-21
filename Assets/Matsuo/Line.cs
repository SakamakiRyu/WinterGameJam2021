using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
	[SerializeField] GameObject cube_1;
	[SerializeField] GameObject cube_2;


	void Update()
	{

		LineRenderer lineY = GetComponent<LineRenderer>();

		lineY.SetPosition(0, cube_1.transform.position);
		lineY.SetPosition(1, cube_2.transform.position);
	}
}
