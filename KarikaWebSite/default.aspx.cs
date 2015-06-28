using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using SSS.Karika.KarikaBusiness;
using SSS.Karika;

public partial class _default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["no"] != null)
            //{
            //    KarikaturGetir(Convert.ToDecimal(Request.QueryString["no"]));
            //}
            //else
            //{
            initPage();
            //}
        }
    }

    private void KarikaturGetir(decimal p)
    {
        try
        {

            GetKarikaImages img = new GetKarikaImages();
            string KarikaPath = String.Format(@"/upload/images/{0}", ConvertToImageName(p));
            KarikaImage Karika = img.GetKarikaImageFromFilePath(KarikaPath);
            if (Karika == null)
            {
                throw new Exception("Karikatür Bulunamadı.");
            }


            //GN_KARIKATUR kar = new GN_KARIKATUR(); ;

            //using (var Ent = new KarikaturEntity())
            //{
            //    kar = Ent.GN_KARIKATUR.Where(k => k.KARIKATUR_NO == p).FirstOrDefault();
            //}

            //imgKarikatur.Src = "/upload/images/" + kar.DOSYA_ADI;
            //imgKarikatur.Attributes["data-adi"] = kar.DOSYA_ADI;

            //List<Fotograf> lstFoto = new List<Fotograf>();

            //lstFoto = DizindekiFotograflar.FotograflariGetir("~/upload/images/", kar.KARIKATUR_NO);

            //lblSonFotografId.Text = DizindekiFotograflar.KarikaturSayisi("~/upload/images/").ToString();
            //lstSayfalama.DataSource = lstFoto;
            //lstSayfalama.DataBind();



        }
        catch (Exception)
        {

        }
    }

    private static string ConvertToImageName(decimal id)
    {
        String result = string.Empty;

        if (id > 9999)
        {
            result = id.ToString() + ".jpg";
        }
        else
        {
            if (id > 999)
            {
                result = "0" + id.ToString() + ".jpg";
            }
            else
            {
                if (id > 99)
                {
                    result = "00" + id.ToString() + ".jpg";
                }
                else
                {
                    if (id > 9)
                    {
                        result = "000" + id.ToString() + ".jpg";
                    }
                    else
                    {
                        if (id > 0)
                        {
                            result = "0000" + id.ToString() + ".jpg";
                        }
                    }
                }
            }
        }

        return result;

    }

    private void initPage()
    {
        try
        {
            List<Fotograf> lstFoto = new List<Fotograf>();
            lstFoto = DizindekiFotograflar.FotograflariGetir("~/upload/images/");
            lblSonFotografId.Text = DizindekiFotograflar.FotografAdeti.ToString();
            lstSayfalama.DataSource = lstFoto;
            lstSayfalama.DataBind();
        }
        catch (Exception)
        {

        }
    }

    [WebMethod]
    public static KarikaImage KarikaturBilgisiGetir(string idKarikatur)
    {
        KarikaImage Karika = new KarikaImage();
        try
        {
            GetKarikaImages img = new GetKarikaImages();
            Decimal id = Convert.ToDecimal(idKarikatur);
            string KarikaPath = String.Format(@"/upload/images/{0}", ConvertToImageName(id));
            Karika = img.GetKarikaImageFromFilePath(KarikaPath);
            if (Karika == null)
            {
                throw new Exception("Karikatür Bulunamadı.");
            }

        }
        catch (Exception ex)
        {
            return null;
            throw new Exception("Tanımsız bir hata oluştu. Hata Detayı : " + ex.Message);
        }

        return Karika;
    }

    public static string ConvertToDate(DateTime dt)
    {
        string result = string.Empty;

        result = dt.ToShortDateString() + "T" + dt.ToShortTimeString() + "Z";

        //2008-07-17T09:24:17Z

        return result;

    }

}