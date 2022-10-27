using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace Beon.Services {
  public interface IViewComponentRenderService {
    public Task<string> RenderAsync(
      ActionContext controllerContext,
      ViewDataDictionary viewData,
      ITempDataDictionary tempData,
      string name,
      object model);
  }

  public class ViewComponentRenderService : IViewComponentRenderService {

    private readonly DefaultViewComponentHelper _helper;
    public ViewComponentRenderService(
      IViewComponentDescriptorCollectionProvider provider,
      IViewComponentSelector selector,
      IViewComponentInvokerFactory invokerFactory,
      IViewBufferScope scope)
    {  
      _helper = new DefaultViewComponentHelper(provider, HtmlEncoder.Default, selector, invokerFactory, scope);
    }

    public async Task<string> RenderAsync(
      ActionContext controllerContext,
      ViewDataDictionary viewData,
      ITempDataDictionary tempData,
      string name,
      object model)
    {
      using (var writer = new StringWriter())
      {
        ViewContext context = new ViewContext(
          controllerContext,
          NullView.Instance,
          viewData,
          tempData,
          writer,
          new HtmlHelperOptions());
         
        _helper.Contextualize(context);
        var result = await _helper.InvokeAsync(name, model);
        result.WriteTo(writer, HtmlEncoder.Default);
        await writer.FlushAsync();
        return writer.ToString();
      }
    }



    private class NullView : IView
    {
        public static readonly NullView Instance = new();

        public string Path => string.Empty;

        public Task RenderAsync(ViewContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.CompletedTask;
        }
    }
  }
}