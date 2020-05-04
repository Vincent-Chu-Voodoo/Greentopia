using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomSFX : MonoBehaviour
{
    public AudioSource mergeAS;
    public List<AudioClip> mergeAudioClipList;

    public void OnCombineAtom(object atomObj)
    {
        OnCombineAtom(atomObj as Atom);
    }

    public void OnCombineAtom(Atom atom)
    {
        mergeAS.PlayOneShot(mergeAudioClipList[atom.atomLevel]);
    }
}
