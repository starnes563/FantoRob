using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapa : MonoBehaviour
{
    public List<AndarMapa> Andares = new List<AndarMapa>();
    public List<BauMapa> Baus = new List<BauMapa>();
    public Sprite BauAberto;
    public Text TextoTopo;
    public List<GameObject> BotoesAndares = new List<GameObject>();
    public AudioSource Source;
    public AudioClip SomConfirma;
    public int IDMapa;
    // Start is called before the first frame update
    void OnEnable()
    {
        foreach(AndarMapa a in Andares)
        {
            a.MeuMapa.SetActive(false);
        }
        if (StoryEvents.MapaDesafio[IDMapa])
        {
            for (int i = 0; i < Baus.Count; i++)
            {
                if (StoryEvents.DesafiosCamp[IDMapa].Interagiveis[Baus[i].ID] == true)
                {
                    Baus[i].MeuBau.sprite = BauAberto;
                }
            }
            TextoTopo.text = ManagerGame.Instance.Regiao.RegionName[ManagerGame.Instance.Idm];
            for (int i = 0; i < Andares.Count; i++)
            {
                BotoesAndares[i].SetActive(true);
                BotoesAndares[i].transform.GetChild(0).GetComponent<Text>().text = Andares[i].Nome[ManagerGame.Instance.Idm];
            }
        }
    }  
    public void AcionaBotao()
    {
        foreach(AndarMapa a in Andares)
        {
            a.MeuMapa.SetActive(false);
        }
        Source.PlayOneShot(SomConfirma);
    }
    void Update()
    {
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
        {
            gameObject.SetActive(false);
        }
    }
}
[System.Serializable]
public class AndarMapa
{
    public List <string> Nome = new List<string>();
    public GameObject MeuMapa;
}
[System.Serializable]
public class BauMapa
{
    public Image MeuBau;
    public int ID;
}