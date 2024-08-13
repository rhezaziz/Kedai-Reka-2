using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps_Controller : MonoBehaviour
{
    int lantai;
    int index;
    public Transform view;
    
    public List<Lantai> content = new List<Lantai>();

    public class Lantai
    {
        public int index;
        public string nama;
        public Transform lantai;
        public List<Ruangan> Ruangan = new List<Ruangan> ();
    }

    public class Ruangan
    {
        public string namaRuangan;
        public Transform ruangan;
    }

    public void changeLantai(int value)
    {
        currentIndex = 0;
        currentLantai += 1;
        view.position = content[currentLantai + value].lantai.position;

    }

    void disableContent()
    {

    }

    int currentLantai
    {
        get
        {
            return lantai;
        }

        set
        {
            lantai = value;

            if(lantai > content.Count - 1)
            {
                lantai = 0;
            }
            else if(lantai < 0)
            {
                lantai = content.Count - 1;
            }
        }
    }

    int currentIndex
    {
        get
        {
            return index;

        }

        set
        {
            index = value;

            if (index > content[currentLantai].Ruangan.Count - 1)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = content[currentLantai].Ruangan.Count - 1;
            }
        }
    }
}
