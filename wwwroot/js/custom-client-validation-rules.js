(function($) {
  //i was trying to get clientside image validation working but had no success so

  /*$.validator.addMethod("filesize", function(value, element, params) {
    var maxFileSize = $(element).data('val-filesize-max');
    if (!element.files[0]) {
      return true;
    }
    return element.files[0].size <= maxFileSize;
  });

  $.validator.unobtrusive.adapters.addBool("filesize");*/

  /*$.validator.addMethod("image", function(value, element, params) {
    if (!element.files[0]) {
      return true;
    }
    console.log("a");
    return false;
    /*var p = await new Promise((resolve) => {
      var image = new Image();
      image.src = URL.createObjectURL(element.files[0]);
      image.onload = () => {
        resolve("ok");
      }
      image.onerror = () => {
        resolve("bad");
      }
    });
    if (p === "ok") {
      return true;
    }
    else {
      return false;
    }
  });

  $.validator.unobtrusive.adapters.addBool("image");*/
})(jQuery);

