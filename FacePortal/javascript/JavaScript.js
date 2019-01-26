function __weatherwidget_init() {
    var t = document.getElementsByClassName("weatherwidget-io"),
        e = [];
    if (0 !== t.length) {
        for (var o = 0, a = Math.min(t.length, 10) ; o < a; o++) {
            var i;
            ! function (o) {
                var a = t[o],
                    d = {};
                d.id = "weatherwidget-io-" + o, d.href = a.href, d.label_1 = a.getAttribute("data-label_1"), d.label_2 = a.getAttribute("data-label_2"), d.font = a.getAttribute("data-font"), d.icons = a.getAttribute("data-icons"), d.mode = a.getAttribute("data-mode"), d.days = a.getAttribute("data-days"), d.theme = a.getAttribute("data-theme"), d.baseColor = a.getAttribute("#99FF33"), d.accent = a.getAttribute("data-accent"), d.textColor = a.getAttribute("data-textColor"), d.textAccent = a.getAttribute("data-textAccent"), d.highColor = a.getAttribute("data-highColor"), d.lowColor = a.getAttribute("data-lowColor"), d.sunColor = a.getAttribute("data-sunColor"), d.moonColor = a.getAttribute("data-moonColor"), d.cloudColor = a.getAttribute("data-cloudColor"), d.cloudFill = a.getAttribute("data-cloudFill"), d.rainColor = a.getAttribute("data-rainColor"), d.snowColor = a.getAttribute("data-snowColor"), d.windColor = a.getAttribute("data-windColor"), d.fogColor = a.getAttribute("data-fogColor"), d.hailColor = a.getAttribute("data-hailColor"), d.daysColor = a.getAttribute("data-daysColor"), d.tempColor = a.getAttribute("data-tempColor"), d.descColor = a.getAttribute("data-descColor"), d.label1Color = a.getAttribute("data-label1Color"), d.label2Color = a.getAttribute("data-label2Color"), d.shadow = a.getAttribute("data-shadow"), (i = document.getElementById(d.id)) && a.removeChild(i), e[d.id] = document.createElement("iframe"), e[d.id].setAttribute("id", d.id), e[d.id].setAttribute("class", "weatherwidget-io-frame"), e[d.id].setAttribute("scrolling", "no"), e[d.id].setAttribute("frameBorder", "0"), e[d.id].setAttribute("width", "100%"), e[d.id].setAttribute("src", "https://weatherwidget.io/w/"), e[d.id].style.display = "block", e[d.id].style.top = "0", e[d.id].onload = function () {
                    e[d.id].contentWindow.postMessage(d, "https://weatherwidget.io")
                }, a.style.display = "block", a.style.height = "150px", a.style.padding = "0", a.style.overflow = "hidden", a.style.textAlign = "left", a.style.textIndent = "-299px", a.appendChild(e[d.id])
            }(o)
        }
        window.addEventListener("message", function (t) {
            "https://weatherwidget.io" === t.origin && e[t.data.wwId] && e[t.data.wwId].parentNode && (e[t.data.wwId].style.height = t.data.wwHeight + "px", e[t.data.wwId].parentNode.style.height = t.data.wwHeight + "px")
        })
    }
}
__weatherwidget_init();