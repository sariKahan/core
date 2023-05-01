const uri = '/User';

function login(){

    const name = document.getElementById('name');
    const password = document.getElementById('password');
    alert("נכנסתי לפונקציה");
    const item = {
       
        Password: password.value.trim(),
        Name: name.value.trim(),
    };

    fetch(`${uri}/Login`, {
        method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        
        .then(response => response.text())
        .then((data) => {
            sessionStorage.setItem('token', data.slice(1,data.length-1));

            // const arrSongsChosen = (sessionStorage.getItem('token'));
            //let indexSong = JSON.parse(sessionStorage.getItem('indexSong'));
            // alert("arrSongsChosen"+(arrSongsChosen));
            // name.value = '';
            location.href="../html/display.html";
        })
        .catch(error => console.error('user not valid.'));
}