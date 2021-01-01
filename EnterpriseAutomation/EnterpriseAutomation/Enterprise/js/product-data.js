function getComboA(selectObject) {
    var value = selectObject.value;
    document.getElementById('hfValue').value = value;
}

function getSuscriptionValue(selectObject) {
    var value = selectObject.value;
    $('#hfSubscription').val(value);
    __doPostBack('hfSubscription');
}

function getServerValue(selectObject) {
    var value = selectObject.value;
    $('#hfCurrentServer').val(value);
    __doPostBack('hfCurrentServer');
}


//Getting Servers List from Subscription
/*var HiddenValue = document.getElementById('hfServers').value;
aServers = HiddenValue.split("[");
aServers = aServers[2].split(",");
for (var i = 0; i < aServers.length; i++) {
    var newSelect = document.createElement('option');
    selectHTML = "<option value='" + aServers[i] + "'>" + aServers[i] + "</option>";
    newSelect.innerHTML = selectHTML;
    //document.getElementById('serverValue').add(newSelect);
    document.getElementById('serverValue').append(newSelect);
}*/


//Getting Databases List from Server
/*var HiddenValue_1 = document.getElementById('hfDatabases').value;
aDatabases = HiddenValue_1.split("[");
aDatabases = aDatabases[2].split(",");
for (var i = 0; i < aDatabases.length; i++) {
    var newSelect = document.createElement('option');
    selectHTML = "<option value='" + aDatabases[i] + "'>" + aDatabases[i] + "</option>";
    newSelect.innerHTML = selectHTML;
    document.getElementById('databaseValue').add(newSelect);
}*/

