using UnityEngine;
using System.Collections.Generic;

public class BeamController : MonoBehaviour {
	public GameObject[] beams;
	public ParticleSystem[] emitters;

	Dictionary<ParticleSystem, float> emissionRates;

	void Start() {
		emissionRates = new Dictionary<ParticleSystem, float>();
	}

	public void Toggle() {
		//gameObject.SetActive(!gameObject.activeSelf);
		foreach (var beam in beams) {
			beam.SetActive(!beam.activeSelf);
		}

		foreach (var emitter in emitters) {
			if (!emissionRates.ContainsKey(emitter)) {
				emissionRates.Add(emitter, emitter.emission.rateOverDistanceMultiplier);
			}

			float oldEmissionRate;
			emissionRates.TryGetValue(emitter, out oldEmissionRate);
			// emitter.emission.rateOverTime = emitter.emission.rateOverDistanceMultiplier == 0.0f ? oldEmissionRate : 0.0f;
		}
	}
}
