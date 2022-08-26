using CodeKicker.BBCode.Core;

namespace Beon.Infrastructure {
  public static class BBCode {
    public static readonly BBCodeParser parser = new BBCodeParser(new []
    {
      new BBTag("B", "<b>", "</b>", 0),
      new BBTag("PRE", "<pre>", "</pre>", 1),
      new BBTag("CENTER", "<div style=\"text-align: center;\">", "</div>", 2),
      new BBTag("RIGHT", "<div style=\"text-align: right;\">", "</div>", 3),
      new BBTag("IMG", "<img class=\"image-medium\" src=\"${content}\" />", "", false, true, 4, allowUrlProcessingAsText: false), 
      new BBTag("IMGSMALL", "<img class=\"image-small\" src=\"${content}\" />", "", false, true, 5, allowUrlProcessingAsText: false), 
      new BBTag("IMGLARGE", "<img class=\"image-large\" src=\"${content}\" />", "", false, true, 6, allowUrlProcessingAsText: false), 
    });
  }
}

