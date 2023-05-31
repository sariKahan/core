const uri = '/User';

function login(){

    const name = document.getElementById('name');
    const password = document.getElementById('password');
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
        
        .then(response => response.json())
        .then((data) => {
            sessionStorage.setItem('token',data.token);
            if(data.isAdmin)
                location.href="../html/user.html";
            else
                location.href="../html/task.html";
        })
        .catch(error => console.error('user not valid.'));
}