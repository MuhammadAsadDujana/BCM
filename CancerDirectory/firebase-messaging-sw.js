importScripts('https://www.gstatic.com/firebasejs/8.0.1/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/8.0.1/firebase-messaging.js');


var firebaseConfig = {
    apiKey: "AIzaSyBQKc37xVjdTroL22cyc55tkBgy4s_dbAs",
    authDomain: "bcmtest-b9214.firebaseapp.com",
    databaseURL: "https://bcmtest-b9214.firebaseio.com",
    projectId: "bcmtest-b9214",
    storageBucket: "bcmtest-b9214.appspot.com",
    messagingSenderId: "409812816691",
    appId: "1:409812816691:web:60aa535ee130bb48eb6446"
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);

// Retrieve Firebase Messaging object.
const messaging = firebase.messaging();