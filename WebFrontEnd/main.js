var staffsData = {};
var staffPopup, staffForm, infoPopup, deleteConfirmPopup;
var staffTypeObject = {
    teaching: "teaching",
    admin: "admin",
    support: "support"
}
var infoPopupBackgroundStyles = {
    success: "#4CAF50",
    error: "#f44336",
    info: "#2196F3",
    warning: "#ff9800"
}
staffsData.SelectedType = staffTypeObject.teaching; //initial stafftype

//When document loaded
document.addEventListener("DOMContentLoaded", () => {
    staffPopup = document.getElementById("staffFormPopup");
    staffForm = document.getElementById("staffForm");
    infoPopup = document.getElementById("infoPopup");
    deleteConfirmPopup = document.getElementById("deleteConfirmationPopup");
    getAllStaffFromWeb(staffsData.SelectedType);
});

function getAllStaffFromWeb(typeStr) {

    fetch(`http://localhost:56366/Staff/?type=${typeStr}`, {
        method: "GET",
    })
        .then((response) => {
            if (response.ok) {
                return response.json();
            } else if (response.status == 404) {
                staffsData.SelectedType = typeStr;
                staffsData.staffs = null;
                loadStaffsToTable();
            } else {
                throw "invalid Staff Type"
            }
        })
        .then((i) => {
            staffsData.SelectedType = typeStr;
            if (i != null) {
                staffsData.staffs = [...i];
                loadStaffsToTable();
            }
        })
        .catch((error) => showInfoPopup(error, 3000, infoPopupBackgroundStyles.error));
}

function createStaffInWeb(staffObj) {
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify(staffObj);

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw
    };

    fetch("http://localhost:56366/Staff/", requestOptions)
        .then(response => {
            if (response.ok) {
                getAllStaffFromWeb(staffsData.SelectedType);
                showInfoPopup("Staff Created", 3000, infoPopupBackgroundStyles.success)
            } else {
                showInfoPopup("Invaid Request.", 3000, infoPopupBackgroundStyles.error)
            }
            closeStaffPopup();
            return response.text();
        })
        .then(result => console.log(result))
        .catch(error => showInfoPopup(error, 3000, infoPopupBackgroundStyles.error));
}

function updateStaffInWeb(staffObj) {
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify(staffObj);

    var requestOptions = {
        method: 'PUT',
        headers: myHeaders,
        body: raw
    };

    fetch(`http://localhost:56366/Staff/${staffObj["id"]}`, requestOptions)
        .then(response => {
            if (response.ok) {
                getAllStaffFromWeb(staffsData.SelectedType);
                showInfoPopup("Staff Updated", 3000, infoPopupBackgroundStyles.success)
            } else if (response.status === 404) {
                showInfoPopup("Staff not Found.", 3000, infoPopupBackgroundStyles.error)
            } else {
                showInfoPopup("Invaid Request.", 3000, infoPopupBackgroundStyles.error)
            }
            closeStaffPopup();
            return response.text();
        })
        .then(result => console.log(result))
        .catch(error => showInfoPopup(error, 3000, infoPopupBackgroundStyles.error));
}

function deleteStaffInWeb(staffIdForDelete) {
    var requestOptions = {
        method: 'DELETE'
    }

    fetch(`http://localhost:56366/Staff/${staffIdForDelete}`, requestOptions)
        .then(response => {
            if (response.ok) {
                getAllStaffFromWeb(staffsData.SelectedType);
                showInfoPopup("Staff Deleted", 3000, infoPopupBackgroundStyles.success)
            } else if (response.status === 404) {
                showInfoPopup("Staff not Found.", 3000, infoPopupBackgroundStyles.error)
            } else {
                showInfoPopup("Invaid Request.", 3000, infoPopupBackgroundStyles.error)
            }
            return response.text();
        })
        .then(result => console.log(result))
        .catch(error => showInfoPopup(error, 3000, infoPopupBackgroundStyles.error));
}


