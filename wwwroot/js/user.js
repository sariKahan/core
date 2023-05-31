const uri = '/User';
let ListUser = [];
const token = sessionStorage.getItem('token');
function getUsers() {
    fetch(uri, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },

    })
        .then(response => response.json())
        .then(data => {
            _displayItems(data)
        }

        )
        .catch(error => console.log('Unable to get items.'));
}

function addUser() {
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');

    const item = {
        name: addNameTextbox.value.trim(),
        password : addPasswordTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': "Bearer " + token
        },
        body: JSON.stringify(item)
    })
        .then(response => (response.json()))
        .then((data) => {
            getUsers();
            addNameTextbox.value = '';
            addPasswordTextbox.value = '';
        })
        .catch(error => console.log('Unable to add item.'));
}

function deleteUser(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
    })
        .then(() => getUsers())
        .catch(error => console.log('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = ListUser.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-password').value = item.password;
    document.getElementById('editForm').style.display = 'block';
}

function updateUser() {
    const id = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(id, 10),
        name: document.getElementById('edit-name').value.trim(),
        password: document.getElementById('edit-password').value.trim()
};

    fetch(`${uri}/${id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        },
        body: JSON.stringify(item)
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'User' : 'Users';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('listUser');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUser(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(item.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNode1 = document.createTextNode(item.password);
        td2.appendChild(textNode1);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    ListUser = data;
}
