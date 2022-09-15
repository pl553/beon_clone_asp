"use strict;"

// expects topicId to be defined

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalR").build();

$("#post-submit-button").disabled = true;

connection.on("ReceiveComment", function(postId) {
  $.get("/Comment/GetRawHtml?postId=" + postId.toString(), function (data) {
    const parser = new DOMParser();
    const postContainer = $("#comment-container");
    postContainer.append(data);
    PlayOkSound();
    connection.invoke("ReceivedComment", topicId);
  });
});

connection.start().then(function () {
  $("#post-submit-button").disabled = false;
  connection.invoke("AddToTopicGroup", topicId);
}).catch(function (err) {
  return console.error(err.toString());
});