// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

PlayOkSound = function () {
  var audio = new Audio('/i/ok.ogg');
  audio.loop = false;
  audio.play();
}

deleteLinkClicked = function(event, link) {
  event.preventDefault();
  parent = link.parentNode;
  form = $(parent).find('.delete-form');
  form.submit();
}

optionsLinkClicked = function(event, link) {
  event.preventDefault();
  parent = link.parentNode;
  options = $(parent).find('.post-options');
  options.css('display', 'inline-block');
  $(link).css('display', 'none');
}

formLinkClicked = function (event, link) {
  event.preventDefault();
  parent = link.parentNode;
  form = $(parent).find('form');
  form.submit();
}

addTag = function(tag, hbutton) {
  // hbutton -> hbuttonbar -> form
  parent = hbutton.parentNode.parentNode;
  textarea = $(parent).find('textarea');
  textarea.replaceSelectedText('[' + tag + ']' + textarea.getSelection().text + '[/' + tag + ']');
}

addSmile = function(smile, button) {
  // button -> smile-box -> form
  parent = button.parentNode.parentNode;
  textarea = $(parent).find('textarea');
  textarea.replaceSelectedText(smile);
}