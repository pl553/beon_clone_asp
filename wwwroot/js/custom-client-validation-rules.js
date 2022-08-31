(function($) {
  $.validator.addMethod("filesize", function(value, element, params) {
    var maxFileSize = $(element).data('val-filesize-max');
    if (!element.files[0]) {
      return true;
    }
    return element.files[0].size <= maxFileSize;
  });

  $.validator.unobtrusive.adapters.addBool("filesize");
})(jQuery);