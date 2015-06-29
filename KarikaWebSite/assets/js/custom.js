$(document).ready(function () {
    var sonId = $("#lblSonFotografId").html();
    var idKarika = 0;
    sonId = parseInt(sonId, 10);

    $(".fb-like").attr("data-href", "/Caricature.aspx?id=" + sonId);

    function dosyaAdiGetir(id) {
        var dosyaAdi;
        if (id > 9999) {
            dosyaAdi = id.toString() + ".jpg";
        }
        else {
            if (id > 999) {
                dosyaAdi = "0" + id.toString() + ".jpg";
            }
            else {
                if (id > 99) {
                    dosyaAdi = "00" + id.toString() + ".jpg";
                }
                else {
                    if (id > 9) {
                        dosyaAdi = "000" + id.toString() + ".jpg";
                    }
                    else {
                        if (id > 0) {
                            dosyaAdi = "0000" + id.toString() + ".jpg";
                        }
                    }
                }
            }
        }
        return dosyaAdi;
    }

    function SagEkleme(lastIdd) {
        if (lastIdd < 1) {
            return false;
        }

        $("ul.pagination li a.karikatur").first().parent().remove();
        $("ul.pagination li a.karikatur").last().parent().after("<li><a href=\"javascript:;\" data-id=\"" + lastIdd.toString() + "\" data-adi=\"" + dosyaAdiGetir(lastIdd) + "\" class=\"karikatur\">" + lastIdd + "</a></li>");
    }

    function SolEkleme(lastIdd) {
        if (lastIdd > sonId) {
            return false;
        }
        $("ul.pagination li a.karikatur").last().parent().remove();
        $("ul.pagination li a.karikatur").first().parent().before("<li><a href=\"javascript:;\" data-id=\"" + lastIdd.toString() + "\" data-adi=\"" + dosyaAdiGetir(lastIdd) + "\" class=\"karikatur\">" + lastIdd + "</a></li>");
    }

    function butonKontrolu(fotoId) {
        // Tüm pasif düğmeleri kaldırıyoruz.
        $("ul.pagination li").first().removeAttr("class");
        $("ul.pagination li a").first().removeAttr("disabled");
        $("ul.pagination li").last().removeAttr("class");
        $("ul.pagination li a").last().removeAttr("disabled");

        // Eğer DB'deki son fotoId ile sonId-1 aynı ise
        if (fotoId == sonId) {
            $("ul.pagination li").first().attr("class", "disabled");
            $("ul.pagination li").first().find("a").attr("disabled", "disabled");
        }
        else if (fotoId == 1) {
            $("ul.pagination li").last().attr("class", "disabled");
            $("ul.pagination li a").last().attr("disabled", "disabled");
        }
    }

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

    $(".img-bg").mousewheel(function (event, delta) {
        if (delta > 0) {
            $(".btnSonraki").click();
        }
        else {
            $(".btnOnceki").click();
        }
        return false;
    });

    $(document).keydown(function (e) {
        switch (e.which) {
            case 37: // left
                $(".btnSonraki").click();
                break;

            case 38: // up
                $(".btnSonraki").click();
                break;

            case 39: // right
                $(".btnOnceki").click();
                break;

            case 40: // down
                $(".btnOnceki").click();
                break;

            default: return; // exit this handler for other keys
        }
        e.preventDefault(); // prevent the default action (scroll / move caret)
    });

    $(".karikatur").first().parent().attr("class", "active");

    $("#imgKarikatur").attr("src", "/upload/images/" + $(".karikatur").first().attr("data-adi"));
    $("#imgKarikatur").attr("data-adi", $(".karikatur").first().attr("data-adi"));

    function karikaturGetir(idFoto) {
        $(".karikatur").parent().removeClass("active");

        $(".karikatur[data-id=" + idFoto + "]").parent().addClass("active");

        var imgAdresi = "/upload/images/" + dosyaAdiGetir(idFoto);

        butonKontrolu(idFoto);

        KarikaturBilgisiGetir(idFoto);

        $("#imgKarikatur").attr("src", imgAdresi);
        $("#imgKarikatur").attr("data-adi", dosyaAdiGetir(idFoto));
    }

    $(document).on('click', '.karikatur', function () {
        var id = $(this).attr("data-id");
        id = parseInt(id, 10);
        karikaturGetir(id);
    });

    $(".btnOnceki").click(function () {
        if ($(this).attr("disabled") == "disabled") {
            return false;
        }

        // Sayfadaki minimum id hesaplanır.
        var minId = $("ul.pagination li a.karikatur").last().attr("data-id");
        minId = parseInt(minId, 10);
        // Sayfadaki aktif butonun id si bulunur.
        var id = $("ul.pagination li.active a").attr("data-id");
        id = parseInt(id, 10);

        if (id == minId) {
            SagEkleme(id - 1);
        }
        id = id - 1;

        idKarika = id;

        if (id == 0) {
            return false;
        }

        var dosyaAdi = dosyaAdiGetir(id);
        $("#sayfalama .active").next().addClass("active");
        $("#sayfalama .active").first().removeClass("active");
        butonKontrolu(id);
        KarikaturBilgisiGetir(id);
        $("#imgKarikatur").attr("src", "/upload/images/" + dosyaAdi);
        $("#imgKarikatur").attr("data-adi", dosyaAdi);
    });

    $(".btnSonraki").click(function () {
        if ($(this).attr("disabled") == "disabled") {
            return false;
        }
        // Sayfadaki maks id hesaplanır.
        var maksId = $("ul.pagination li a.karikatur").first().attr("data-id");
        maksId = parseInt(maksId, 10);

        // Sayfadaki aktif butonun id si bulunur.
        var id = $("ul.pagination li.active a").attr("data-id");
        id = parseInt(id, 10);

        if (id == maksId) {
            SolEkleme(id + 1);
        }
        id = id + 1;
        idKarika = id;
        var dosyaAdi = dosyaAdiGetir(id);
        $("#sayfalama .active").prev().addClass("active");
        $("#sayfalama .active").last().removeClass("active");
        butonKontrolu(id);
        KarikaturBilgisiGetir(id);
        $("#imgKarikatur").attr("src", "/upload/images/" + dosyaAdi);
        $("#imgKarikatur").attr("data-adi", dosyaAdi);
    });

    var Xcord = 0;

    $('.img-bg').bind('contextmenu', function (e) {
        return false;
    });

    oncontextmenu = "return false;"

    $(document).on('mousedown', '.img-bg', function (e) {
        //Xcord = event.pageX;

        if (e.button == 2) {
            $(".btnOnceki").click();
        }
        else {
            $(".btnSonraki").click();
        }
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
});