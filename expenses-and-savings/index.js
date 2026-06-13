const separator = "<separator>";

document.getElementById("radio-expense").onclick = function (){
    document.getElementById("radio-saving").checked = false;
}
document.getElementById("radio-saving").onclick = function (){
    document.getElementById("radio-expense").checked = false;
}

function add(){
    document.getElementById("add-panel").style.display = "flex";
}

function clearAll(){
    localStorage.setItem("savedData", "");
    window.location.reload();
}

function cancelAddPanel(){
    document.getElementById("add-panel").style.display = "none";
}

function compute(){
    let savedData = localStorage.getItem("savedData");
    let split = savedData.split("\n");
    let expenses = 0;
    let savings = 0;
    for (let a = 0; a < split.length; a++){
        if (split[a] != ""){
            let value_split = split[a].split(separator);
            for (let b = 0; b < value_split.length; b++){
                if (b == 2){
                    if (value_split[b].startsWith("-")){
                        expenses += parseFloat(value_split[b].substring(2));
                    }
                    if (value_split[b].startsWith("+")){
                        savings += parseFloat(value_split[b].substring(2));
                    }
                }
            }
        }
    }
    document.getElementById("compute-panel").style.display = "flex";
    document.getElementById("total-expenses").innerHTML = `Total expenses: ₱${expenses}`;
    document.getElementById("total-savings").innerHTML = `Total savings: ₱${savings}`;
    document.getElementById("total-money").innerHTML = `Total money: ₱${savings - expenses}`;
}

function closeComputePanel(){
    document.getElementById("compute-panel").style.display = "none";
}

function confirmAddPanel(){
    const date = new Date();
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    let final_date = `${year}-${month}-${day}`;
    const name = document.getElementById("item-name").value;
    const price = (document.getElementById("radio-expense").checked ? "-" : "+") + "₱" + document.getElementById("item-price").value;
    const reason = document.getElementById("item-reason").value;
    let data = `${final_date}${separator}${name}${separator}${price}${separator}${reason}\n`;
    localStorage.setItem("savedData", localStorage.getItem("savedData") + data);
    window.location.reload();
}

function refreshData(){
    let savedData = localStorage.getItem("savedData");
    let split = savedData.split("\n");
    for (let a = 0; a < split.length; a++){
        if (split[a] != ""){
            let tr = document.createElement("tr");
            let value_split = split[a].split(separator);
            for (let b = 0; b < value_split.length; b++){
                let td = document.createElement("td");
                if (b == 2){
                    td.classList.add("price");
                }
                td.innerHTML = (b == value_split.length - 1 ? (value_split[b] == "" ? "No reason available" : value_split[b]) : value_split[b]);
                tr.appendChild(td);
            }
            document.getElementById("main-table").appendChild(tr);
        }
    }
}

refreshData();