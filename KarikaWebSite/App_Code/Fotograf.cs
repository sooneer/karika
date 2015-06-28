using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

public class Fotograf
{
    private string _fotografAdi;
    public string FotografAdi
    {
        get
        { return _fotografAdi; }
        set
        { _fotografAdi = value; }
    }

    private string _fotografYolu;
    public string FotografYolu
    {
        get
        { return _fotografYolu; }
        set
        { _fotografYolu = value; }
    }

    private string _fotografTuru;
    public string FotografTuru
    {
        get { return _fotografTuru; }
        set { _fotografTuru = value; }
    }

    private decimal _fotografBoyutu;
    public decimal FotografBoyutu
    {
        get { return _fotografBoyutu; }
        set { _fotografBoyutu = value; }
    }

    private decimal _idFotograf;
    public decimal IdFotograf
    {
        get { return _idFotograf; }
        set { _idFotograf = value; }
    }
}

public static class DizindekiFotograflar
{
    public static decimal FotografAdeti;

    public static string FotografGetir(string albumYolu)
    {
        string yol = albumYolu + "/";
        yol = yol.Replace("~", "");
        albumYolu = HttpContext.Current.Server.MapPath(albumYolu);

        Fotograf r = new Fotograf();
        if (Directory.Exists(albumYolu))
        {
            FileInfo[] resimler = new DirectoryInfo(albumYolu).GetFiles();
            foreach (FileInfo resim in resimler)
            {
                if (resim.Extension.Replace(".", "").ToUpper() == "JPG" || resim.Extension.Replace(".", "").ToUpper() == "PNG")
                {
                    r.FotografAdi = resim.Name;
                    r.FotografYolu = yol + resim.Name;
                    r.FotografTuru = resim.Extension.Replace(".", "").ToUpper();
                    r.FotografBoyutu = resim.Length;
                    r.IdFotograf = Convert.ToDecimal(resim.Name.Substring(10, 15));
                    break;
                }
            }
        }

        return r.FotografYolu;
    }

    public static List<Fotograf> FotograflariGetir(string albumYolu)
    {
        string yol = albumYolu + "/";
        yol = yol.Replace("~", "");
        albumYolu = HttpContext.Current.Server.MapPath(albumYolu);

        List<Fotograf> colResim = new List<Fotograf>();
        if (Directory.Exists(albumYolu))
        {

            FileInfo[] resimler = new DirectoryInfo(albumYolu).GetFiles();

            foreach (FileInfo resim in resimler)
            {
                if (resim.Extension.Replace(".", "").ToUpper() == "JPG" || resim.Extension.Replace(".", "").ToUpper() == "PNG")
                {
                    try
                    {
                        Fotograf r = new Fotograf();
                        r.FotografAdi = resim.Name;
                        r.FotografYolu = yol + resim.Name;
                        r.FotografTuru = resim.Extension.Replace(".", "").ToUpper();
                        r.FotografBoyutu = resim.Length;
                        r.IdFotograf = Convert.ToDecimal(resim.Name.Substring(0, 5));
                        colResim.Add(r);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }
            }

            FotografAdeti = colResim.Count;

            colResim = colResim.OrderByDescending(k => k.IdFotograf).Take(6).ToList();

        }

        return colResim;
    }

    public static List<Fotograf> FotograflariGetir(string albumYolu, Decimal fotoId)
    {
        Decimal adet = 0;
        if (fotoId < 7)
        {
            adet = 6;
        }

        string yol = albumYolu + "/";
        yol = yol.Replace("~", "");
        albumYolu = HttpContext.Current.Server.MapPath(albumYolu);

        List<Fotograf> colResim = new List<Fotograf>();
        if (Directory.Exists(albumYolu))
        {

            FileInfo[] resimler = new DirectoryInfo(albumYolu).GetFiles();

            Decimal tmp = 0;
            foreach (FileInfo resim in resimler)
            {
                tmp++;
                if (resim.Extension.Replace(".", "").ToUpper() == "JPG" || resim.Extension.Replace(".", "").ToUpper() == "PNG")
                {
                    try
                    {
                        if (tmp <= adet)
                        {
                            Fotograf r = new Fotograf();
                            r.FotografAdi = resim.Name;
                            r.FotografYolu = yol + resim.Name;
                            r.FotografTuru = resim.Extension.Replace(".", "").ToUpper();
                            r.FotografBoyutu = resim.Length;
                            r.IdFotograf = Convert.ToDecimal(resim.Name.Substring(10, 5));
                            colResim.Add(r);
                        }

                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }
            }

            colResim = colResim.OrderByDescending(k => k.IdFotograf).Take(6).ToList();

        }

        return colResim;
    }

    public static Decimal KarikaturSayisi(string albumYolu)
    {
        string yol = albumYolu + "/";
        yol = yol.Replace("~", "");
        albumYolu = HttpContext.Current.Server.MapPath(albumYolu);

        List<Fotograf> colResim = new List<Fotograf>();
        if (Directory.Exists(albumYolu))
        {

            FileInfo[] resimler = new DirectoryInfo(albumYolu).GetFiles();

            foreach (FileInfo resim in resimler)
            {
                if (resim.Extension.Replace(".", "").ToUpper() == "JPG" || resim.Extension.Replace(".", "").ToUpper() == "PNG")
                {
                    try
                    {
                        Fotograf r = new Fotograf();
                        r.FotografAdi = resim.Name;
                        r.FotografYolu = yol + resim.Name;
                        r.FotografTuru = resim.Extension.Replace(".", "").ToUpper();
                        r.FotografBoyutu = resim.Length;
                        r.IdFotograf = Convert.ToDecimal(resim.Name.Substring(10, 5));
                        colResim.Add(r);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }
            }
        }

        return colResim.Count;
    }

}