using UnityEngine;
using System.Collections;

public class StarParticleSystem : MonoBehaviour {

	public int testSize;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}





	ParticleSystem m_System;
	ParticleSystem.Particle[] m_Particles;

	void LateUpdate() {

		InitializeIfNeeded();

		// GetParticles is allocation free because we reuse the m_Particles buffer between updates
		var numParticlesAlive = m_System.GetParticles(m_Particles);

		// Change only the particles that are alive
		for (var i = 0; i < numParticlesAlive; i++) {
			m_Particles[i].size =testSize;
		}
		// Apply the particle changes to the particle system
		m_System.SetParticles(m_Particles, numParticlesAlive);
	}

	void InitializeIfNeeded() {
		if (m_System == null)
			m_System = GetComponent<ParticleSystem>();

		if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
			m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
	}

}