function loadStaffsToTable() {

    var table = document.getElementById("staffs-table");

    //Clear table
    table.innerHTML = "";

    //Add the header row.
    //--Adding specific Fields
    var specificItems = "";
    switch (staffsData.SelectedType) {
        case staffTypeObject.teaching:
            specificItems = `<th onClick="sortAndSwitch(this.cellIndex, true, this)">Subject Name</th>`;
            break;
        case staffTypeObject.admin:
            specificItems = `<th onClick="sortAndSwitch(this.cellIndex, true, this)">Position</th>`;
            break;
        case staffTypeObject.support:
            specificItems = `<th onClick="sortAndSwitch(this.cellIndex, true, this)">Role</th>`;
            break;
        default:
            return;
    }
    //--Adding common Fields
    var row = `<tr>
                <th> <input type="checkbox" onClick="onSelectAllStaff()"></th>
                <th onClick="sortAndSwitch(this.cellIndex, true, this)">Staff ID</th>
                <th onClick="sortAndSwitch(this.cellIndex, true, this)">Name</th>
                ${specificItems}
                <th>Edit/Delete</th>
            </tr>`;

    table.innerHTML = row;

    if (staffsData.staffs == null || !(Symbol.iterator in Object(staffsData.staffs))) {
        table.insertRow().innerHTML += '<tr><td colspan="1000" style="text-align: center;">No staff</td></tr>';
        return;
    }

    //Fill rows
    for (let element of staffsData.staffs) {

        //Add row
        let row = table.insertRow();

        //onclick for row
        //row.onclick = () => editStaffPopup(element["id"]);

        //Adding checkbox
        row.insertCell().innerHTML = `<input type="checkbox">`

        //Fill common Fields (cells)
        insertCellToRow(row, element["id"])
        insertCellToRow(row, element["name"])

        //Fill specific Fields (cells)
        switch (staffsData.SelectedType) {
            case staffTypeObject.teaching:
                insertCellToRow(row, element["subjectName"])
                break;
            case staffTypeObject.admin:
                insertCellToRow(row, element["position"])
                break;
            case staffTypeObject.support:
                insertCellToRow(row, element["role"])
                break;
            default:
                table.innerHTML = ""
                return;
        }

        //Edit symbol
        var cell = row.insertCell();
        cell.style = "text-align: center;"
        cell.innerHTML = `<span onClick="editStaffPopup(${element["id"]})" style='font-size: x-large; cursor: pointer'>&#9998;</span>
                          <span onClick="showDeleteConfirmPopup(()=>deleteOneStaff(${element["id"]}))" style='font-size: x-large; cursor: pointer; color: #d11a2a;'><b>&#128465;<b></span>`;
    }
}

function insertCellToRow(row, value) {
    var cell = row.insertCell();
    var text = document.createTextNode(value);
    cell.appendChild(text);
}

function sortAndSwitch(index, isReverse, cell) {

    //Clear all symbols
    var table = document.getElementById("staffs-table");
    for (var j = 0, col; col = table.rows[0].cells[j]; j++) {
        col.setAttribute("class", "");
    }

    cell.setAttribute("onClick", `sortAndSwitch(${index}, ${!isReverse}, this)`);
    cell.setAttribute("class", `${isReverse ? "headerSortUp" : "headerSortDown"}`);
    sortTable(index, isReverse);
}

