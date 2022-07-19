function toggleMenu(){
    if(document.querySelector("#menu").style.visibility == "visible")
        document.querySelector("#menu").style.visibility = "hidden";
    else if(document.querySelector("#menu").style.visibility == "hidden")
        document.querySelector("#menu").style.visibility = "visible";
}

function resetSearch(){
    document.querySelector("#searchField").value = "";
}

function expandTopic(id){

    var topicId = "#topic"+id+"details";

    document.querySelector(topicId).style.height == "0px" ?
    document.querySelector(topicId).style.height = "auto" :
    document.querySelector(topicId).style.height = "0px";

    document.querySelector(topicId).style.visibility == "hidden" ?
    document.querySelector(topicId).style.visibility = "visible" :
    document.querySelector(topicId).style.visibility = "hidden";
    
}   