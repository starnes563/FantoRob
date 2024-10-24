using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void Save()
    {
        CaixaDeSalvamento.Instancia.Salvando();
        DadosJogador data = new DadosJogador();
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + ManagerGame.Instance.SavePath[ManagerGame.Instance.ActualSavePath];
        FileStream stream = new FileStream(path,FileMode.Create);
        formater.Serialize(stream, data);
        stream.Close();
        CaixaDeSalvamento.Instancia.Salvo();
    }
    public static DadosJogador Load()
    {       
        string path = Application.persistentDataPath + ManagerGame.Instance.SavePath[ManagerGame.Instance.ActualSavePath];
        if(File.Exists(path))
        {           
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DadosJogador data = formater.Deserialize(stream) as DadosJogador;            
            stream.Close();
            return data;          
        }
        else
        {
            Debug.Log("Não Deu para Carregar");
            return null;
        }        
    }
    public static DadosJogador ListaSalvo(int i)
    {
        string path = Application.persistentDataPath + ManagerGame.Instance.SavePath[i];
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DadosJogador data = formater.Deserialize(stream) as DadosJogador;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
