const uri = '/TasksProject';
let ListJob = [];
const token = sessionStorage.getItem('token');
function getTasks() {
    fetch(`${uri}/Get`, {
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

function addTask() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        name: addNameTextbox.value.trim(),
        IsDone: false
    };

    fetch(`${uri}/Post`, {
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
            getTasks();
            addNameTextbox.value = '';
        })
        .catch(error => console.log('Unable to add item.'));
}

function deleteTask(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
    })
        .then(() => getTasks())
        .catch(error => console.log('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = ListJob.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-IsDone').checked = item.isDone;
    document.getElementById('editForm').style.display = 'block';
}

function updateTask() {
    const id = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(id, 10),
        name: document.getElementById('edit-name').value.trim(),
        isDone: document.getElementById('edit-IsDone').checked
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
        .then(() => getTasks())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Task' : 'Tasks';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('listJob');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let IsDoneCheckbox = document.createElement('input');
        IsDoneCheckbox.type = 'checkbox';
        IsDoneCheckbox.disabled = true;
        IsDoneCheckbox.checked = item.isDone;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteTask(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    ListJob = data;
}
