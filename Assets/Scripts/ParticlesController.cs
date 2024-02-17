using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    //Used to control several particle systems at once + audio
    public ParticleSystem[] particles;
    public AudioSource soundEffect;

    public void Play(bool muteAudio = false){
        foreach(ParticleSystem ps in particles){
            if(ps)
                ps.Play();
        }
        if(soundEffect && !muteAudio)
            soundEffect.Play();

    }
    public void Stop(){
        foreach(ParticleSystem ps in particles){
            if(ps)
                ps.Stop();
        }
        if(soundEffect)
            soundEffect.Stop();
    }
}
