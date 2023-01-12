function shouldDisplayAlert() {
    var pass1 = document.getElementById("firstPassword").value;
    var pass2 = document.getElementById("secondPassword").value;
    if (pass1 != pass2)
        alert("Password do not match!");
}
