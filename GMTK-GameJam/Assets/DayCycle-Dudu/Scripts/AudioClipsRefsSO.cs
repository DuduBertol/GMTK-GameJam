using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipsRefsSO : ScriptableObject
{
    public AudioClip dropGetItem; //
    public AudioClip spawnEgg; // 
    public AudioClip sellEgg; //
    public AudioClip beanTreeGrow; //
    public AudioClip coin; //
    public AudioClip nightBecome; //
    public AudioClip joaozinhoDeath; //
    public AudioClip hit; //
    public AudioClip beanTreeDamage; //Inimigo atacando
    public AudioClip beanTreeBreak; //Pé de feijão quebrando
    public AudioClip[] beanTreeFootstep; //Colisão do player na folha do pé de feijão
    public AudioClip[] villagerFootstep; // Passos dos villagers
    public AudioClip[] giantFootstep; // Passos do Gigante
}
