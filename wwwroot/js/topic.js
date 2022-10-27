"use strict;"

// expects topicPostId to be defined

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalR").build();

$("#post-submit-button").disabled = true;

connection.on("ReceiveComment", function(commentPostId) {
  $.get("/Post/GetRawHtml?postId=" + commentPostId.toString(), function (data) {
    const parser = new DOMParser();
    const postContainer = $("#comment-container");
    postContainer.append(data);
    PlayOkSound();
    connection.invoke("ReceivedComment", topicPostId);
  });
});

connection.start().then(function () {
  $("#post-submit-button").disabled = false;
  connection.invoke("AddToTopicGroup", topicPostId);
}).catch(function (err) {
  return console.error(err.toString());
});