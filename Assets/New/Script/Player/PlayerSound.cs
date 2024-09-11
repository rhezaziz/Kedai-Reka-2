using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru{
    
    public class PlayerSound : MonoBehaviour
    {
        AudioSource audioWalk;
        public List<AudioClip> walk = new List<AudioClip>();
        // Start is called before the first frame update
        void Start()
        {
            audioWalk = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void playStep(){
            AudioClip clip = null;

            clip = walk[Random.Range(0, walk.Count - 1)];
            audioWalk.clip = clip;
            audioWalk.volume = Random.Range(.02f, .05f);
            audioWalk.pitch = Random.Range(0.75f, 1.25f); 

            audioWalk.Play();
        }
    }
}
