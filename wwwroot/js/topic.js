"use strict;"

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalR").build();

$("#post-submit-button").disabled = true;

connection.on("ReceivePost", function(postRawHtml) {
  const parser = new DOMParser();
  const postContainer = $("#post-container");
  postContainer.append(postRawHtml);
  postContainer.append('<br>');
});

connection.start().then(function () {
  $("#post-submit-button").disabled = false;
  connection.invoke("AddToTopicGroup", topicId);
}).catch(function (err) {
  return console.error(err.toString());
});