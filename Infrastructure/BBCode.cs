using CodeKicker.BBCode.Core;

namespace Beon.Infrastructure {
  public static class BBCode {
    public static readonly BBCodeParser parser = new BBCodeParser(new []
    {
      new BBTag("B", "<b>", "</b>", 0),
      new BBTag("PRE", "<pre>", "</pre>", 1),
      new BBTag("CENTER", "<div style=\"text-align: center;\">", "</div>", 2),
      new BBTag("RIGHT", "<div style=\"text-align: right;\">", "</div>", 3)
    });
  }
}

