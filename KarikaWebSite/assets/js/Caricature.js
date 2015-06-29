$(document).ready(function () {
    var sonId = $("#lblSonFotografId").html();
    sonId = parseInt(sonId, 10);

    $(".fb-like").attr("data-href", "/Caricature.aspx?id=" + sonId);

    function aciklamaDuzenle(text) {
        var tmpText = "";
        var tmpOtherText = "";

        var str = text;
        if (text == null) {
            return "";
        }
        else if (text.trim() == "") {
            return "";
        }

        var words = str.split(" ");
        var tmp = 0;
        for (var i = 0; i < words.length; i++) {
            if (i < 20) {
                tmpText += words[i] + " ";
            }
            else {
                tmp = 1;
                tmpOtherText += words[i] + " ";
            }
        }
        var html = "<p>";
        html += tmpText;
        if (tmp == 1) {
            html += "<span id=\"devaminiOku\">Devamını Oku</span>";
            html += "<span class=\"aciklamaDevami\">";
            html += tmpOtherText;
            html += "</span>";
            html += "<span id=\"aciklamaGizle\">Açıklamayı Daralt</span>";
        }

        html += "</p>";

        return html;

        //        <p></p>
        //<span id="devaminiOku">Devamını oku.</span>
        //<div class="aciklamaDevami">
        //Bu karikatürde doğa manzaralı güneşin ışıltısında, açık bir alanda yeşil çimenlerle çevrili bir yerde masa ve üzerinde muzip gülücük dağıtan bir tavuk yatış pozisyonda duruyor. İnek olan arkadaşının fotoğraf çekmesini istiyor. Ve tavuk muzip muzip diyor ki "Açıyı öyle bir ayarla ki güneşi ben yumurtluyormuşum gibi çıksın." :)</div>
        //<span id="aciklamaGizle">Açıklama daralt</span>
    }

    $(document).on('click', '#btnIndir', function (event) {
        var adres = $("#imgKarikatur").attr("src");
        $("#btnIndir").attr("href", adres);
    });

    $(document).on('click', '#devaminiOku', function (event) {
        $(".aciklamaDevami").css("display", "inline");
        $("#devaminiOku").css("display", "none");
        $("#aciklamaGizle").css("display", "inline");
    });

    $(document).on('click', '#aciklamaGizle', function (event) {
        $(".aciklamaDevami").css("display", "none");
        $("#devaminiOku").css("display", "inline");
        $("#aciklamaGizle").css("display", "none");
    });

    $.date = function (dateObject) {
        var d = new Date(parseInt(dateObject.substr(6)));
        var second = d.getSeconds();
        if (second < 10) {
            second = "0" + second;
        }
        var minute = d.getMinutes();
        if (minute < 10) {
            minute = "0" + minute;
        }

        var hour = d.getHours();
        if (hour < 10) {
            hour = "0" + hour;
        }
        var day = d.getDate();
        var month = d.getMonth() + 1;
        var year = d.getFullYear();
        if (day < 10) {
            day = "0" + day;
        }
        if (month < 10) {
            month = "0" + month;
        }
        var date = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second + "Z";
        return date;
    };

    function KarikaturBilgisiGetir(idKarikatur) {
        idKarika = parseInt(idKarikatur, 10);

        $.ajax({
            type: "POST",
            url: "/default.aspx/KarikaturBilgisiGetir",
            data: "{'idKarikatur':'" + idKarikatur + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d == null) {
                    $("#karikaturBaslik").text("");
                    $("#karikaturAciklama").html("");

                    $("#karikaturEklenmeTarihi").text("");
                    $("#karikaturEklenmeTarihi").attr("title", "");
                    return false;
                }

                var Karikatur = response.d;
                $("#karikaturBaslik").text(Karikatur.Subject);
                $("#karikaturAciklama").html(aciklamaDuzenle(Karikatur.Description));

                $("#karikaturEklenmeTarihi").text($.date(Karikatur.CreateDate));
                $("#karikaturEklenmeTarihi").attr("title", $.date(Karikatur.CreateDate));

                $("#karikaturEklenmeTarihi").timeago();
            },
            failure: function (msg) {
                alert(msg);
            }
        });
    }

    KarikaturBilgisiGetir(sonId);

});