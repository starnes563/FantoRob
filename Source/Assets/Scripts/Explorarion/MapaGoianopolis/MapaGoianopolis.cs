using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapaGoianopolis : MonoBehaviour
{
    public Text[] OndeEstou = new Text[2];    
    public List<GameObject> LocalNeftari = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        mostrarOndeEstou();
    }
    void OnEnable()
    {
        NaoExibir();
        this.transform.position = new Vector3(this.transform.position.x, -7.23f);
        LeanTween.moveLocalY(this.gameObject, 0f, 0.7f);
    }   
    void mostrarOndeEstou()
    {
        foreach (GameObject g in LocalNeftari) { g.SetActive(false); }
        //0-fazendinha
        //1-bairro residencial
        //2-centro
        //3-fazenda dagua
        //4-castelo
        //5-estrada parque
        //6-ginasio
        //7-parque
        //8-centro entreterimento
        //9-barco
        //10-setor mansoes
        //11-mansaoabandonada
        //12-quadradao/circo       
        //13-minas
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 8:
                LocalNeftari[0].SetActive(true);
                break;
            case 7:
                LocalNeftari[1].SetActive(true);
                break;
            case 9:
                LocalNeftari[2].SetActive(true);
                break;
            case 10:
                LocalNeftari[3].SetActive(true);
                break;
            case 11:
                LocalNeftari[4].SetActive(true);
                break;
            case 12:
                LocalNeftari[5].SetActive(true);
                break;
            case 13:
                LocalNeftari[6].SetActive(true);
                break;
            case 14:
                LocalNeftari[7].SetActive(true);
                break;
            case 93:
                LocalNeftari[8].SetActive(true);
                break;
            case 16:
                LocalNeftari[9].SetActive(true);
                break;
            case 23:
                LocalNeftari[10].SetActive(true);
                break;
            case 18:
                LocalNeftari[11].SetActive(true);
                break;
            case 17:
                LocalNeftari[12].SetActive(true);
                break;
            case 19:
                LocalNeftari[12].SetActive(true);
                break;
            case 15:
                LocalNeftari[13].SetActive(true);
                break;
            case 20:
                LocalNeftari[2].SetActive(true);
                break;
            case 21:
                LocalNeftari[2].SetActive(true);
                break;
            case 22:
                LocalNeftari[2].SetActive(true);
                break;
        }

    }
    void Update()
    {
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
        {
            Fechar();
        }
    }
    public void ExibirBotao(string nomeregiao)
    {
        OndeEstou[0].text = nomeregiao;
        OndeEstou[1].text = nomeregiao;        
    }
    public void NaoExibir()
    {
        OndeEstou[0].text = ManagerGame.Instance.Regiao.RegionName[ManagerGame.Instance.Idm];
        OndeEstou[1].text = ManagerGame.Instance.Regiao.RegionName[ManagerGame.Instance.Idm];       
    }
    public void Fechar()
    {
        SonsMenu.Desistir();
        this.gameObject.SetActive(false);
    }
}
