// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

PlayOkSound = function () {
  var audio = new Audio('/i/ok.ogg');
  audio.loop = false;
  audio.play();
}