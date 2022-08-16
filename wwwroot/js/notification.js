"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

//Disable the send button until connection is established.
// document.getElementById("btnSubmit").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    // var li = document.createElement("li");
     //document.getElementById("messagesList").appendChild(li);
     //We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    // li.textContent = `${user} says ${message}`;
    alert(`${user} ${message}`);
});

connection.start().then(function () {
    // document.getElementById("btnSubmit").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// document.getElementById("btnSubmit").addEventListener("click", function (event) {
//     var user = document.getElementById("Name").value;
//     var message = "- Actor added successfully";
//     connection.invoke("SendMessage", user, message).catch(function (err) {
//         return console.error(err.toString());
//     });
//     event.preventDefault();
// });