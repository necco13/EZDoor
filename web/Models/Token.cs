using System.Diagnostics;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace web.Models;

public class Token {
    [Key]
    int cifra;
    DateTime timestamp;
    public Token(){
        Random rnd = new Random();
        this.cifra = rnd.Next(1000,9999);
        this.timestamp = DateTime.Now.AddSeconds(30);
    }
    public void generateToken(){
        Random rnd = new Random();
        this.cifra = rnd.Next(1000,9999);
        this.timestamp = DateTime.Now.AddSeconds(30);
    }
    public bool IsValid(){
        if(DateTime.Now > this.timestamp)
            return false;
        return true;
    }
    public int GetCifra(){
        return this.cifra;
    }
    public DateTime GetTimestamp(){
        return this.timestamp;
    }
    override public string ToString(){
        return this.cifra + " " + this.timestamp;
    }
}
public class CifraCas {
    public int cifra;
    public int cas;

    public CifraCas(Token x){
        this.cifra = x.GetCifra();
        var tmp = x.GetTimestamp() - DateTime.Now;
        this.cas = tmp.Seconds;
    }
}
public class Veljavnost {
    public bool veljavnost;
    public Veljavnost(){    //dodano ce se tokena ne ujemata
        this.veljavnost = false;
    }
    public Veljavnost(Token x){
        this.veljavnost = x.IsValid();
    }
}
