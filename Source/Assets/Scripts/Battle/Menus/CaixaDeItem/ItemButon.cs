using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemButon : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public Item MyItem;
    private BattleManager battleManager;
    public GameObject ImagemdeConfirmação;
    [HideInInspector]
    public bool PossoApertar = true;

     public void Confirmar()
    {
        if (PossoApertar)
        {
            
            SonsDoMenu.Desistir();
            GameObject confimacao = Instantiate(ImagemdeConfirmação, transform) as GameObject;
            //associa o botão
            confimacao.GetComponent<Confirmacao>().MyButton = this;
            PossoApertar = false;
        }       
    }
    public void GastarItem()
    {
        battleManager = GameObject.FindWithTag("Gerenciador").GetComponent<BattleManager>();
        

        switch(MyItem.Tipo)
        {
            case 0:
                battleManager.playerManager.RetiraSpy();
                break;
            case 1:
                battleManager.playerManager.RetiraKeylogger();
                break;
            case 2:
                battleManager.playerManager.RetiraTrojan();
                break;
            case 3:
                battleManager.playerManager.RetiraRanson();
                break;
            case 4:
                battleManager.playerManager.RetiraWorm();
                break;
            case 5:
                battleManager.playerManager.RetiraVirus();
                break;
            case 6:
                battleManager.playerManager.RecuperaIntegridade(MyItem.Fator);
                break;
            case 7:
                battleManager.playerManager.RecuperaBateria(MyItem.Fator);
                break;
            case 8:
                battleManager.playerManager.RecuperaIntegridade(MyItem.Fator/5);
                battleManager.playerManager.RecuperaBateria(MyItem.Fator);
                break;
            case 9:
                battleManager.playerManager.RetiraSpy();
                battleManager.playerManager.RetiraKeylogger();
                battleManager.playerManager.RetiraTrojan();
                battleManager.playerManager.RetiraRanson();
                battleManager.playerManager.RetiraWorm();
                battleManager.playerManager.RetiraVirus();
                break;

        }

        // atualiza quantidade de itens.
        battleManager.playerManager.DiminuiAcoes(MyItem.GastoAcoes);
        MyItem.GastarItem(1);
        transform.GetChild(2).GetComponent<Text>().text = MyItem.Quantidade.ToString();
        if (MyItem.Quantidade ==0)
        {
            Destruir();
        }

    }
    public void Destruir()
    {
        Destroy(this.gameObject);
    }

}