function sortTable(nameIndex, isReverse) {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("staffs-table");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[nameIndex];
            y = rows[i + 1].getElementsByTagName("TD")[nameIndex];
            if (isReverse) {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function onSelectAllStaff() {
    table = document.getElementById("staffs-table");
    rows = table.rows;
    var checkedState = table.rows[0].cells[0].getElementsByTagName("input")[0]?.checked;
    if (checkedState != null) {
        for (i = 1; i < rows.length; i++) {
            try{
                table.rows[i].cells[0].getElementsByTagName("input")[0].checked = checkedState;
            }catch{}
        }
    }
}

window.onclick = function (event) {
    if (event.target == staffPopup) {
        closeStaffPopup();
    } else if (event.target == infoPopup) {
        closeInfoPopup();
    }
}

function closeDeleteConfirmPopup() {
    document.getElementById("deleteConfirmedButton").onclick = null;
    deleteConfirmPopup.style.display = "none";
}

function showDeleteConfirmPopup(callback) {
    document.getElementById("deleteConfirmedButton").onclick = callback;
    deleteConfirmPopup.style.display = "block";
}

function deleteOneStaff(staffIdToDelete) {
    deleteStaffInWeb(staffIdToDelete);
    closeDeleteConfirmPopup();
}

function deleteSelectedStaffs() {
    closeDeleteConfirmPopup();
    table = document.getElementById("staffs-table");
    var staffIdList = [];
    rows = table.rows;
    for (i = 1; i < rows.length; i++) {
        if (table.rows[i].cells[0].getElementsByTagName("input")[0]?.checked) {
            staffIdList.push(table.rows[i].cells[1].innerText)
        }
    }
    console.log(staffIdList);
    staffIdList.forEach(deleteStaffInWeb);
}

function closeStaffPopup() {
    //staffPopup.style.display = "none";
    staffPopup.style.opacity = "0";
    setTimeout(() => { staffPopup.style.display = "none"; }, 600);
}

function showStaffPopup() {
    staffPopup.style.display = "block";
    staffPopup.style.opacity = "1";
}

function showInfoPopup(infoText, timeOut, infoPopupBackgroundStyle) {
    document.getElementById("infoPopupText").innerHTML = infoText;
    document.getElementById("infoPopupBody").style.backgroundColor = infoPopupBackgroundStyle;
    infoPopup.style.display = "block";
    infoPopup.style.opacity = "1";
    setTimeout(closeInfoPopup, timeOut)
}

function closeInfoPopup() {
    infoPopup.style.opacity = "0";
    setTimeout(() => {
        infoPopup.style.display = "none";
        document.getElementById("infoPopupText").innerHTML = "";
    }, 600);
}

function createForm(type, isCreating) {

    //Creating staff Common Fields
    staffForm.innerHTML = `<label for="stafftypeSelect">Staff Type</label>
                            <select name="stafftypeSelect" id="stafftypeSelect" onchange="createForm(this.value, true)" ${isCreating ? "" : "disabled"}>
                                <option value=${staffTypeObject.teaching} ${type === staffTypeObject.teaching ? "selected" : ""}>Teaching Staff</option>
                                <option value=${staffTypeObject.admin}    ${type === staffTypeObject.admin ? "selected" : ""}>Administrative Staff</option>
                                <option value=${staffTypeObject.support}  ${type === staffTypeObject.support ? "selected" : ""}>Support staff</option>
                            </select>
                            ${isCreating ? "" : `<label for="staffID">Staff ID</label><input id="staffID" type="number" placeholder="Staff ID" disabled>`}
                            <label for="staffname">Name</label>
                            <input id="staffname" type="text" placeholder="Staff Name" required>`;

    //Creating staff Specific Fields
    switch (type) {
        case staffTypeObject.teaching:
            staffForm.innerHTML += `<label for="staffSubjectName">Subject Name</label>
                                    <input id="staffSubjectName" type="text" placeholder="Subject Name" required>`;
            break;
        case staffTypeObject.admin:
            staffForm.innerHTML += `<label for="staffPosition">Position</label>
                                    <input id="staffPosition" type="text" placeholder="Position" required>`;
            break;
        case staffTypeObject.support:
            staffForm.innerHTML += `<label for="staffRole">Role</label>
                                    <input id="staffRole" type="text" placeholder="Role" required>`;
            break;
        default:
            staffForm.innerHTML = "";
            return;
    }

    if (isCreating) {
        staffForm.innerHTML += `<input type="button" value="submit" onClick="createStaffInWeb(readStaffDataFromForm())">`;
        document.getElementById("StaffFormTitle").innerHTML = "Create Staff";
    } else {
        staffForm.innerHTML += `<input type="button" value="Update" onClick="updateStaffInWeb(readStaffDataFromForm())">`;
        document.getElementById("StaffFormTitle").innerHTML = "Edit Staff";
    }
}

function createNewStaffPopup() {

    //Setting up Form
    createForm(staffsData.SelectedType, true);
    //Show popup
    showStaffPopup();
}

function editStaffPopup(staffId) {
    var staffFromId = staffsData?.staffs?.find(s => s["id"] === staffId)
    if (staffFromId == null) return;

    createForm(getStaffTypeFromNum(staffFromId["staffType"]), false);

    //inflating common values
    document.getElementById("staffID").value = staffFromId["id"];
    document.getElementById("staffname").value = staffFromId["name"];

    //inflating staff Specific values
    switch (getStaffTypeFromNum(staffFromId["staffType"])) {
        case staffTypeObject.teaching:
            document.getElementById("staffSubjectName").value = staffFromId["subjectName"];
            break;
        case staffTypeObject.admin:
            document.getElementById("staffPosition").value = staffFromId["position"];
            break;
        case staffTypeObject.support:
            document.getElementById("staffRole").value = staffFromId["role"];
            break;
        default:
            staffForm.innerHTML = "";
            return;
    }

    //Show popup
    showStaffPopup();
}

function readStaffDataFromForm() {

    //Read common Fields
    var staffObject = {
        name: document.getElementById("staffname").value,
        staffType: getNumFromStaffType(document.getElementById("stafftypeSelect").value)
    }
    if (document.getElementById("staffID")) {
        staffObject["id"] = document.getElementById("staffID").value;
    }

    //Read staff specific Fields
    switch (document.getElementById("stafftypeSelect").value) {
        case staffTypeObject.teaching:
            staffObject["subjectName"] = document.getElementById("staffSubjectName").value;
            break;
        case staffTypeObject.admin:
            staffObject["position"] = document.getElementById("staffPosition").value;
            break;
        case staffTypeObject.support:
            staffObject["role"] = document.getElementById("staffRole").value;
            break;
        default:
            staffForm.innerHTML = "";
            return;
    }

    return staffObject;
}

function getStaffTypeFromNum(typeNum) {
    switch (typeNum) {
        case 1:
            return staffTypeObject.teaching;
        case 2:
            return staffTypeObject.admin;
        case 3:
            return staffTypeObject.support;
        default:
            return;
    }
}

function getNumFromStaffType(staffTypeForChange) {
    switch (staffTypeForChange) {
        case staffTypeObject.teaching:
            return 1;
        case staffTypeObject.admin:
            return 2;
        case staffTypeObject.support:
            return 3;
        default:
            return;
    }

}