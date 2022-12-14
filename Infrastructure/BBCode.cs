using CodeKicker.BBCode.Core;
using System.Web;

namespace Beon.Infrastructure {
  public static class BBCode {
    //returns length of s if it doesnt contain t
    private static int MyIndexOf(string s, string t, int start = 0) {
      int res = s.IndexOf(t, start);
      return res == -1 ? s.Length : res;
    }
    public static string Parse(string text) {
      //BBCode Core's link generator doesnt work properly for some reason so we're rolling our own
      //need to prefix links with _ so that it doesnt try to generate links
      text = text.Replace("_", "._.");
      if (text.Length >= 4 && text.Substring(0, 4) == "http") {
        text = text.Remove(0, 4).Insert(0, "_http");
      }
      text = text
        .Replace(" http", " _http")
        .Replace("\nhttp", "\n_http")
        .Replace("]http", "]_http")
        .Replace("\u0009http", "\u0009_http");
      //undo prefixing within image tags
      text = text.Replace("[IMG]_", "[IMG]").Replace("[IMGSMALL]_", "[IMGSMALL]").Replace("[IMGLARGE]_", "[IMGLARGE]");

      text = parser.ToHtml(text);
      
      int i = 0;
      for (;;) {
        i = MyIndexOf(text, "_http");
        if (i == text.Length) {
          break;
        }
        int j = Math.Min(MyIndexOf(text, " ", i), MyIndexOf(text, "\n", i));
        j = Math.Min(j, MyIndexOf(text, "<", i));

        string uri = text.Substring(i+1, j-(i+1));
        if (Uri.IsWellFormedUriString(uri, UriKind.Absolute)) {
          string elem = "<a target=\"_blank\" href=" + uri + ">" + uri + "</a>";
          text = text.Remove(i, j-i).Insert(i, elem);
        }
        else {
          text = text.Remove(i, 5).Insert(i, "http");
        }
      }

      text = text.Replace("._.", "_");

      foreach (var s in Enumerable.Reverse(SmileButtons)) {
        text = text.Replace(HttpUtility.HtmlEncode(s.Text), $"<img class=\"smile\" src=\"{s.ImgPath}\" />");
      }
      return text;
    }
    private static BBCodeParser parser = new BBCodeParser(new []
    {
      new BBTag("B", "<b>", "</b>", 0),
      new BBTag("PRE", "<pre>", "</pre>", 1),
      new BBTag("CENTER", "<div style=\"text-align: center;\">", "</div>", 2),
      new BBTag("RIGHT", "<div style=\"text-align: right;\">", "</div>", 3),
      new BBTag("IMG", "<img class=\"image-medium\" src=\"${content}\" />", "", false, true, s => HttpUtility.HtmlEncode(s), 4, allowUrlProcessingAsText: false), 
      new BBTag("IMGSMALL", "<img class=\"image-small\" src=\"${content}\" />", "", false, true, s => HttpUtility.HtmlEncode(s), 5, allowUrlProcessingAsText: false), 
      new BBTag("IMGLARGE", "<img class=\"image-large\" src=\"${content}\" />", "", false, true, s => HttpUtility.HtmlEncode(s), 6, allowUrlProcessingAsText: false), 
      new BBTag("I", "<i>", "</i>", 7),
      new BBTag("U", "<u>", "</u>", 8)
    });

    public class BBTagButton {
      public string Text { get; set; }
      public string Title { get; set; }
      public string ImgPath { get; set; }
      public BBTagButton(string text, string title, string imgPath) {
        Text = text;
        Title = title;
        ImgPath = imgPath;
      }
    }
    
    public static IEnumerable<BBTagButton> TagButtons { get; private set; } = new List<BBTagButton>
    {
      new BBTagButton("B", "????????????", "/i/smiles/b.gif"),
      new BBTagButton("I", "????????????", "/i/smiles/i.gif"),
      new BBTagButton("U", "????????????????????????", "/i/smiles/u.gif"),
      new BBTagButton("CENTER", "???? ????????????", "/i/smiles/center.gif"),
      new BBTagButton("RIGHT", "???? ?????????????? ????????", "/i/smiles/right.gif"),
      new BBTagButton("IMG", "??????????????????????", "/i/smiles/image.gif")
    };

    public static IEnumerable<BBTagButton> SmileButtons { get; private set; } = new List<BBTagButton>
    {
      new BBTagButton(":-)", "????????????", "/i/smiles/smile.png"),
      new BBTagButton(":-(", "??????????????????????????", "/i/smiles/sad.png"),
      new BBTagButton(";-)", "????????????????????????", "/i/smiles/wink.png"),
      new BBTagButton(":-*", "????????????????????", "/i/smiles/kiss.png"),
      new BBTagButton(":-D", "????????????????", "/i/smiles/big-smile.png"),
      new BBTagButton(":-O", "??????????????????", "/i/smiles/surprised.png"),
      new BBTagButton(":-P", "???????????????????? ????????", "/i/smiles/tongue-sticking-out.png"),
      new BBTagButton("X-(", "????????????", "/i/smiles/angry.png"),
      new BBTagButton("]:-)", "????????????", "/i/smiles/devil.png"),
      new BBTagButton("O:-)", "??????????????????", "/i/smiles/angel.png"),
      new BBTagButton(":\'(", "??????????????", "/i/smiles/cry.png"),
      new BBTagButton(":-[", "??????????????????", "/i/smiles/upset.png"),
      new BBTagButton(":-\\", "????????????????", "/i/smiles/confused.png"),
      new BBTagButton(":-|", "??????????????????????????", "/i/smiles/undecided.png"),
      new BBTagButton(":-?", "????-??-??...", "/i/smiles/thinking.png"),
      new BBTagButton(";~)", "???????????? ????????????", "/i/smiles/cunning.png"),
      new BBTagButton("(:|", "??????????????????", "/i/smiles/tired.png"),
      new BBTagButton("8-}", "????????????????????????", "/i/smiles/crazy.png"),
      new BBTagButton(":-$", "????-??-??!", "/i/smiles/shhh.png"),
      new BBTagButton("8-|", "?? ?? ????????!", "/i/smiles/shocked.png"),
      new BBTagButton("B-)", "?? ??????????!", "/i/smiles/sun-glasses.png"),
      new BBTagButton(":^)", "????????????????????!", "/i/smiles/turn-red.png"),
      new BBTagButton("=^B", "??????????????!", "/i/smiles/thumbs-up.png"),
      new BBTagButton("=,B", "????????????", "/i/smiles/thumbs-down.png")
    };
  }
}

