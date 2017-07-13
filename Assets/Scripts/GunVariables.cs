using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVariables : MonoBehaviour {

	[Header("Stats")]
	[Range(0f, 100f)] public float accuracy = 75f;
	[Header("Effects")]
	public GameObject hitParticle;
	public GameObject shellsParticle;
	public GameObject muzzleFlash;
	public GameObject bullet;

}
