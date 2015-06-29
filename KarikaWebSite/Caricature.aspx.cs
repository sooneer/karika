using SSS.Karika;
using SSS.Karika.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Caricature : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                KarikaturGetir(Convert.ToDecimal(Request.QueryString["id"]));
                lblSonFotografId.Text = Request.QueryString["id"].ToString();
            }
            else
            {
                KarikaturGetir(1);
            }
        }

    }
    private void KarikaturGetir(decimal p)
    {
        try
        {

            GetKarikaImages img = new GetKarikaImages();
            string KarikaPath = String.Format(@"/upload/images/{0}", KarikaBusiness.ConvertToImageName(p));
            KarikaImage Karika = img.GetKarikaImageFromFilePath(KarikaPath);
            if (Karika == null)
            {
                throw new Exception("Karikatür Bulunamadı.");
            }

            imgKarikatur.ImageUrl = KarikaPath;


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

}