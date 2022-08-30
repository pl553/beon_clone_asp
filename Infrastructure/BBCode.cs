using CodeKicker.BBCode.Core;
using System.Web;

namespace Beon.Infrastructure {
  public static class BBCode {
    public static string Parse(string text) {
      text = parser.ToHtml(text);
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
      new BBTagButton("B", "Жирный", "/i/smiles/b.gif"),
      new BBTagButton("I", "Курсив", "/i/smiles/i.gif"),
      new BBTagButton("U", "Подчёркнутый", "/i/smiles/u.gif"),
      new BBTagButton("CENTER", "По центру", "/i/smiles/center.gif"),
      new BBTagButton("RIGHT", "По правому краю", "/i/smiles/right.gif"),
      new BBTagButton("IMG", "Изображение", "/i/smiles/image.gif")
    };

    public static IEnumerable<BBTagButton> SmileButtons { get; private set; } = new List<BBTagButton>
    {
      new BBTagButton(":-)", "улыбка", "/i/smiles/smile.png"),
      new BBTagButton(":-(", "разочарование", "/i/smiles/sad.png"),
      new BBTagButton(";-)", "подмигивание", "/i/smiles/wink.png"),
      new BBTagButton(":-*", "поцелуйчик", "/i/smiles/kiss.png"),
      new BBTagButton(":-D", "смеяться", "/i/smiles/big-smile.png"),
      new BBTagButton(":-O", "удивление", "/i/smiles/surprised.png"),
      new BBTagButton(":-P", "показывать язык", "/i/smiles/tongue-sticking-out.png"),
      new BBTagButton("X-(", "злость", "/i/smiles/angry.png"),
      new BBTagButton("]:-)", "чёртик", "/i/smiles/devil.png"),
      new BBTagButton("O:-)", "ангелочек", "/i/smiles/angel.png"),
      new BBTagButton(":\'(", "плакать", "/i/smiles/cry.png"),
      new BBTagButton(":-[", "огорчение", "/i/smiles/upset.png"),
      new BBTagButton(":-\\", "смущение", "/i/smiles/confused.png"),
      new BBTagButton(":-|", "неуверенность", "/i/smiles/undecided.png"),
      new BBTagButton(":-?", "хм-м-м...", "/i/smiles/thinking.png"),
      new BBTagButton(";~)", "хитрая улыбка", "/i/smiles/cunning.png"),
      new BBTagButton("(:|", "усталость", "/i/smiles/tired.png"),
      new BBTagButton("8-}", "сумасшествие", "/i/smiles/crazy.png"),
      new BBTagButton(":-$", "тц-ц-ц!", "/i/smiles/shhh.png"),
      new BBTagButton("8-|", "я в шоке!", "/i/smiles/shocked.png"),
      new BBTagButton("B-)", "в очках!", "/i/smiles/sun-glasses.png"),
      new BBTagButton(":^)", "покраснеть!", "/i/smiles/turn-red.png"),
      new BBTagButton("=^B", "классно!", "/i/smiles/thumbs-up.png"),
      new BBTagButton("=,B", "отстой", "/i/smiles/thumbs-down.png")
    };
  }
}

